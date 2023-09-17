using System;

namespace Persistence
{
    public class Customer
    {
        public int  IDCustomer { set; get; }
        public string NameCustomer { set; get; }
        public string PhoneCustomer { set; get; }
        public string Address { set; get; }
  

        public Customer
        ( int idCustomer,string nameCustomer, string phoneCustomer, string address)
        {
            this.IDCustomer = idCustomer;
            this.NameCustomer = nameCustomer;
            this.PhoneCustomer = phoneCustomer;
            this.Address = address;
        }
    }
}