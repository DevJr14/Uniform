using System.ComponentModel.DataAnnotations;

namespace SharedR.Requests.Identity
{
    public class UpdateProfileRequest
    {
        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public bool IsPartner { get; set; }
    }
}
