using SharedR.Responses.Partners;
using System;

namespace SharedR.Responses.Catalogs
{
    public class TagResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Guid PartnerId { get; set; }
        public PartnerResponse Partner { get; set; } = new();
    }
}
