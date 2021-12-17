namespace Clients.Infrastructure.Routes.Identity
{
    public static class TokenEndpoints
    {
        public static string Get = "api/v1/auth/token/get-token";
        public static string Refresh = "api/v1/identity/token/get-refresh-token";
    }
}
