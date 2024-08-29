using CleanArchitecture.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.Helpers
{
    public class PermissionAuthorizeAttribute : AuthorizeAttribute,IAuthorizationFilter
    {
        private readonly Permissions _permissions;

        public PermissionAuthorizeAttribute(Permissions permissions)
        {
            _permissions = permissions;
        }
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var user = context.HttpContext.User;
            if (!user.Identity.IsAuthenticated)
            {
                context.Result = new UnauthorizedResult();
                return;
            }
            var userManager = context.HttpContext.RequestServices.GetService(typeof(UserManager<User>)) as UserManager<User>;
            var roleManager = context.HttpContext.RequestServices.GetService(typeof(RoleManager<Role>)) as RoleManager<Role>;

            var roles = userManager.GetRolesAsync(userManager.GetUserAsync(user).Result).Result;
            var userRoles = roleManager.Roles.Where(r => roles.Contains(r.Name)).ToList();

            bool hasPermission = userRoles.Any(role => (role.Permissions & _permissions) == _permissions);

            if (!hasPermission)
            {
                context.Result = new ForbidResult();
            }
        }
    }
}
