using System;
using System.Collections.Generic;

namespace SharedR.Requests.Catalogs
{
    public class ProductCategoriesRequest
    {
        public Guid Id { get; set; }
        public Guid ProductId { get; set; }
        public List<Guid> CategoryIds { get; set; }
    }
}
