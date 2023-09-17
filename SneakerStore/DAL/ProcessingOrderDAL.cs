using System;
using MySqlConnector;
using Persistence;

namespace DAL
{
    public class ProcessingorderDAL
    {
        public static MySqlConnection connect = DbConfig.GetDBConnection();
        public List<Orders> GetOrderListDay()
        {
            List<Orders> Customerlist = new List<Orders>();

            try
            {
                connect.Open();
                string query = @"SELECT o.OrderID,o.StaffSeller,o.Order_Time , c.Name_Customer, SUM(od.Quantity) AS TotalQuantity, SUM(s.Price * od.Quantity) AS TotalPrice
FROM Orders o
INNER JOIN Customer c ON o.CustomerID = c.ID_Customer
INNER JOIN OrderDetails od ON o.OrderID = od.OrderID
INNER JOIN Sneaker s ON od.ProductID = s.Product_Code
WHERE DATE(o.Order_Time) = CURDATE() AND o.Status_Order = 'Pending'
GROUP BY o.OrderID, c.Name_Customer;";

                MySqlCommand cmd = new MySqlCommand(query, connect);
                MySqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    int OrderID = dr.GetInt32("OrderID");
                    string NameSeller = dr.GetString("StaffSeller");
                    string Name_Customer = dr.GetString("Name_Customer");
                    DateTime Order_Time = dr.GetDateTime("Order_Time");
                    int TotalQuantity = dr.GetInt32("TotalQuantity");
                    int TotalPrice = dr.GetInt32("TotalPrice");
                    Orders CsList = new Orders(OrderID, NameSeller, Name_Customer, Order_Time, TotalQuantity, TotalPrice);
                    Customerlist.Add(CsList);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: " + ex.Message);
            }
            finally
            {
                connect.Close();
            }
            return Customerlist;
        }

        public List<OrdersDetails> GetOrderDetailbyID(string idorder)
        {
            List<OrdersDetails> ListOrdersDetails = new List<OrdersDetails>();

            try
            {
                connect.Open();
                string query = @$"SELECT od.ProductID,c.Name_Customer,c.Address,c.Phone,o.OrderID AS Product_Code, s.Name_sneaker, od.Quantity, od.Size, s.Price
FROM OrderDetails od
INNER JOIN Sneaker s ON od.ProductID = s.Product_Code
INNER JOIN Orders o ON od.OrderID = o.OrderID
inner join Customer c on o.CustomerID = c.ID_Customer
WHERE DATE(o.Order_Time) = CURDATE() AND o.Status_Order = 'Pending' and  o.OrderID = @idorder;";

                MySqlCommand cmd = new MySqlCommand(query, connect);
                cmd.Parameters.AddWithValue("@idorder", idorder);

                MySqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    string ProductID = dr.GetString("ProductID");
                    string NameProduct = dr.GetString("Name_sneaker");
                    int Quantity = dr.GetInt32("Quantity");
                    string Size = dr.GetString("Size");
                    int Price = dr.GetInt32("Price");
                    string NameCustomer = dr.GetString("Name_Customer");
                    string AddressCustomer = dr.GetString("Address");
                    string PhoneCustomer = dr.GetString("Phone");
                    int IDorder = dr.GetInt32("Product_Code");
                    OrdersDetails ordersDetails = new OrdersDetails(ProductID, IDorder, NameProduct, Quantity, Size, Price, NameCustomer, AddressCustomer, PhoneCustomer);
                    ListOrdersDetails.Add(ordersDetails);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: " + ex.Message);
            }
            finally
            {
                connect.Close();
            }

            return ListOrdersDetails;
        }

        public bool CheckIdOrder(string idorder)
        {
            bool isValid = false;

            try
            {
                connect.Open();
                string query = $"SELECT * FROM Orders where date(Order_Time) = date(current_timestamp()) and Status_Order = 'Pending' and OrderID = @idorder;";
                MySqlCommand cmd = new MySqlCommand(query, connect);
                cmd.Parameters.AddWithValue("@idorder", idorder);

                MySqlDataReader dr = cmd.ExecuteReader();
                isValid = dr.Read();
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: " + ex.Message);
            }
            finally
            {
                connect.Close();
            }
            return isValid;
        }

        public List<Orders> GetOrderById(string idorder){
            List<Orders> Customerlist = new List<Orders>();
            try
            {
                connect.Open();
                // DateTime now = DateTime.Now;
                string query = @$"SELECT o.OrderID,o.StaffSeller,o.Order_Time , c.Name_Customer, SUM(od.Quantity) AS TotalQuantity, SUM(s.Price * od.Quantity) AS TotalPrice
FROM Orders o
INNER JOIN Customer c ON o.CustomerID = c.ID_Customer
INNER JOIN OrderDetails od ON o.OrderID = od.OrderID
INNER JOIN Sneaker s ON od.ProductID = s.Product_Code
WHERE DATE(o.Order_Time) = CURDATE() AND o.Status_Order = 'Pending' and o.OrderID = @idorder
GROUP BY o.OrderID, c.Name_Customer;";
                MySqlCommand cmd = new MySqlCommand(query, connect);
                cmd.Parameters.AddWithValue("@idorder", idorder);
                MySqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    int OrderID = dr.GetInt32("OrderID");
                    string NameSeller = dr.GetString("StaffSeller");
                    string Name_Customer = dr.GetString("Name_Customer");
                    DateTime Order_Time = dr.GetDateTime("Order_Time");
                    int TotalQuantity = dr.GetInt32("TotalQuantity");
                    int TotalPrice = dr.GetInt32("TotalPrice");
                    Orders CsList = new Orders(OrderID, NameSeller, Name_Customer, Order_Time, TotalQuantity, TotalPrice);
                    Customerlist.Add(CsList);
                }
            }
            catch(Exception ex){
                Console.WriteLine("An error occurred: " + ex.Message);
            }
            finally
            {
                connect.Close();
            }
            return Customerlist;
        }
        public void UpdateStatusOrder(string idorder, string statusorder)
        {
            try
            {
                connect.Open();
                string query = "UPDATE Orders SET Status_Order = @statusorder WHERE OrderID = @idorder";
                MySqlCommand cmd = new MySqlCommand(query, connect);
                cmd.Parameters.AddWithValue("@statusorder", statusorder);
                cmd.Parameters.AddWithValue("@idorder", idorder);

                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: " + ex.Message);
            }
            finally
            {
                connect.Close();
            }
        }

    }
}
