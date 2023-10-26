using SMG_BLL;
using SMG_Interface;
using SMG_Model;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace GroceryStore.Controllers
{
    public class ProductController : Controller
    {
        // GET: Product
        public ActionResult Index()
        {
            IProduct l_product = ProductBLL.GetCategory();
            ViewBag.Category = l_product.GetCategory();
            return View();
        }
        [HttpPost]
        public ActionResult SaveProduct(ProductModel Prt_Data)
        {
            IProduct l_product = ProductBLL.SaveProduct(Prt_Data);
            string status = l_product.SaveProduct(Prt_Data);
            return Json(status, JsonRequestBehavior.AllowGet); ;
        }
        [HttpGet]
        public JsonResult GetAllProduct()
        {
            IProduct l_product = ProductBLL.GetAllProduct();
            List<ProductModel> l_Products = l_product.GetAllProduct();
            return Json(new { data = l_Products }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetEditData(int prdId)
        {
            IProduct l_product = ProductBLL.GetEditData(prdId);
            ProductModel p_data = l_product.GetEditData(prdId);
            return Json(p_data, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Menu()
        {
            IProduct l_collection = ProductBLL.Menu();
            MenuModel l_Data = l_collection.Menu();
            ViewBag.Product = l_Data.l_Product;
            ViewBag.CateData = l_Data.l_Category;
            return View(ViewBag);
        }
    }
}