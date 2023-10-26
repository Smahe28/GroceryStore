using Database;
using SMG_Interface;
using SMG_Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace SMG_DAL
{
    public class ProductRepository : IProduct
    {
        public List<SelectListItem> GetCategory()
        {
            List<SelectListItem> cat_data = new List<SelectListItem>();
            Connection l_con = new Connection();
            using (SqlConnection Con = l_con.MyConnection())
            {
                Con.Open();
                string l_Query = "select categoryId,CategoryName from Category_Table";
                SqlCommand cmd = new SqlCommand(l_Query, Con);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    cat_data.Add(new SelectListItem
                    {
                        Value = dr["categoryId"].ToString(),
                        Text = dr["CategoryName"].ToString(),

                    });
                }
                Con.Close();

            }
            return cat_data;
        }
        public List<ProductModel> GetAllProduct()
        {
            List<ProductModel> l_Products = new List<ProductModel>();
            DataTable tblProduct = new DataTable();
            Connection l_con = new Connection();
            using (SqlConnection sqlCon = l_con.MyConnection())
            {
                sqlCon.Open();
                string query = "select ProductId,ProductName,Price from Products_Table";
                SqlDataAdapter sqlDa = new SqlDataAdapter(query, sqlCon);
                sqlDa.Fill(tblProduct);
                foreach (DataRow l_row in tblProduct.Rows)
                {
                    ProductModel p_data = new ProductModel();
                    p_data.ProductId = Convert.ToInt32(l_row["ProductId"].ToString());
                    p_data.ProductName = l_row["ProductName"].ToString();
                    p_data.unitPrice = l_row["Price"].ToString();
                    l_Products.Add(p_data);
                }
            }
            return l_Products;
        }
        public string SaveProduct(ProductModel Prt_Data)
        {
            int prtId = Prt_Data.ProductId;
            string result = "";
            Connection l_con = new Connection();
            using (SqlConnection sqlCon = l_con.MyConnection())
            {
                sqlCon.Open();
                if (prtId == 0)
                {
                    string query = "insert into Products_Table(CategoryId,ProductName,Price) values(@CategoryId,@ProductName,@Price);" +
                                    "insert into Stock_Table (ProductId,c_Stock) select top 1 ProductId,0 from Products_Table order by ProductId desc;";
                    SqlCommand sqlcmd = new SqlCommand(query, sqlCon);
                    sqlcmd.Parameters.AddWithValue("@CategoryId", Prt_Data.CategoryId);
                    sqlcmd.Parameters.AddWithValue("@ProductName", Prt_Data.ProductName);
                    sqlcmd.Parameters.AddWithValue("@Price", Prt_Data.unitPrice);
                    sqlcmd.ExecuteNonQuery();
                    result = "Product Saved Succesfully";
                }
                else
                {
                    string query = "update Products_Table set CategoryId=@CategoryId,ProductName=@ProductName,Price=@Price where ProductId=" + prtId;
                    SqlCommand sqlcmd = new SqlCommand(query, sqlCon);
                    sqlcmd.Parameters.AddWithValue("@CategoryId", Prt_Data.CategoryId);
                    sqlcmd.Parameters.AddWithValue("@ProductName", Prt_Data.ProductName);
                    sqlcmd.Parameters.AddWithValue("@Price", Prt_Data.unitPrice);
                    sqlcmd.ExecuteNonQuery();
                    result = "Product Updated Succesfully";
                }
            }
            return result;
        }
        public ProductModel GetEditData(int prdId)
        {
            ProductModel p_data = new ProductModel();
            DataTable tblProduct = new DataTable();
            Connection l_con = new Connection();
            using (SqlConnection sqlCon = l_con.MyConnection())
            {
                sqlCon.Open();
                string query = "select ProductId,CategoryId,ProductName,Price from Products_Table where ProductId =" + prdId;
                SqlDataAdapter sqlDa = new SqlDataAdapter(query, sqlCon);
                sqlDa.Fill(tblProduct);
                if(tblProduct.Rows.Count>0)
                {
                    p_data.ProductId = Convert.ToInt32(tblProduct.Rows[0]["ProductId"].ToString());
                    p_data.CategoryId = Convert.ToInt32(tblProduct.Rows[0]["CategoryId"].ToString());
                    p_data.ProductName = tblProduct.Rows[0]["ProductName"].ToString();
                    p_data.unitPrice = tblProduct.Rows[0]["Price"].ToString();
                }
            }
            return p_data;

        }
        public List<ProductModel> GetProducts(int CategoryId)
        {
            List<ProductModel> l_Products = new List<ProductModel>();
            DataTable tblProduct = new DataTable();
            Connection l_con = new Connection();
            using (SqlConnection sqlCon = l_con.MyConnection())
            {
                sqlCon.Open();
                string query = "select ProductId,ProductName from Products_Table where CategoryId=" + CategoryId;
                SqlDataAdapter sqlDa = new SqlDataAdapter(query, sqlCon);
                sqlDa.Fill(tblProduct);
                foreach (DataRow l_row in tblProduct.Rows)
                {
                    ProductModel p_data = new ProductModel();
                    p_data.ProductId = Convert.ToInt32(l_row["ProductId"].ToString());
                    p_data.ProductName = l_row["ProductName"].ToString();
                    l_Products.Add(p_data);
                }
            }
            return l_Products;
        }
        public ProductModel GetItemUnitPrice(int ProductId)
        {
            ProductModel l_product = new ProductModel();
            Connection l_con = new Connection();
            using (SqlConnection sqlCon = l_con.MyConnection())
            {
                sqlCon.Open();
                DataTable tbl_UtPrice = new DataTable();
                string l_query = "select c_Stock from Stock_Table where ProductId=" + ProductId;
                SqlDataAdapter da = new SqlDataAdapter(l_query, sqlCon);
                da.Fill(tbl_UtPrice);
                l_product.CStock = Convert.ToInt32(tbl_UtPrice.Rows[0]["c_Stock"].ToString());
                if (l_product.CStock > 0)
                {
                    tbl_UtPrice = new DataTable();
                    string query = "select Price from Products_Table where ProductId=" + ProductId;
                    SqlDataAdapter sqlDa = new SqlDataAdapter(query, sqlCon);
                    sqlDa.Fill(tbl_UtPrice);
                    if (tbl_UtPrice != null)
                    {
                        l_product.unitPrice = tbl_UtPrice.Rows[0]["Price"].ToString();
                    }
                }
            }
            return l_product;

        }
        public MenuModel Menu()
        {
            MenuModel l_collection = new MenuModel();
            List<Product_category> l_Data = new List<Product_category>();
            Connection l_con = new Connection();
            DataTable tblProduct = new DataTable();
            using (SqlConnection sqlCon = l_con.MyConnection())
            {
                sqlCon.Open();
                string query = "select CategoryId,ProductName,Price from Products_Table";
                SqlDataAdapter sqlDa = new SqlDataAdapter(query, sqlCon);
                sqlDa.Fill(tblProduct);
                foreach (DataRow l_row in tblProduct.Rows)
                {
                    Product_category listdata = new Product_category();
                    listdata.product.CategoryId = Convert.ToInt32(l_row["CategoryId"].ToString());
                    listdata.product.ProductName = l_row["ProductName"].ToString();
                    listdata.product.unitPrice = l_row["Price"].ToString();
                    l_Data.Add(listdata);

                }
                sqlCon.Close();
            }
            l_collection.l_Product = l_Data;
            List<Product_category> c_Data = new List<Product_category>();
            DataTable tblcategory = new DataTable();
            using (SqlConnection sqlCon = l_con.MyConnection())
            {
                sqlCon.Open();
                string query = "select categoryId,CategoryName from Category_Table";
                SqlDataAdapter sqlDa = new SqlDataAdapter(query, sqlCon);
                sqlDa.Fill(tblcategory);
                foreach (DataRow l_row in tblcategory.Rows)
                {
                    Product_category listdata = new Product_category();
                    listdata.category.CategoryId = Convert.ToInt32(l_row["categoryId"].ToString());
                    listdata.category.CategoryName = l_row["CategoryName"].ToString();
                    c_Data.Add(listdata);

                }
                sqlCon.Close();
            }
            l_collection.l_Category = c_Data;
            return l_collection;
        }
    }
}
