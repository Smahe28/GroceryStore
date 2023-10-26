using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMG_Model
{
    public class OrderBuyGoods
    {
        public int DealerID { get; set; }
        public string DealerName { get; set; }
        public string City { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime EndDate { get; set; }

    }
}
