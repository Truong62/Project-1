using System;

namespace Persistence
{
    public class Orders
    {
        public int OrderID { set; get;}
        public string NameSeller{set;get;}
        public string Name_Customer { set; get;}
        public DateTime Order_Time { set; get;}
        public int TotalQuantity { set; get;}
        public int TotalPrice { set; get;}

        public Orders
        (int orderid, string  nameseller,string namecustomer, DateTime ordertime, int totalquantity, int totalprice)
        {
            this.OrderID = orderid;
            this.NameSeller= nameseller;
            this.Name_Customer = namecustomer;
            this.Order_Time = ordertime;
            this.TotalQuantity = totalquantity;
            this.TotalPrice = totalprice;
        }
    }
}