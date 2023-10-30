using SMG_BLL;
using SMG_Interface;
using SMG_Model;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace GroceryStore.Controllers
{
    public class BillingController : Controller
    {
        // GET: Billing
        public ActionResult Index()
        {
            IProduct l_product = ProductBLL.GetCategory();
            ViewBag.Category = l_product.GetCategory();
            return View();
        }
        [HttpGet]
        public ActionResult GetProducts(int CategoryId)
        {
            IProduct l_product = ProductBLL.GetProducts(CategoryId);
            List<ProductModel> l_Products = l_product.GetProducts(CategoryId);
            return Json(l_Products, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetItemUnitPrice(int ProductId)
        {
            IProduct l_product = ProductBLL.GetItemUnitPrice(ProductId);
            ProductModel data = l_product.GetItemUnitPrice(ProductId);
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        public JsonResult SaveOrders(DailyOrderModel objDailyOrder)
        {
            string SaveID = "abc";
            IBilling l_bill = BillingBLL.SaveOrders(objDailyOrder);
            string status = l_bill.SaveOrders(objDailyOrder);
            return Json(status, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Orders()
        {
            IProduct l_product = ProductBLL.GetCategory();
            ViewBag.Category = l_product.GetCategory();
            return View();
        }
        public JsonResult SaveWholeSaleOrder(DailyOrderModel objWholeSaleOrder)
        {
            IBilling l_bill = BillingBLL.SaveWholeSaleOrder(objWholeSaleOrder);
            string status = l_bill.SaveWholeSaleOrder(objWholeSaleOrder);
            return Json(status, JsonRequestBehavior.AllowGet);
        }
        public ActionResult OrderListView()
        {
            IBilling l_bill = BillingBLL.OrderListView();
            List<DailyOrderModel> l_Products = l_bill.OrderListView();
            return View(l_Products);
        }
        public ActionResult OrderDetails(string id)
        {
            IBilling l_bill = BillingBLL.OrderDetails(id);
            List<DailyOrderDetailsModel> l_Products = l_bill.OrderDetails(id);
            return View(l_Products);
        }
        public ActionResult RetailPayments()
        {
            IBilling l_bill = BillingBLL.RetailPayments();
            List<DailyOrderModel> l_Products = l_bill.RetailPayments();
            return View(l_Products);
        }
        public ActionResult RetailOrderDetails(string id)
        {
            IBilling l_bill = BillingBLL.RetailOrderDetails(id);
            List<DailyOrderDetailsModel> l_Products = l_bill.RetailOrderDetails(id);
            return View(l_Products);
        }
        public ActionResult WholeSalePayments()
        {
            IBilling l_bill = BillingBLL.WholeSalePayments();
            List<DailyOrderModel> l_Products = l_bill.WholeSalePayments();
            return View(l_Products);
        }
        public JsonResult GetWholesaleDetails(string WCId)
        {
            IBilling l_bill = BillingBLL.GetWholesaleDetails(WCId);
            WholeSaleModel data = l_bill.GetWholesaleDetails(WCId);
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult WholeSalePayment(WholeSalePaymentModel objWSPayment)
        {
            IBilling l_bill = BillingBLL.WholeSalePayment(objWSPayment);
            string status = l_bill.WholeSalePayment(objWSPayment);
            return Json(status, JsonRequestBehavior.AllowGet);
        }

    }
}