using Domain.Contracts;
using System;

namespace Domain.Entities.Partners
{
    public class BankAccount : AuditableEntity<Guid>
    {
        public Guid PartnerId { get; set; }
        public string BankName { get; set; }
        public string AccountType { get; set; }
        public string AccountNo { get; set; }
        public DateTime ExpiryDate { get; set; }
        public int CVV { get; set; }
        public bool IsActive { get; set; }
        public Partner Partner { get; set; }
    }
}
