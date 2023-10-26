using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMG_Model
{
    public class StockModel
    {
        public string BillNo { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public int CurrentStock { get; set; }
        public int LastStockAdd { get; set; }
        public int StockUnit { get; set; }
        public int ImportCost { get; set; }
        public int CategoryId { get; set; }
    }
}
