using DAL;
using Persistence;
using MySqlConnector;

namespace BL
{
    public class ItemBL
    {
        List<Sneakers> ProductList = new List<Sneakers>();
        public static Item ProductListDb = new Item();
        public List<Sneakers> GetAllProduct()
        {
            ProductList = ProductListDb.GetAllProduct();
            return ProductList;
        }
        public List<Sneakers> GetInforProducts(string productCode)
        {
            ProductList = ProductListDb.GetInforProducts(productCode);
            return ProductList;
        }
        public bool CheckProduct(string productCode)
        {
            return ProductListDb.CheckCodeProduct(productCode);
        }
        public bool CheckPhoneCustomerBL(string phone)
        {
            return ProductListDb.CheckPhoneCustomer(phone);
        }
        public List<Customer> GetInforWithPhoneBL(string phone)
        {
            return ProductListDb.GetInforWithPhone(phone);
        }
    }
}