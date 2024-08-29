using CleanArchitecture.Domain.Entities;
using Microsoft.AspNetCore.Authorization;

namespace CleanArchitecture.Infrastructure.Tokens.FlexibleAuth
{
    public class PermissionAuthorizationRequirement : IAuthorizationRequirement
    {
        public PermissionAuthorizationRequirement(Permissions permission)
        {
            Permissions = permission;
        }

        public Permissions Permissions { get; }
    }
}
