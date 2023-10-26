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
    public class ProductBLL
    {
        public static IProduct GetCategory()
        {
            return new ProductRepository();
        }
        public static IProduct GetAllProduct()
        {
            return new ProductRepository();
        }
        public static IProduct SaveProduct(ProductModel Prt_Data)
        {
            return new ProductRepository();
        }
        public static IProduct GetEditData(int prdId)
        {
            return new ProductRepository();
        }
        public static IProduct GetProducts(int CategoryId)
        {
            return new ProductRepository();
        }
        public static IProduct GetItemUnitPrice(int ProductId)
        {
            return new ProductRepository();
        }
        public static IProduct Menu()
        {
            return new ProductRepository();
        }
    }
}
