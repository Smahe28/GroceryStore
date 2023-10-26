using SMG_DAL;
using SMG_Interface;
using SMG_Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMG_BLL
{
    public class StockBLL
    {
        public static IStock GetStockDetails()
        {
            return new StockRepository();
        }
        public static IStock RegisterGoods(ImportGoodsModel ImportGoods)
        {
            return new StockRepository();
        }
        public static IStock GetOrderGoods()
        {
            return new StockRepository();
        }
        public static IStock RegisterBuyGoods(ImportGoodsModel ImportGoods)
        {
            return new StockRepository();
        }
        public static IStock GetAllDealers()
        {
            return new StockRepository();
        }
        public static IStock GetBuyGoodsdetails(int DealerID)
        {
            return new StockRepository();
        }
    }
}
