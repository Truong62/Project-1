using System;

namespace Persistence
{
    public class OrdersDetails
    {
        public string ProductCode {set; get;}
        public int IDorder { set; get; }
        public string NameProduct { set; get; }
        public int Quantity { set; get; }
        public string Size { set; get; }
        public int Price { set; get; }
        public string NameCustomer { set; get; }
        public string AddressCustomer { set; get; }
        public string PhoneCustomer { set; get; }

        public OrdersDetails(string productCode,int idorder, string nameproduct, int quantity, string size, int price, string nameCustomer, string addressCustomer, string phoneCustomer)
        {
            this.ProductCode = productCode;
            this.IDorder = idorder;
            this.NameProduct = nameproduct;
            this.Quantity = quantity;
            this.Size = size;
            this.Price = price;
            this.NameCustomer = nameCustomer;
            this.AddressCustomer = addressCustomer;
            this.PhoneCustomer = phoneCustomer;
        }
    }
}