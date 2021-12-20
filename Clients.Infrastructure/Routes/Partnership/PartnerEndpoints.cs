using System;

namespace Clients.Infrastructure.Routes.Partnership
{
    public static class PartnerEndpoints
    {
        public static string GetAll = "api/v1/partnership/partners/get-all";
        public static string Save = "api/v1/partnership/partners/add-edit";
        public static string Activate = "api/v1/partnership/partners/activate-de-activate?id=";
        public static string Delete = "api/v1/partnership/partners/delete?id=";

        public static string GetForUser(Guid userId)
        {
            return $"api/v1/partnership/partners/get-for-user?userid={userId}";
        }

        public static string GetById(Guid id)
        {
            return $"api/v1/partnership/partners/get-by-id?id={id}";
        }

        public static string GetByVerificationStatus(bool isVerified)
        {
            return $"api/v1/partnership/partners/get-by-verification-status?isVerified={isVerified}";
        }

    }
}
