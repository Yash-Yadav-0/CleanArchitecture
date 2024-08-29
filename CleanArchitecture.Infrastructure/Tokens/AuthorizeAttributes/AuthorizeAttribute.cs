using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Infrastructure.Tokens.FlexibleAuth;

namespace CleanArchitecture.Infrastructure.Tokens.AuthorizeAttributes
{
    public class AuthorizeAttribute :  Microsoft.AspNetCore.Authorization.AuthorizeAttribute
    {
        public AuthorizeAttribute() { }
        public AuthorizeAttribute(string policy) : base(policy) { }

        public AuthorizeAttribute(Permissions permissions)
        {
            Permissions = permissions;
        }
        public Permissions Permissions
        {
            get =>
                !string.IsNullOrEmpty(Policy)
                ? PolicyNameHelper.GetPermissionsFrom(Policy)
                : Permissions.None;
            set =>
                Policy = value != Permissions.None
                    ? PolicyNameHelper.GeneratePolicyNameFor(value)
                    : string.Empty;
        }
    }
}
