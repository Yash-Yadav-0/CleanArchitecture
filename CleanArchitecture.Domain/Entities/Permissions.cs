namespace CleanArchitecture.Domain.Entities
{
    [Flags]
    public enum Permissions
    {
        None = 0,
        ViewRoles = 1,
        ManageRoles = 2,
        ViewUsers = 4,
        ManageUsers = 8,
        ManageProducts = 16,
        ViewOrder = 32,
        ManageOrder = 64,
        ViewPermissions = 128,
        ManagePermissions = 256,
        All = ~None,
    }
    public static class PermissionProvider
    {
        public static IEnumerable<Permissions> GetAll(bool includeNone = false, bool includeAll = false)
        {
            var values = Enum.GetValues(typeof(Permissions))
                .OfType<Permissions>()
                .ToList();

            if (!includeNone)
                values.Remove(Permissions.None);
            if (!includeAll)
                values.Remove(Permissions.All);

            return values;
        }
    }
}
