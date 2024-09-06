using CleanArchitecture.Application.Helpers;
using CleanArchitecture.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Logging;

namespace CleanArchitecture.Application.Features.UserFeature.Queries
{
    public class ShowAllUserPermissionsQueryHandler : IRequestHandler<ShowAllUserPermissionsQueryRequest,List<ShowAllUserPermissionsResponse>>
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<Role> _roleManager;

        public ShowAllUserPermissionsQueryHandler(
            UserManager<User> userManager,
            RoleManager<Role> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<List<ShowAllUserPermissionsResponse>> Handle(ShowAllUserPermissionsQueryRequest request, CancellationToken cancellationToken)
        {
            LogHelper.LogInformation("Handling ShowAllUserPermission for Admin/SuperAdmin ");

            var users = await _userManager.Users.ToListAsync(cancellationToken);
            if (users == null)
            {
                LoggerHelper.LogWarning("Unable to find users , connection to db error !");
                throw new Exception("Unable to find users , connection to db error ! exception");
            }

            var userPermissionsList = new List<ShowAllUserPermissionsResponse>();
            try
            {
                foreach (var user in users)
                {
                    var roles = await _userManager.GetRolesAsync(user);
                    var roleEntities = await _roleManager.Roles
                        .Where(r => roles.Contains(r.Name))
                        .ToListAsync();

                    var userPermissions = Permissions.None;
                    foreach (var role in roleEntities)
                    {
                        userPermissions |= role.Permissions;
                    }

                    userPermissionsList.Add(new ShowAllUserPermissionsResponse
                    {
                        UserId = user.Id,
                        Email = user.Email,
                        Roles = roles.ToList(),
                        Permissions = userPermissions.ToString()
                    });
                    LogHelper.LogInformation(" Successful handler request for user permissions.");
                }
            }
            catch(Exception ex)
            {
                LoggerHelper.LogError("Error occured in :", ex, null);
            }

            return userPermissionsList;
        }
    }
}
