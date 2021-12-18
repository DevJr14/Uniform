using System.ComponentModel.DataAnnotations;

namespace SharedR.Requests.Identity
{
    public class ForgotPasswordRequest
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
