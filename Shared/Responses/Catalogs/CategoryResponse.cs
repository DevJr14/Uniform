using SharedR.Responses.Partners;
using System;

namespace SharedR.Responses.Catalogs
{
    public class CategoryResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
        public Guid PartnerId { get; set; }
        public PartnerResponse Partner { get; set; } = new();
    }
}
