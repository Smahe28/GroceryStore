using SMG_Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace SMG_Interface
{
    public interface IStock
    {
        List<StockModel> GetStockDetails();
        string RegisterGoods(ImportGoodsModel ImportGoods);
        List<StockModel> GetOrderGoods();
        string RegisterBuyGoods(ImportGoodsModel ImportGoods);
        List<SelectListItem> GetAllDealers();
        BuyGoodsdetailsModel GetBuyGoodsdetails(int DealerID);
    }
}
