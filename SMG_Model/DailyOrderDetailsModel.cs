using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMG_Model
{
    public class DailyOrderDetailsModel
    {
        public int OrderDetailsId { get; set; }
        public string OrderNumber { get; set; }
        public int ItemId { get; set; }
        public string ItemName { get; set; }
        public decimal UnitPrice { get; set; }
        public int Quantity { get; set; }
        public decimal Discount { get; set; }
        public decimal Total { get; set; }

        public DailyOrderModel OrderInfo = new DailyOrderModel();
        public ProductModel product = new ProductModel();
    }
}
