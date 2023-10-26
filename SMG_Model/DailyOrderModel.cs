using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMG_Model
{
    public class DailyOrderModel
    {
        public int OrderId { get; set; }
        public string PaymentType { get; set; }
        public string CustomerName { get; set; }
        public string Location { get; set; }
        public string OrderNumber { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal FinalTotal { get; set; }
        public string PayStatus { get; set; }
        public IEnumerable<DailyOrderDetailsModel> ListOfOrderDetailsModel { get; set; }
    }
}
