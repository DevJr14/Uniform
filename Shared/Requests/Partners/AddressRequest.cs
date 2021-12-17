using System;

namespace Shared.Requests.Partners
{
    public class AddressRequest
    {
        public Guid Id { get; set; }
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
