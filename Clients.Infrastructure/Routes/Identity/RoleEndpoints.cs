namespace Clients.Infrastructure.Routes.Identity
{
    public static class RoleEndpoints
    {
        public static string Delete = "api/v1/identity/role";
        public static string GetAll = "api/v1/identity/role/get-all";
        public static string Save = "api/v1/identity/role/save";
        public static string GetPermissions = "api/v1/identity/role/permissions/";
        public static string UpdatePermissions = "api/v1/identity/role/permissions/update";
    }
}
