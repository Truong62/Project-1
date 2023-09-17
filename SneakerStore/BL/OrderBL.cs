using System;
using DAL;
using Persistence;

namespace BL
{
    public class OrderBL
    {
        public void CreateOrderBL(List<OrdersDetails> ordersdetails, string nameCustomer, string phone, string address, string StaffSeller)
        {
            CreateOrderDb.CreateOrder(ordersdetails, nameCustomer, phone, address, StaffSeller);
        }
    }
}