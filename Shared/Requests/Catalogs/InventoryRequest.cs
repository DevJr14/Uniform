using System;

namespace SharedR.Requests.Catalogs
{
    public class InventoryRequest
    {
        public Guid Id { get; set; } 
        public Guid ProductId { get; set; }
        public int StockQty { get; set; }
        public int AvailableQty { get; set; }
        public bool DisplayQty { get; set; }
        public int MinCartQty { get; set; }
        public int MaxCartQty { get; set; }
        public int NotifyQtyBelow { get; set; }
        public bool IsReturnable { get; set; }        
    }
}
