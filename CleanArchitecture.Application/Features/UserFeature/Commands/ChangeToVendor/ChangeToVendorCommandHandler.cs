using CleanArchitecture.Application.Bases;
using CleanArchitecture.Application.Features.UserFeature.Rules;
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
            var userId = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (userId == null)
            {
                throw new UnauthorizedAccessException("User is not authenticated.");
            }

            // Find the user and check if they are in the Admin role
            var userAdmin = await userManager.FindByIdAsync(userId);
            if (userAdmin == null || !(await userManager.IsInRoleAsync(userAdmin, "ADMIN")))
            {
                throw new UnauthorizedAccessException("User does not have the required Admin role.");
            }
            //await userRules.UserShouldnotBeExistsAsync(await userManager.FindByEmailAsync(request.Email));
            User? user = await userManager.FindByEmailAsync(request.Email);

            if (user == null)
            {
                Log.Error("Unable to find User {@Id}", request.Email);
                throw new Exception("User with specified Id does not exist");
            }
            if (await roleManager.RoleExistsAsync("USER"))
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
            return new()
            {
                MessageToReturn = UserId.ToString(),
            };
        }
    }
}
