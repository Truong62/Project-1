using System;
using MySqlConnector;
using Persistence;

namespace DAL
{
    public class CreateOrderDb
    {
        private static MySqlConnection conn = DbConfig.GetDefaultConnection();
        public static void CreateOrder(List<OrdersDetails> ordersDetails, string nameCustomer, string phone, string address, string StaffSeller)
        {
            conn.Open();

            using (MySqlTransaction transaction = conn.BeginTransaction())
            {
                int customerID = 0;
                string GetInforWithPhone = $"SELECT * FROM Customer where Phone = @phone";
                MySqlCommand cmd = new MySqlCommand(GetInforWithPhone, conn);
                cmd.Parameters.AddWithValue("@phone", phone);
                MySqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    customerID = reader.GetInt32("ID_Customer");
                    reader.Close();
                }
                else
                {
                    reader.Close();
                    string insertCustomerQuery = "INSERT INTO Customer (Name_Customer, Address, Phone) VALUES (@Name, @Address, @Phone)";
                    MySqlCommand insertCustomerCmd = new MySqlCommand(insertCustomerQuery, conn);
                    insertCustomerCmd.Parameters.AddWithValue("@Name", $"{nameCustomer}");
                    insertCustomerCmd.Parameters.AddWithValue("@Address", $"{address}");
                    insertCustomerCmd.Parameters.AddWithValue("@Phone", $"{phone}");
                    insertCustomerCmd.ExecuteNonQuery();
                    customerID = (int)insertCustomerCmd.LastInsertedId;
                }


                string insertOrdersQuery = "INSERT INTO Orders (CustomerID, Order_Time, Status_Order, StaffSeller) VALUES (@CustomerID, @OrderTime, @Status, @Staff)";
                MySqlCommand insertOrdersCmd = new MySqlCommand(insertOrdersQuery, conn);
                insertOrdersCmd.Parameters.AddWithValue("@CustomerID", customerID);
                insertOrdersCmd.Parameters.AddWithValue("@OrderTime", DateTime.Now);
                insertOrdersCmd.Parameters.AddWithValue("@Status", "Pending");
                insertOrdersCmd.Parameters.AddWithValue("@Staff", $"{StaffSeller}");
                insertOrdersCmd.ExecuteNonQuery();
                try
                {
                    int orderID = (int)insertOrdersCmd.LastInsertedId;
                    foreach (OrdersDetails item in ordersDetails)
                    {
                        string insertOrderDetailsQuery = "INSERT INTO OrderDetails (OrderID, ProductID, Quantity, Size) VALUES (@OrderID, @ProductID, @Quantity, @Size)";
                        MySqlCommand insertOrderDetailsCmd = new MySqlCommand(insertOrderDetailsQuery, conn);
                        insertOrderDetailsCmd.Parameters.AddWithValue("@OrderID", orderID);
                        insertOrderDetailsCmd.Parameters.AddWithValue("@ProductID", item.ProductCode);
                        insertOrderDetailsCmd.Parameters.AddWithValue("@Quantity", item.Quantity);
                        insertOrderDetailsCmd.Parameters.AddWithValue("@Size", item.Size);
                        insertOrderDetailsCmd.ExecuteNonQuery();

                    }
                    transaction.Commit();

                    Console.WriteLine("\u001b[31mSuccessful order creation... \u001b[0m");
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    Console.WriteLine("\u001b[31mError occurred: " + ex.Message + "\u001b[0m");
                }
                finally
                {
                    conn.Close();
                }
            }
        }
    }
}
