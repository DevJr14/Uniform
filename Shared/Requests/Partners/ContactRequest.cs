using System;

namespace Shared.Requests.Partners
{
    public class ContactRequest
    {
        public Guid Id { get; set; }
        public Guid PartnerId { get; set; }
        public string Title { get; set; }
        public string CellphoneNo { get; set; }
        public string TelephoneNo { get; set; }
        public string Email { get; set; }
        public bool IsActive { get; set; }
    }
}
