using CleanArchitecture.Application.Helpers;
using CleanArchitecture.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Logging;

namespace CleanArchitecture.Application.Features.UserFeature.Commands.ChangeUserPermissions
{
    public class ChangeUserPermissionsHandler : IRequestHandler<ChangeUserPermissionsRequest,ChangeUserPermissionsResponse>
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<Role> _roleManager;
        public ChangeUserPermissionsHandler(
            UserManager<User> userManager,
            RoleManager<Role> roleManager
            )
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<ChangeUserPermissionsResponse> Handle(ChangeUserPermissionsRequest request,CancellationToken cancellationToken)
        {
            LogHelper.LogInformation($"Handling ChangeUserPermissions for Email: {request.Email}");

            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user == null)
            {
                LoggerHelper.LogWarning($"User not found: {request.Email}");
                throw new Exception($"Unable to find user with Email: {request.Email}");
            }
            var expectedPermissions = PermissionProvider.GetAll(includeNone: false, includeAll: false).Aggregate(Permissions.None, (current, permission) => current | permission);
            if ((request.permissions & ~expectedPermissions) != Permissions.None)
            {
                LoggerHelper.LogWarning($"Invalid permissions requested for Email: {request.Email}");
                throw new Exception("Invalid permissions requested.");
            }
            try
            {
                // Remove  current roles from user
                var currentRoles = await _userManager.GetRolesAsync(user);
                if (currentRoles.Any())
                {
                    var removeResult = await _userManager.RemoveFromRolesAsync(user, currentRoles);
                    if (!removeResult.Succeeded)
                    {
                        throw new Exception("Failed to remove user from current roles");
                    }
                }

                // Assign the user to new roles based on the provided permissions
                var newRoles = await _roleManager.Roles
                    .Where(r => (r.Permissions & request.permissions) != Permissions.None)
                    .Select(r => r.Name)
                    .ToListAsync(cancellationToken);

                if (newRoles.Any())
                {
                    var addResult = await _userManager.AddToRolesAsync(user, newRoles);
                    if (!addResult.Succeeded)
                    {
                        throw new Exception("Failed to assign new roles to the user");
                    }
                }

                // Update user permissions
                var updatedPermissions = Permissions.None;
                var updatedRoleEntities = await _roleManager.Roles
                    .Where(r => newRoles.Contains(r.Name))
                    .ToListAsync(cancellationToken);

                foreach (var role in updatedRoleEntities)
                {
                    updatedPermissions |= role.Permissions;
                }

                return new ChangeUserPermissionsResponse
                {
                    UserId = user.Id,
                    Email = user.Email,
                    Roles = newRoles,
                    Permissions = updatedPermissions.ToString()
                };
            }
            catch (Exception ex)
            {
                LoggerHelper.LogError("Error occurred in ChangeUserPermissionsHandler:", ex, null);
                throw;
            }
        }
    }
}
