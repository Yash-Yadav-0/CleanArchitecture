using CleanArchitecture.Application.Bases;
using CleanArchitecture.Application.Features.Auth.Exceptions;
using CleanArchitecture.Application.Features.UserFeature.Rules;
using CleanArchitecture.Application.Helpers;
using CleanArchitecture.Application.Interfaces.AutoMapper;
using CleanArchitecture.Application.Interfaces.UnitOfWorks;
using CleanArchitecture.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Serilog;
using System.Security.Claims;


namespace CleanArchitecture.Application.Features.UserFeature.Commands.ChangeToVendor
{
    //Change User to Vendor
    public class ChangeToVendorCommandHandler : BaseHandler, IRequestHandler<ChangeToVendorCommandRequest,ChangeToVendorCommandResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserManager<User> userManager;
        private readonly UserRules userRules;
        private readonly RoleManager<Role> roleManager;

        public ChangeToVendorCommandHandler(IUnitOfWork unitOfWork,
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


        public async Task<ChangeToVendorCommandResponse> Handle(ChangeToVendorCommandRequest request, CancellationToken cancellationToken)
        {
            LoggerHelper.LogInformation("Handling ChangeToVendorCommandRequest for Email: {Email}", request.Email);


            //commented as this is checked in controller level.
            /*var userId = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (userId == null)
            {
                LoggerHelper.LogError("User is not authenticated.", new UnauthorizedAccessException("User is not authenticated."));
                throw new UnauthorizedAccessException("User is not authenticated.");
            }

            // Find the user and check if they are in the Admin role
            var userAdmin = await userManager.FindByIdAsync(userId);
            if (userAdmin == null || !(await userManager.IsInRoleAsync(userAdmin, "Admin")))
            {
                LoggerHelper.LogError("User does not have the required Admin role.", new UnauthorizedAccessException("User doesnot meet required Role."));
                throw new UnauthorizedAccessException("User does not have the required Admin role.");
            }
           */
            var user = await userManager.FindByEmailAsync(request.Email);

            if (user == null)
            {
                LoggerHelper.LogError("Unable to find User with Email: {Email}", new Exception("User with specified email doesnot exist"), request.Email);
                throw new Exception("User with specified Id does not exist");
            }
            try
            {
                if (!await roleManager.RoleExistsAsync("Vendor"))
                {
                    await roleManager.CreateAsync(new Role
                    {
                        Id = Guid.NewGuid(),
                        Name = "Vendor",
                        NormalizedName = "VENDOR",
                        ConcurrencyStamp = Guid.NewGuid().ToString(),
                    });
                    await userManager.AddToRoleAsync(user, "VENDOR");
                    await userManager.UpdateAsync(user);
                }
                //Remove all current roles
                var currentRole = await userManager.GetRolesAsync(user);
                if (currentRole == null)
                {
                    currentRole = new List<string>();
                }

                foreach (var role in currentRole)
                {
                    await userManager.RemoveFromRoleAsync(user, role);
                }

                await userManager.AddToRoleAsync(user, "VENDOR");
                await userManager.UpdateAsync(user);

                LoggerHelper.LogInformation("User with Email: {Email} successfully changed to Vendor role.", request.Email);
            }
            catch (Exception ex)
            {
                LoggerHelper.LogError("Error occurred while changing user to Vendor role for Email: {Email}",ex, request.Email);
                throw; // Re-throw the exception after logging
            }
            
            return new()
            {
                MessageToReturn = user.Id.ToString(),
            };
        }
    }
}
