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
    public class BillingBLL
    {
        public static IBilling SaveOrders(DailyOrderModel objDailyOrder)
        {
            return new BillingRepository();
        }
        public static IBilling SaveWholeSaleOrder(DailyOrderModel objWholeSaleOrder)
        {
            return new BillingRepository();
        }
        public static IBilling OrderListView()
        {
            return new BillingRepository();
        }
        public static IBilling OrderDetails(string id)
        {
            return new BillingRepository();
        }
        public static IBilling RetailPayments()
        {
            return new BillingRepository();
        }
        public static IBilling RetailOrderDetails(string id)
        {
            return new BillingRepository();
        }
        public static IBilling WholeSalePayments()
        {
            return new BillingRepository();
        }
        public static IBilling GetWholesaleDetails(string WCId)
        {
            return new BillingRepository();
        }
        public static IBilling WholeSalePayment(WholeSalePaymentModel objWSPayment)
        {
            return new BillingRepository();
        }

    }
}
