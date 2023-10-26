using SMG_Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace SMG_Interface
{
    public interface IProduct
    {
        List<SelectListItem> GetCategory();
        List<ProductModel> GetAllProduct();
        string SaveProduct(ProductModel Prt_Data);
        ProductModel GetEditData(int prdId);
        List<ProductModel> GetProducts(int CategoryId);
        ProductModel GetItemUnitPrice(int ProductId);
        MenuModel Menu();
    }
}
