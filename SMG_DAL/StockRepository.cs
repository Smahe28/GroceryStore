using Database;
using SMG_Interface;
using SMG_Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace SMG_DAL
{
    public class StockRepository : IStock
    {
        public List<StockModel> GetStockDetails()
        {
            List<StockModel> l_Stock = new List<StockModel>();
            StockModel l_data = null;
            DataTable tblProduct = new DataTable();
            Connection l_con = new Connection();
            using (SqlConnection sqlCon = l_con.MyConnection())
            {
                sqlCon.Open();
                string query = "select s.ProductId,p.ProductName,s.c_Stock,s.l_StockAdd from Stock_Table s join Products_Table p on p.ProductId=s.ProductId;";
                SqlDataAdapter sqlDa = new SqlDataAdapter(query, sqlCon);
                sqlDa.Fill(tblProduct);
                foreach (DataRow l_row in tblProduct.Rows)
                {
                    l_data = new StockModel();
                    l_data.ProductId = Convert.ToInt32(l_row["ProductId"].ToString());
                    l_data.ProductName = l_row["ProductName"].ToString();
                    l_data.CurrentStock = Convert.ToInt32(l_row["c_Stock"].ToString());
                    l_data.LastStockAdd = Convert.ToInt32(l_row["l_StockAdd"].ToString());
                    l_Stock.Add(l_data);
                }
            }
            return l_Stock;
        }
        public string RegisterGoods(ImportGoodsModel ImportGoods)
        {
            Connection l_con = new Connection();
            using (SqlConnection sqlCon = l_con.MyConnection())
            {
                sqlCon.Open();

                string query = "insert into Stock_Arrival (bill_No,deliveryBy,arrival_from,Goods_Arrival_date,vehicle_no,GoodsCost) values(@bill_No,@deliveryBy,@from,@date,@vehicle_no,@GoodsCost);";
                SqlCommand sqlcmd = new SqlCommand(query, sqlCon);

                //sqlcmd.Parameters.AddWithValue("@OrderId", objOrderModel.OrderId);
                sqlcmd.Parameters.AddWithValue("@bill_No", ImportGoods.StockArrival.BillNo);
                sqlcmd.Parameters.AddWithValue("@deliveryBy", ImportGoods.StockArrival.DeliveryBy);
                sqlcmd.Parameters.AddWithValue("@from", ImportGoods.StockArrival.DeliveryFrom);
                sqlcmd.Parameters.AddWithValue("@date", ImportGoods.StockArrival.DeliveryDate);
                sqlcmd.Parameters.AddWithValue("@vehicle_no", ImportGoods.StockArrival.vehicleNo);
                sqlcmd.Parameters.AddWithValue("@GoodsCost", ImportGoods.StockArrival.GoodsCost);
                sqlcmd.ExecuteNonQuery();

                foreach (var item in ImportGoods.ProductDetails)
                {

                    string l_query = "insert into Stock_Arrival_Details (bill_No,ProductId,Stock_unit,Import_Cost) values(@bill_No,@ProductId,@Stock_unit,@Import_Cost);"
                        + "update Stock_Table set c_Stock=c_Stock+@Stock_unit,l_StockAdd=@Stock_unit where ProductId=@ProductId;";
                    SqlCommand l_sqlcmd = new SqlCommand(l_query, sqlCon);
                    l_sqlcmd.Parameters.AddWithValue("@bill_No", ImportGoods.StockArrival.BillNo);
                    l_sqlcmd.Parameters.AddWithValue("@ProductId", item.ProductId);
                    l_sqlcmd.Parameters.AddWithValue("@Stock_unit", item.StockUnit);
                    l_sqlcmd.Parameters.AddWithValue("@Import_Cost", item.ImportCost);
                    l_sqlcmd.ExecuteNonQuery();
                }
            }
            return "Goods has been Succesfully registered.";
        }
        public List<StockModel> GetOrderGoods()
        {
            List<StockModel> l_Stock = new List<StockModel>();
            StockModel l_data = null;
            DataTable tblProduct = new DataTable();
            Connection l_con = new Connection();
            using (SqlConnection sqlCon = l_con.MyConnection())
            {
                sqlCon.Open();
                string query = "select CategoryId,P.ProductId,ProductName,S.c_Stock from Products_Table P join Stock_Table S on s.ProductId=P.ProductId";
                SqlDataAdapter sqlDa = new SqlDataAdapter(query, sqlCon);
                sqlDa.Fill(tblProduct);
                foreach (DataRow l_row in tblProduct.Rows)
                {
                    l_data = new StockModel();
                    l_data.CategoryId = Convert.ToInt32(l_row["CategoryId"].ToString());
                    l_data.ProductId = Convert.ToInt32(l_row["ProductId"].ToString());
                    l_data.ProductName = l_row["ProductName"].ToString();
                    l_data.CurrentStock = Convert.ToInt32(l_row["c_Stock"].ToString());
                    l_Stock.Add(l_data);
                }
            }
            return l_Stock;
        }
        public string RegisterBuyGoods(ImportGoodsModel ImportGoods)
        {
            Connection l_con = new Connection();
            using (SqlConnection sqlCon = l_con.MyConnection())
            {
                sqlCon.Open();

                string query = "insert into OrderBuyGoods (DealerName,City,OrderDate,EndDate) values(@deliveryBy,@from,@ODate,@EDate);";
                SqlCommand sqlcmd = new SqlCommand(query, sqlCon);

                sqlcmd.Parameters.AddWithValue("@deliveryBy", ImportGoods.StockArrival.DeliveryBy);
                sqlcmd.Parameters.AddWithValue("@from", ImportGoods.StockArrival.DeliveryFrom);
                sqlcmd.Parameters.AddWithValue("@ODate", DateTime.Now);
                sqlcmd.Parameters.AddWithValue("@EDate", DateTime.Now.AddDays(5));
                sqlcmd.ExecuteNonQuery();
                DataTable tblProduct = new DataTable();
                string s_query = "select top 1 ID from OrderBuyGoods order by  ID desc";
                SqlDataAdapter sqlDa = new SqlDataAdapter(s_query, sqlCon);
                sqlDa.Fill(tblProduct);
                if (tblProduct.Rows.Count > 0)
                {
                    string RegisterID = tblProduct.Rows[0]["ID"].ToString();

                    foreach (var item in ImportGoods.ProductDetails)
                    {
                        string l_query = "insert into OrderBuyGoodsDetails(DealerID,ProductId,Unit) values(@bill_No,@ProductId,@Stock_unit);";
                        SqlCommand l_sqlcmd = new SqlCommand(l_query, sqlCon);
                        l_sqlcmd.Parameters.AddWithValue("@bill_No", RegisterID);
                        l_sqlcmd.Parameters.AddWithValue("@ProductId", item.ProductId);
                        l_sqlcmd.Parameters.AddWithValue("@Stock_unit", item.StockUnit);
                        l_sqlcmd.ExecuteNonQuery();
                    }
                }

            }
            return "Goods has been Succesfully registered.";
        }
        public List<SelectListItem> GetAllDealers()
        {
            List<SelectListItem> de_data = new List<SelectListItem>();
            Connection l_con = new Connection();
            using (SqlConnection Con = l_con.MyConnection())
            {
                Con.Open();
                string l_Query = "select ID,DealerName from OrderBuyGoods";
                SqlCommand cmd = new SqlCommand(l_Query, Con);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    de_data.Add(new SelectListItem
                    {
                        Value = dr["ID"].ToString(),
                        Text = dr["DealerName"].ToString(),
                    });
                }
                Con.Close();
            }
            return de_data;
        }

        public BuyGoodsdetailsModel GetBuyGoodsdetails(int DealerID)
        {
            BuyGoodsdetailsModel l_data = new BuyGoodsdetailsModel();
            OrderBuyGoods l_dealer = new OrderBuyGoods();
            List<OrderBuyGoodsDetails> p_list = new List<OrderBuyGoodsDetails>();
            OrderBuyGoodsDetails l_product = null;
            DataSet tblSet = new DataSet();
            Connection l_con = new Connection();
            using (SqlConnection sqlCon = l_con.MyConnection())
            {
                sqlCon.Open();
                string query = "select ID,City from OrderBuyGoods where ID=@DId;" +
                                "select DealerID,O.ProductId,P.ProductName,Unit from OrderBuyGoodsDetails O join Products_Table P on P.ProductId=O.ProductId where DealerID=@DId; ";
                SqlDataAdapter sqlDa = new SqlDataAdapter(query, sqlCon);
                sqlDa.SelectCommand.Parameters.AddWithValue("@DId", DealerID);
                sqlDa.Fill(tblSet);
                if (tblSet != null)
                {
                    if(tblSet.Tables[0].Rows.Count>0)
                    {
                        l_dealer.City = tblSet.Tables[0].Rows[0]["City"].ToString();
                        l_data.Dealer = l_dealer;
                    }
                    foreach (DataRow l_row in tblSet.Tables[1].Rows)
                    {
                        l_product = new OrderBuyGoodsDetails();
                        l_product.ProductID = Convert.ToInt32(l_row["ProductId"].ToString());
                        l_product.ProductName = l_row["ProductName"].ToString();
                        l_product.Unite = Convert.ToInt32(l_row["Unit"].ToString());
                        p_list.Add(l_product);
                    }
                }
                l_data.Products = p_list;
            }
            return l_data;
        }
    }
}
