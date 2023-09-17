using System;

namespace Persistence
{
    public class Sneakers
    {
        public string NameSneaker {set; get;}
        public int Quantity {set; get;}
        public string Brand {set; get;}
        public string Color {set; get;}
        public string Origin {set; get;}
        public string Material {set; get;}
        public string StatusSneaker {set; get;}
        public string ProductCode {set; get;}
        public int Price { set; get;}
        public string Size {set; get;}

        public Sneakers(string namesneaker, int quantity, string brand, string color,string origin, string material, string status,string productCode, int price, string size)
        {
            this.NameSneaker = namesneaker;
            this.Quantity = quantity;
            this.Brand = brand;
            this.Color = color;
            this.Origin = origin;
            this.Material = material;
            this.StatusSneaker = status;
            this.ProductCode = productCode;
            this.Price = price;
            this.Size = size;
        }
    }
}