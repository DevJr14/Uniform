using System;

namespace SharedR.Responses.Partners
{
    public class BankAccountResponse
    {
        public Guid Id { get; set; }
        public Guid PartnerId { get; set; }
        public string BankName { get; set; }
        public string AccountType { get; set; }
        public string AccountNo { get; set; }
        public DateTime ExpiryDate { get; set; }
        public int CVV { get; set; }
        public bool IsActive { get; set; }
    }
}
