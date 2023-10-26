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

namespace SMG_DAL
{
    public class BillingRepository:IBilling
    {
        public string SaveOrders(DailyOrderModel objDailyOrder)
        {
            Connection l_con = new Connection();
            using (SqlConnection sqlCon = l_con.MyConnection())
            {
                sqlCon.Open();

                string query = "insert into DailyOrder (CustomerName,PaymentType,OrderNumber,OrderDate,TotalAmount) values( @CustomerName,@PaymentType,@OrderNumber,@OrderDate,@TotalAmount)";
                SqlCommand sqlcmd = new SqlCommand(query, sqlCon);

                //sqlcmd.Parameters.AddWithValue("@OrderId", objOrderModel.OrderId);
                sqlcmd.Parameters.AddWithValue("@CustomerName", objDailyOrder.CustomerName);
                sqlcmd.Parameters.AddWithValue("@PaymentType", objDailyOrder.PaymentType);
                sqlcmd.Parameters.AddWithValue("@OrderNumber", String.Format("{0:ddMMyyyyhhmmss}", DateTime.Now));
                sqlcmd.Parameters.AddWithValue("@OrderDate", DateTime.Now);
                sqlcmd.Parameters.AddWithValue("@TotalAmount", objDailyOrder.FinalTotal);
                sqlcmd.ExecuteNonQuery();

                DataTable tblOrderdata = new DataTable();
                string l_query = "SELECT top 1 OrderNumber FROM DailyOrder where CustomerName=@CustomerName ORDER BY OrderId DESC";
                SqlDataAdapter sqlDa = new SqlDataAdapter(l_query, sqlCon);
                sqlDa.SelectCommand.Parameters.AddWithValue("@CustomerName", objDailyOrder.CustomerName);
                sqlDa.Fill(tblOrderdata);
                if (tblOrderdata.Rows.Count > 0)
                {
                    string OrderNumber = tblOrderdata.Rows[0]["OrderNumber"].ToString();
                    foreach (var item in objDailyOrder.ListOfOrderDetailsModel)
                    {
                        string m_query = "insert into DailyOrderDetails(OrderNumber,ItemId,UnitPrice,Quantity,Discount,Total) values(@OrderNumber,@ItemId,@UnitPrice,@Quantity,@Discount,@Total)";

                        SqlCommand m_sqlcmd = new SqlCommand(m_query, sqlCon);
                        //sqlcmd.Parameters.AddWithValue("@OrderDetailsId",item.OrderDetailsId);
                        m_sqlcmd.Parameters.AddWithValue("@OrderNumber", OrderNumber);
                        m_sqlcmd.Parameters.AddWithValue("@ItemId", item.ItemId);
                        m_sqlcmd.Parameters.AddWithValue("@UnitPrice", item.UnitPrice);
                        m_sqlcmd.Parameters.AddWithValue("@Quantity", item.Quantity);
                        m_sqlcmd.Parameters.AddWithValue("@Discount", item.Discount);
                        m_sqlcmd.Parameters.AddWithValue("@Total", item.Total);
                        m_sqlcmd.ExecuteNonQuery();
                    }
                }
            }
            return "Order has been Succesfully Created.";
        }
        public string SaveWholeSaleOrder(DailyOrderModel objWholeSaleOrder)
        {
            Connection l_con = new Connection();
            using (SqlConnection sqlCon = l_con.MyConnection())
            {
                sqlCon.Open();

                string query = "insert into WholeSaleOrder (CustomerName,Location,OrderNumber,DeliveryDate) values( @CustomerName,@Location,@OrderNumber,@DeliveryDate)";
                SqlCommand sqlcmd = new SqlCommand(query, sqlCon);

                sqlcmd.Parameters.AddWithValue("@CustomerName", objWholeSaleOrder.CustomerName);
                sqlcmd.Parameters.AddWithValue("@Location", objWholeSaleOrder.Location);
                sqlcmd.Parameters.AddWithValue("@OrderNumber", String.Format("{0:ddMMyyyyhhmmss}", DateTime.Now));
                sqlcmd.Parameters.AddWithValue("@DeliveryDate", objWholeSaleOrder.OrderDate);
                sqlcmd.ExecuteNonQuery();

                DataTable tblOrderdata = new DataTable();
                string l_query = "SELECT top 1 OrderNumber FROM WholeSaleOrder where CustomerName=@CustomerName ORDER BY OrderId DESC";
                SqlDataAdapter sqlDa = new SqlDataAdapter(l_query, sqlCon);
                sqlDa.SelectCommand.Parameters.AddWithValue("@CustomerName", objWholeSaleOrder.CustomerName);
                sqlDa.Fill(tblOrderdata);
                if (tblOrderdata.Rows.Count > 0)
                {
                    string OrderNumber = tblOrderdata.Rows[0]["OrderNumber"].ToString();
                    foreach (var item in objWholeSaleOrder.ListOfOrderDetailsModel)
                    {

                        string m_query = "insert into WholeSaleOrderDetails(OrderNumber,ItemId,Quantity) values(@OrderNumber,@ItemId,@Quantity)";

                        SqlCommand m_sqlcmd = new SqlCommand(m_query, sqlCon);
                        //sqlcmd.Parameters.AddWithValue("@OrderDetailsId",item.OrderDetailsId);
                        m_sqlcmd.Parameters.AddWithValue("@OrderNumber", OrderNumber);
                        m_sqlcmd.Parameters.AddWithValue("@ItemId", item.ItemId);
                        m_sqlcmd.Parameters.AddWithValue("@Quantity", item.Quantity);
                        m_sqlcmd.ExecuteNonQuery();
                    }
                }
            }
            return "Your Order has been Succesfully Recorded.";
        }
        public List<DailyOrderModel> OrderListView()
        {
            List<DailyOrderModel> l_Products = new List<DailyOrderModel>();
            DataTable tblProduct = new DataTable();
            Connection l_con = new Connection();
            using (SqlConnection sqlCon = l_con.MyConnection())
            {
                sqlCon.Open();
                string query = "select CustomerName,Location,a.OrderNumber,DeliveryDate,ISNULL(b.OrderNumber,'Pending') as PayStatus from WholeSaleOrder a left join w_PaymentDetails b on a.OrderNumber=b.OrderNumber;";
                SqlDataAdapter sqlDa = new SqlDataAdapter(query, sqlCon);
                sqlDa.Fill(tblProduct);
                foreach (DataRow l_row in tblProduct.Rows)
                {
                    DailyOrderModel p_data = new DailyOrderModel();
                    p_data.CustomerName = l_row["CustomerName"].ToString();
                    p_data.Location = l_row["Location"].ToString();
                    p_data.OrderNumber = l_row["OrderNumber"].ToString();
                    p_data.OrderDate = Convert.ToDateTime(l_row["DeliveryDate"].ToString());
                    p_data.PayStatus = l_row["PayStatus"].ToString();

                    l_Products.Add(p_data);
                }
            }
            return l_Products;
        }
        public List<DailyOrderDetailsModel> OrderDetails(string id)
        {
            List<DailyOrderDetailsModel> l_Products = new List<DailyOrderDetailsModel>();
            DataTable tblProduct = new DataTable();
            Connection l_con = new Connection();
            using (SqlConnection sqlCon = l_con.MyConnection())
            {
                sqlCon.Open();
                string query = "select CustomerName,Location,b.ProductName,Quantity,Price from WholeSaleOrderDetails a join Products_Table b on b.ProductId=a.ItemId join WholeSaleOrder c on c.OrderNumber=a.OrderNumber where a.OrderNumber=@id";
                SqlDataAdapter sqlDa = new SqlDataAdapter(query, sqlCon);
                sqlDa.SelectCommand.Parameters.AddWithValue("@id", id);
                sqlDa.Fill(tblProduct);
                foreach (DataRow l_row in tblProduct.Rows)
                {
                    DailyOrderDetailsModel p_data = new DailyOrderDetailsModel();
                    p_data.OrderInfo.CustomerName = l_row["CustomerName"].ToString();
                    p_data.OrderInfo.Location = l_row["Location"].ToString();
                    p_data.ItemName = l_row["ProductName"].ToString();
                    p_data.Quantity = Convert.ToInt32(l_row["Quantity"].ToString());
                    p_data.Total = Convert.ToDecimal(l_row["Price"].ToString());
                    l_Products.Add(p_data);
                }
            }
            return l_Products;
        }
        public List<DailyOrderModel> RetailPayments()
        {
            List<DailyOrderModel> l_Products = new List<DailyOrderModel>();
            DataTable tblProduct = new DataTable();
            Connection l_con = new Connection();
            using (SqlConnection sqlCon = l_con.MyConnection())
            {
                sqlCon.Open();
                string query = "select CustomerName,OrderNumber,OrderDate,PaymentType,TotalAmount from DailyOrder";
                SqlDataAdapter sqlDa = new SqlDataAdapter(query, sqlCon);
                sqlDa.Fill(tblProduct);
                foreach (DataRow l_row in tblProduct.Rows)
                {
                    DailyOrderModel p_data = new DailyOrderModel();
                    p_data.CustomerName = l_row["CustomerName"].ToString();
                    p_data.OrderNumber = l_row["OrderNumber"].ToString();
                    p_data.OrderDate = Convert.ToDateTime(l_row["OrderDate"].ToString());
                    p_data.PaymentType = l_row["PaymentType"].ToString();
                    p_data.FinalTotal = Convert.ToDecimal(l_row["TotalAmount"].ToString());

                    l_Products.Add(p_data);
                }
            }
            return l_Products;
        }
        public List<DailyOrderDetailsModel> RetailOrderDetails(string id)
        {
            List<DailyOrderDetailsModel> l_Products = new List<DailyOrderDetailsModel>();
            DataTable tblProduct = new DataTable();
            Connection l_con = new Connection();
            using (SqlConnection sqlCon = l_con.MyConnection())
            {
                sqlCon.Open();
                string query = "select a.CustomerName,a.OrderNumber,a.OrderDate,a.TotalAmount,p.ProductName,b.Quantity,b.UnitPrice,b.Total from DailyOrder a join DailyOrderDetails b on a.OrderNumber=b.OrderNumber join Products_Table p on p.ProductId=b.ItemId where a.OrderNumber=@id";
                SqlDataAdapter sqlDa = new SqlDataAdapter(query, sqlCon);
                sqlDa.SelectCommand.Parameters.AddWithValue("@id", id);
                sqlDa.Fill(tblProduct);
                foreach (DataRow l_row in tblProduct.Rows)
                {
                    DailyOrderDetailsModel p_data = new DailyOrderDetailsModel();
                    p_data.OrderInfo.CustomerName = l_row["CustomerName"].ToString();
                    p_data.OrderInfo.OrderNumber = l_row["OrderNumber"].ToString();
                    p_data.OrderInfo.OrderDate = Convert.ToDateTime(l_row["OrderDate"].ToString());
                    p_data.OrderInfo.FinalTotal = Convert.ToInt32(l_row["TotalAmount"].ToString());
                    p_data.product.ProductName = l_row["ProductName"].ToString();
                    p_data.Quantity = Convert.ToInt32(l_row["Quantity"].ToString());
                    p_data.UnitPrice = Convert.ToInt32(l_row["UnitPrice"].ToString());
                    p_data.Total = Convert.ToInt32(l_row["Total"].ToString());

                    l_Products.Add(p_data);
                }
            }
            return l_Products;
        }
        public List<DailyOrderModel> WholeSalePayments()
        {
            List<DailyOrderModel> l_Products = new List<DailyOrderModel>();
            DataTable tblProduct = new DataTable();
            Connection l_con = new Connection();
            using (SqlConnection sqlCon = l_con.MyConnection())
            {
                sqlCon.Open();
                string query = "select CustomerName,OrderNumber,OrderDate,PaymentType,TotalAmount from DailyOrder";
                SqlDataAdapter sqlDa = new SqlDataAdapter(query, sqlCon);
                sqlDa.Fill(tblProduct);
                foreach (DataRow l_row in tblProduct.Rows)
                {
                    DailyOrderModel p_data = new DailyOrderModel();
                    p_data.CustomerName = l_row["CustomerName"].ToString();
                    p_data.OrderNumber = l_row["OrderNumber"].ToString();
                    p_data.OrderDate = Convert.ToDateTime(l_row["OrderDate"].ToString());
                    p_data.PaymentType = l_row["PaymentType"].ToString();
                    p_data.FinalTotal = Convert.ToDecimal(l_row["TotalAmount"].ToString());

                    l_Products.Add(p_data);
                }
            }
            return l_Products;
        }
        public WholeSaleModel GetWholesaleDetails(string WCId)
        {
            DataTable tbl_UtPrice = new DataTable();
            Connection l_con = new Connection();
            WholeSaleModel data = new WholeSaleModel();
            using (SqlConnection sqlCon = l_con.MyConnection())
            {
                sqlCon.Open();
                string query = "select c.CustomerName,c.OrderNumber,SUM(Convert(INT,b.Price)) as Price from WholeSaleOrderDetails a join Products_Table b on b.ProductId=a.ItemId join WholeSaleOrder c on c.OrderNumber=a.OrderNumber where c.OrderNumber = @id group by c.CustomerName,c.OrderNumber ";
                SqlDataAdapter sqlDa = new SqlDataAdapter(query, sqlCon);
                sqlDa.SelectCommand.Parameters.AddWithValue("@id", WCId);
                sqlDa.Fill(tbl_UtPrice);
                data.ShopName = tbl_UtPrice.Rows[0]["CustomerName"].ToString();
                data.OrderNumber = tbl_UtPrice.Rows[0]["OrderNumber"].ToString();
                data.TotalAmount = Convert.ToInt32(tbl_UtPrice.Rows[0]["Price"].ToString());
            }
            return data;
        }
        public string WholeSalePayment(WholeSalePaymentModel objWSPayment)
        {
            DataTable tblProduct = new DataTable();
            Connection l_con = new Connection();
            using (SqlConnection sqlCon = l_con.MyConnection())
            {
                sqlCon.Open();
                string query = "select * from w_PaymentDetails where OrderNumber=@id";
                SqlDataAdapter sqlDa = new SqlDataAdapter(query, sqlCon);
                sqlDa.SelectCommand.Parameters.AddWithValue("@id", objWSPayment.OrderNumber);
                sqlDa.Fill(tblProduct);
                if (tblProduct.Rows.Count == 0)
                {
                    string l_query = "insert into w_PaymentDetails (OrderNumber,PaymentType,Amount,PaymentDate) values( @OrderNumber,@PaymentType,@Amount,@PaymentDate)";
                    SqlCommand sqlcmd = new SqlCommand(l_query, sqlCon);

                    sqlcmd.Parameters.AddWithValue("@OrderNumber", objWSPayment.OrderNumber);
                    sqlcmd.Parameters.AddWithValue("@PaymentType", objWSPayment.PaymentType);
                    sqlcmd.Parameters.AddWithValue("@Amount", objWSPayment.Amount);
                    sqlcmd.Parameters.AddWithValue("@PaymentDate", DateTime.Now);
                    sqlcmd.ExecuteNonQuery();

                    return "Your Payment has been Succesfully Paied.";
                }
                else
                {
                    return "Your Payment has been already Paied.";
                }
            }
        }
    }
}
