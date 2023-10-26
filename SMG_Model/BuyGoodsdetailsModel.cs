using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMG_Model
{
    public class BuyGoodsdetailsModel
    {
        public OrderBuyGoods Dealer { get; set; }
        public List<OrderBuyGoodsDetails> Products { get; set; }
    }
}
