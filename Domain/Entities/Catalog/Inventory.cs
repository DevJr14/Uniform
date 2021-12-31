using Domain.Contracts;
using System;

namespace Domain.Entities.Catalog
{
    public class Inventory : AuditableEntity<Guid>
    {
        public Guid ProductId { get; set; }
        public int StockQty { get; set; }
        public int AvailableQty { get; set; }
        public bool DisplayQty { get; set; }
        public int MinCartQty { get; set; }
        public int MaxCartQty { get; set; }
        public int NotifyQtyBelow { get; set; }
        public bool IsReturnable { get; set; }
        public Product Product { get; set; }
    }
}
