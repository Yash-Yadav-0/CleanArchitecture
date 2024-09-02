using CleanArchitecture.Application.Bases;
using CleanArchitecture.Application.Features.UserFeature.Rules;
using CleanArchitecture.Application.Helpers;
using CleanArchitecture.Application.Interfaces.AutoMapper;
using CleanArchitecture.Application.Interfaces.UnitOfWorks;
using CleanArchitecture.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Serilog;
using System.Security.Claims;

namespace CleanArchitecture.Application.Features.UserFeature.Commands.ChangeToMember
{
    //Change Admin / Vendor to User (normal/customer)
    public class ChangeToMemberCommandHandler : BaseHandler, IRequestHandler<ChangeToMemberCommandRequest, ChangeToMemberCommandResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserManager<User> userManager;
        private readonly UserRules userRules;
        private readonly RoleManager<Role> roleManager;

        public ChangeToMemberCommandHandler(IUnitOfWork unitOfWork,
                                                UserManager<User> userManager,
                                                RoleManager<Role> roleManager,
                                                IMapper mapper,
                                                UserRules userRules,
                                                IHttpContextAccessor httpContextAccessor)
            : base(unitOfWork, mapper, httpContextAccessor)
        {
            _unitOfWork = unitOfWork;
            this.userManager = userManager;
            this.roleManager = roleManager;
            this.userRules = userRules;
            this._mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<ChangeToMemberCommandResponse> Handle(ChangeToMemberCommandRequest request, CancellationToken cancellationToken)
        {
            LoggerHelper.LogInformation("Handling ChangeToMemberCommandRequest for Email: {Email}", request.Email);
            //used for role identification in case of checking in after controller passes through role check(double check)
            /*var userId = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (userId == null)
            {
                LoggerHelper.LogError("User is not authenticated.", 
                    new UnauthorizedAccessException("User is not authenticated."));
                throw new UnauthorizedAccessException("User is not authenticated.");
            }

            // Find the user and check if they are in the Admin role
            var userAdmin = await userManager.FindByIdAsync(userId);
            if (userAdmin == null || !(await userManager.IsInRoleAsync(userAdmin, "Admin")))
            {
                LoggerHelper.LogError("User does not have the required Admin role.",
                    new UnauthorizedAccessException("User does not have the required Admin role."));
                throw new UnauthorizedAccessException("User does not have the required Admin role.");
            }
            //await userRules.UserShouldnotBeExistsAsync(await userManager.FindByEmailAsync(request.Email));*/

            User? user = await userManager.FindByEmailAsync(request.Email);

            if (user == null)
            {
                LoggerHelper.LogError("Unable to find User with Email: {Email}", 
                    new Exception("User with specified Email does not exist"), request.Email);
                throw new Exception("User with specified Id does not exist");
            }
            try
            {
                // Remove any existing role if necessary before adding new one
                var currentRoles = await userManager.GetRolesAsync(user);
                if (currentRoles == null)
                {
                    currentRoles = new List<string>(); // Default to empty list if null
                }

                foreach (var role in currentRoles)
                {
                    await userManager.RemoveFromRoleAsync(user, role);
                }

                await userManager.AddToRoleAsync(user, "Customer");
                await userManager.UpdateAsync(user);

                LoggerHelper.LogInformation("User with Email: {Email} successfully changed to Customer role.", request.Email);
            }
            catch (Exception ex)
            {
                LoggerHelper.LogError("Error occurred while changing user to Customer role for Email: {Email}", ex, request.Email);
                throw; // Re-throw the exception after logging
            }
            return new()
            {
                MessageToReturn = user.Id.ToString(),
            };
        }
    }
}