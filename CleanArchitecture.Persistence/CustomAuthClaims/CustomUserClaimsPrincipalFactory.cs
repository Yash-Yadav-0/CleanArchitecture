using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Persistence.CustomAuthClaims;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Persistence.CustomAuthClaims
{
    public class CustomUserClaimsPrincipalFactory : UserClaimsPrincipalFactory<User,Role>
    {
        public CustomUserClaimsPrincipalFactory(
            UserManager<User> userManager,
            RoleManager<Role> roleManager,
            IOptions<IdentityOptions> options
            )
            :base( userManager, roleManager, options)
        {

        }
        protected override async Task<ClaimsIdentity> GenerateClaimsAsync(User user)
        {
            var identity = await base.GenerateClaimsAsync(user);
            var userRoleNames = await UserManager.GetRolesAsync(user) ?? Array.Empty<string>();
            var userRoles = await RoleManager.Roles
                .Where(r => userRoleNames.Contains(r.Name))
                .ToListAsync();
            var userPermissions = Permissions.None;
            foreach (var role in userRoles)
            {
                userPermissions |= role.Permissions;
            }
            var permissionsValue = (int)userPermissions;
            identity.AddClaim(new Claim(CustomClaimTypes.Permissions,permissionsValue.ToString()));

            return identity;
        }
    }
}
