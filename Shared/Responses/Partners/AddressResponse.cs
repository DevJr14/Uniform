using System;

namespace SharedR.Responses.Partners
{
    public class AddressResponse
    {
        public Guid PartnerId { get; set; }
        public string StreetName { get; set; }
        public string Suburb { get; set; }
        public string City { get; set; }
        public string Province { get; set; }
        public string Country { get; set; }
        public string PostalCode { get; set; }
        public bool IsActive { get; set; }
    }
}
