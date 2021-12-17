using System.ComponentModel;

namespace Domain.Enums
{
    public enum PartnerType
    {
        [Description("Seller")]
        Seller,

        [Description("Tailor")]
        Tailor,

        [Description("Deliver")]
        Deliver
    }
}
