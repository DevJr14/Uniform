using System;

namespace SharedR.Responses.Partners
{
    public class ContactResponse
    {
        public Guid PartnerId { get; set; }
        public string Title { get; set; }
        public string CellphoneNo { get; set; }
        public string TelephoneNo { get; set; }
        public string Email { get; set; }
        public bool IsActive { get; set; }
    }
}
