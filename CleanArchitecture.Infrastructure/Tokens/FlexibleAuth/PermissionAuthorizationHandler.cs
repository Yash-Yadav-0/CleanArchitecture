using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Infrastructure.Tokens.FlexibleAuth.CustomAuthClaims;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Infrastructure.Tokens.FlexibleAuth
{
    public class PermissionAuthorizationHandler : AuthorizationHandler<PermissionAuthorizationRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, PermissionAuthorizationRequirement requirement)
        {
            var permissionClaim = context.User.FindFirst(
                c => c.Type == CustomClaimTypes.Permissions);
            if (permissionClaim == null)
            {
                return Task.CompletedTask;
            }
            if (!int.TryParse(permissionClaim.Value, out int permissionClaimValue))
            {
                return Task.CompletedTask;
            }
            var userPermission = (Permissions)permissionClaimValue;
            if ((userPermission & requirement.Permissions) != 0)
            {
                context.Succeed(requirement);
                return Task.CompletedTask;
            }
            return Task.CompletedTask;
        }
    }
}
