using SMG_Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMG_Interface
{
    public interface IBilling
    {
        string SaveOrders(DailyOrderModel objDailyOrder);
        string SaveWholeSaleOrder(DailyOrderModel objDailyOrder);
        List<DailyOrderModel> OrderListView();
        List<DailyOrderDetailsModel> OrderDetails(string id);
        List<DailyOrderModel> RetailPayments();
        List<DailyOrderDetailsModel> RetailOrderDetails(string id);
        List<DailyOrderModel> WholeSalePayments();
        WholeSaleModel GetWholesaleDetails(string WCId);
        string WholeSalePayment(WholeSalePaymentModel objWSPayment);
    }
}
