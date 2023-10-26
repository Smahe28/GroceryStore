using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMG_Model
{
    public class StockArrivalModel
    {
        public string BillNo { get; set; }
        public string DeliveryBy { get; set; }
        public string DeliveryFrom { get; set; }
        public string DeliveryDate { get; set; }
        public string vehicleNo { get; set; }
        public int GoodsCost { get; set; }
    }
}
