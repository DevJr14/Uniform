using System;

namespace SharedR.Requests.Catalogs
{
    public class CategoryRequest
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
        public Guid PartnerId { get; set; }
    }
}
