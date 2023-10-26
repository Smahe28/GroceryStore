using SMG_BLL;
using SMG_Interface;
using SMG_Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Web.Mvc;

namespace GroceryStore.Controllers
{
    public class StockController : Controller
    {
        public ActionResult Index()
        {
            IProduct l_product = ProductBLL.GetCategory();
            ViewBag.Category = l_product.GetCategory();
            return View();
        }
        [HttpGet]
        public JsonResult GetStockDetails()
        {
            IStock l_stock = StockBLL.GetStockDetails();
            List<StockModel> l_data = l_stock.GetStockDetails();
            return Json(new { data = l_data }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult AddStock()
        {
            IProduct l_product = ProductBLL.GetCategory();
            ViewBag.Category = l_product.GetCategory();
            IStock l_stock = StockBLL.GetAllDealers();
            ViewBag.Dealers = l_stock.GetAllDealers();
            return View();
        }
        public JsonResult RegisterGoods(ImportGoodsModel ImportGoods)
        {
            IStock l_stock = StockBLL.RegisterGoods(ImportGoods);
            string status = l_stock.RegisterGoods(ImportGoods);
            return Json(status, JsonRequestBehavior.AllowGet);
        }
        public ActionResult ImportGoodsDetails()
        {
            return View();
        }
        public ActionResult BuyGoods()
        {
            IProduct l_product = ProductBLL.GetCategory();
            ViewBag.Category = l_product.GetCategory();
            return View();
        }
        public JsonResult GetOrderGoods()
        {
            IStock l_stock = StockBLL.GetOrderGoods();
            List<StockModel> l_list = l_stock.GetOrderGoods();
            return Json(l_list, JsonRequestBehavior.AllowGet);
        }
        public JsonResult RegisterBuyGoods(ImportGoodsModel ImportGoods)
        {
            IStock l_stock = StockBLL.RegisterBuyGoods(ImportGoods);
            string status = l_stock.RegisterBuyGoods(ImportGoods);
            return Json(status, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetBuyGoodsdetails(int DealerID)
        {
            IStock l_stock = StockBLL.GetBuyGoodsdetails(DealerID);
            BuyGoodsdetailsModel data = l_stock.GetBuyGoodsdetails(DealerID);
            return Json(data, JsonRequestBehavior.AllowGet);
        }
    }
}