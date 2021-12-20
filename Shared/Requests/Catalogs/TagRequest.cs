using System;

namespace SharedR.Requests.Catalogs
{
    public class TagRequest
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Guid PartnerId { get; set; }
    }
}
