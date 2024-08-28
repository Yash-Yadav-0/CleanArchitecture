using CleanArchitecture.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
