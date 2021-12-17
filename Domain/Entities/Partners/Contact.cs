using Domain.Contracts;
using System;

namespace Domain.Entities.Partners
{
    public class Contact : AuditableEntity<Guid>
    {
        public Guid PartnerId { get; set; }
        public string Title { get; set; }
        public string CellphoneNo { get; set; }
        public string TelephoneNo { get; set; }
        public string Email { get; set; }
        public bool IsActive { get; set; }
        public Partner Partner { get; set; }
    }
}
