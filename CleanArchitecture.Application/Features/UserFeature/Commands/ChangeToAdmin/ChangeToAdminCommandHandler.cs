using CleanArchitecture.Application.Bases;
using CleanArchitecture.Application.Features.UserFeature.Rules;
using CleanArchitecture.Application.Helpers;
using CleanArchitecture.Application.Interfaces.AutoMapper;
using CleanArchitecture.Application.Interfaces.UnitOfWorks;
using CleanArchitecture.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.Features.UserFeature.Commands.ChangeToAdmin
{
    public class ChangeToAdminCommandHandler : BaseHandler,IRequestHandler<ChangeToAdminCommandRequest,ChangeToAdminCommandResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserManager<User> userManager;
        private readonly UserRules userRules;
        private readonly RoleManager<Role> roleManager;

        public ChangeToAdminCommandHandler(IUnitOfWork unitOfWork,
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
        public async Task<ChangeToAdminCommandResponse> Handle(ChangeToAdminCommandRequest request,CancellationToken cancellationToken)
        {
            LoggerHelper.LogInformation("Handling ChangeToAdminCommandRequest for Email: {Email}", request.Email);

            var user = await userManager.FindByEmailAsync(request.Email);

            if(user == null)
            {
                LoggerHelper.LogError("Unable to find User with Email: {Email}",
                    new Exception("User with specified Email does not exist"), request.Email);
                throw new Exception("User with specified Id does not exist");
            }

            try
            {
                //remove current role
                var currentRole = await userManager.GetRolesAsync(user);
                if(currentRole == null)
                {
                    currentRole = new List<string>();
                }
                foreach(var role in currentRole)
                {
                    await userManager.RemoveFromRoleAsync(user, role);
                }
                //add new role
                await userManager.AddToRoleAsync(user, "Admin");
                await userManager.UpdateAsync(user);
                LogHelper.LogInformation("User wth Email: {Email} successfully changed to Admin role", request.Email);
            }
            catch (Exception ex)
            {
                LoggerHelper.LogError("Error occurred while changing user to User role for Email: {Email}", ex, request.Email);
                throw;
            }

            return new()
            {
                MessagToReturn = user.Id.ToString()
            };
        }
    }
}
