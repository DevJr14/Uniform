using System;

namespace Shared.Responses.Partners
{
    public class PartnerResponse
    {
        public Guid UserId { get; set; }
        public string Name { get; set; }
        //To Do: Change from enum to string
        public string Type { get; set; }
        public string Description { get; set; }
        public string RegistrationNo { get; set; }
        public DateTime? RegistrationDate { get; set; }
        public string TaxNo { get; set; }
        public bool IsVerified { get; set; }
    }
}
