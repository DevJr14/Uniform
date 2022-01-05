namespace Clients.Infrastructure.Routes.Identity
{
    public static class UserEndpoints
    {
        public static string GetAll = "api/v1/identity/users/get-all";
        public static string Export = "api/v1/identity/users/export";
        public static string Register = "api/v1/identity/users";
        public static string ToggleUserStatus = "api/v1/identity/users/toggle-status";
        public static string ForgotPassword = "api/v1/identity/users/forgot-password";
        public static string ChangePassword = "api/v1/identity/users/change-password";
        public static string ResetPassword = "api/v1/identity/users/reset-password";
        public static string UpdateProfile = "api/v1/identity/users/update-profile";

        public static string Get(string userId)
        {
            return $"api/v1/identity/users/{userId}";
        }

        public static string GetUserRoles(string userId)
        {
            return $"api/v1/identity/users/roles/{userId}";
        }

        public static string ExportFiltered(string searchString)
        {
            return $"{Export}?searchString={searchString}";
        }

        public static string GetProfilePicture(string userId)
        {
            return $"api/v1/identity/users/get-profile-picture?userid={userId}";
        }

        public static string UpdateProfilePicture(string userId)
        {
            return $"api/v1/identity/users/update-profile-picture?userid={userId}";
        }

    }
}
