using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMG_Model
{
    public class WholeSalePaymentModel
    {
        public String OrderNumber { get; set; }
        public String PaymentType { get; set; }
        public int Amount { get; set; }
        public DateTime PayDate { get; set; }
    }
}
