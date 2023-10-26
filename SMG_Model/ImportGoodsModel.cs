using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMG_Model
{
    public class ImportGoodsModel
    {
        public StockArrivalModel StockArrival { get; set; }
        public IEnumerable<StockModel> ProductDetails { get; set; }
    }
}
