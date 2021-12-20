using System;

namespace SharedR.Requests.Catalogs
{
    public class BrandRequest
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Guid PartnerId { get; set; }
    }
}
