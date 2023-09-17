using System;
using DAL;
using Persistence;

namespace BL
{
    public class ProcessingorderBL
    {
        List<Orders> CustomerList = new List<Orders>();
        public static ProcessingorderDAL PayMentDb = new ProcessingorderDAL();
        public List<Orders> GetOrderListDayBL()
        {
            CustomerList = PayMentDb.GetOrderListDay();
            return CustomerList;
        }
        public List<OrdersDetails> GetOrderDetailbyID(string idorder){
            return PayMentDb.GetOrderDetailbyID(idorder);
        }
        public bool CheckIdOrderBL(string idorder){
            return PayMentDb.CheckIdOrder(idorder);
        }
        public List<Orders> GetOrderByIdBL(string idorder){
            return PayMentDb.GetOrderById(idorder);
        }
    }
}