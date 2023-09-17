using Persistence;
using MySqlConnector;
namespace DAL
{
    public class Item
    {
        private static MySqlConnection conn = DbConfig.GetDefaultConnection();
        public List<Sneakers> GetAllProduct()
        {
            List<Sneakers> ProductList = new List<Sneakers>();

            try
            {
                conn.Open();
                string query = $"SELECT * FROM Sneaker;";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    string NameSneaker = reader.GetString("Name_sneaker");
                    int Quantity = reader.GetInt32("Quantity");
                    string BrandPR = reader.GetString("Brand");
                    string Color = reader.GetString("Color");
                    string Origin = reader.GetString("Origin");
                    string Material = reader.GetString("Material");
                    string StatusSneaker = reader.GetString("Status_Sneaker");
                    string ProductCode = reader.GetString("Product_Code");
                    int Price = reader.GetInt32("Price");
                    string Size = reader.GetString("Size");
                    Sneakers infopr = new Sneakers(NameSneaker, Quantity, BrandPR, Color, Origin, Material, StatusSneaker, ProductCode, Price, Size);
                    ProductList.Add(infopr);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: " + ex.Message);
            }
            finally
            {
                conn.Close();
            }
            return ProductList;
        }

        public List<Sneakers> GetInforProducts(string productCode)
        {
            List<Sneakers> ProductInfor = new List<Sneakers>();

            try
            {
                conn.Open();
                string query = $"SELECT * FROM Sneaker where Product_Code = @productCode;";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@productCode", productCode);

                MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    string NameSneaker = reader.GetString("Name_sneaker");
                    int Quantity = reader.GetInt32("Quantity");
                    string BrandPR = reader.GetString("Brand");
                    string Color = reader.GetString("Color");
                    string Origin = reader.GetString("Origin");
                    string Material = reader.GetString("Material");
                    string StatusSneaker = reader.GetString("Status_Sneaker");
                    string ProductCode = reader.GetString("Product_Code");
                    int Price = reader.GetInt32("Price");
                    string Size = reader.GetString("Size");

                    Sneakers ProductInfor1 = new Sneakers(NameSneaker, Quantity, BrandPR, Color, Origin, Material, StatusSneaker, ProductCode, Price, Size);
                    ProductInfor.Add(ProductInfor1);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: " + ex.Message);
            }
            finally
            {
                conn.Close();
            }

            return ProductInfor;
        }
        public bool CheckCodeProduct(string productCode)
        {
            bool exists = false;

            try
            {
                conn.Open();
                string query = $"SELECT * FROM Sneaker where Product_Code = @productCode;";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@productCode", productCode);

                MySqlDataReader dr = cmd.ExecuteReader();
                exists = dr.Read();
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: " + ex.Message);
            }
            finally
            {
                conn.Close();
            }

            return exists;
        }
        public bool CheckPhoneCustomer(string phone)
        {
            bool exists = false;

            try
            {
                conn.Open();
                string query = $"SELECT * FROM Customer where Phone = @phone;";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@phone", phone);

                MySqlDataReader dr = cmd.ExecuteReader();
                exists = dr.Read();
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: " + ex.Message);
            }
            finally
            {
                conn.Close();
            }

            return exists;
        }
        public List<Customer> GetInforWithPhone(string phone)
        {
            List<Customer> CustomerList = new List<Customer>();

            try
            {
                conn.Open();
                string query = $"SELECT * FROM Customer where Phone = @phone;";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@phone", phone);

                MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    int ID_Customer = reader.GetInt32("ID_Customer");
                    string Name_Customer = reader.GetString("Name_Customer");
                    string Address = reader.GetString("Address");
                    string Phone = reader.GetString("Phone");

                    Customer CustomerInfor1 = new Customer(ID_Customer, Name_Customer, Phone, Address);
                    CustomerList.Add(CustomerInfor1);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: " + ex.Message);
            }
            finally
            {
                conn.Close();
            }

            return CustomerList;
        }

    }
}