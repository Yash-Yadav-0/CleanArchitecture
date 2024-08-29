using CleanArchitecture.Domain.Entities;

namespace CleanArchitecture.Infrastructure.Tokens.FlexibleAuth
{
    public static class PolicyNameHelper
    {
        public const string Prefix = "Permissions_";

        public static bool IsValidPolicyName(string? policyName)
        {
            return policyName != null && policyName.StartsWith(Prefix, System.StringComparison.OrdinalIgnoreCase);
        }

        public static string GeneratePolicyNameFor(Permissions permissions)
        {
            return $"{Prefix}{(int)permissions}";
        }

        public static Permissions GetPermissionsFrom(string policyName)
        {
            var permissionValue = int.Parse(policyName.Split('_')[1]);
            return (Permissions)permissionValue;
        }
    }
}
