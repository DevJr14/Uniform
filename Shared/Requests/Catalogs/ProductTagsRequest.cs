using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedR.Requests.Catalogs
{
    public class ProductTagsRequest
    {
        public Guid Id { get; set; }
        public Guid ProductId { get; set; }
        public IEnumerable<Guid> TagIds { get; set; }
    }
}
