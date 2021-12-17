using Domain.Contracts;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities.Partners
{
    public class Partner : AuditableEntity<Guid>
    {
        public Guid UserId { get; set; }
        public string Name { get; set; }

        [Column(TypeName = "nvarchar(20)")]
        public PartnerType Type { get; set; }
        public string Description { get; set; }
        public string RegistrationNo { get; set; }
        public DateTime? RegistrationDate { get; set; }
        public string TaxNo { get; set; }
        public bool IsVerified { get; set; } 

        public List<Address> Addresses { get; set; }
        public List<BankAccount> BankAccounts { get; set; }
        public List<Contact> Contacts { get; set; }
        public List<Vehicle> Vehicles { get; set; }

    }
}
