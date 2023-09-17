using System;
using System.Text;
using MySqlConnector;
using Persistence;

namespace DAL
{
    public class LoginDb
    {
        public static MySqlConnection conn = DbConfig.GetDefaultConnection();
        public bool CheckAccount(string username)
        {
            try
            {
                conn.Open();
                string query = "SELECT * FROM Account_Shop WHERE User_name = @username";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@username", username);
                MySqlDataReader dr = cmd.ExecuteReader();
                bool isAdmin = false;

                if (dr.Read())
                {
                    string CheckUS = dr.GetString("User_name");
                    string check = CheckUS.Substring(0, 6);

                    if (check == "seller")
                    {
                        isAdmin = true;
                    }
                }

                return isAdmin;
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR: " + ex.Message);
                return false;
            }
            finally
            {
                conn.Close();
            }
        }

        public bool LoginAccount(string username, string password)
        {
            try
            {
                conn.Open();
                string pwHashed = CreateMD5(password);

                string query = "SELECT * FROM Account_Shop WHERE User_name = @username AND Passcode = @pwHashed";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@username", username);
                cmd.Parameters.AddWithValue("@pwHashed", pwHashed);

                MySqlDataReader dr = cmd.ExecuteReader();

                if (dr.Read())
                {
                    conn.Close();
                    return true;
                }
                else
                {
                    Console.WriteLine("\u001b[31mInvalid username or password, please re-enter!\u001b[0m");
                    conn.Close();
                    return false;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR: " + ex.Message);
                return false;
            }
            finally
            {
                conn.Close();
            }
        }

        public bool GetPassword(string password)
        {
            try
            {
                conn.Open();
                string pwHashed = CreateMD5(password);
                string query = "SELECT * FROM Account_Shop WHERE Passcode = @pwHashed";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@pwHashed", pwHashed);

                MySqlDataReader dr = cmd.ExecuteReader();

                return dr.Read(); 
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR: " + ex.Message);
                return false;
            }
            finally
            {
                conn.Close();
            }
        }

        public string CreateMD5(string input)
        {
            using (System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create())
            {
                byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);
                byte[] hashBytes = md5.ComputeHash(inputBytes);
                return Convert.ToHexString(hashBytes);
            }
        }
        public List<Account> GetNameStaff(string Username)
        {
            List<Account> account = new List<Account>();
            try
            {
                conn.Open();
                string query = "SELECT * FROM Account_Shop WHERE User_name = @Username";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Username", Username);

                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        string UserName = reader.GetString("User_name");
                        string Passcode = reader.GetString("Passcode");
                        string NameStaff = reader.GetString("NameStaff");
                        string StatusStaff = reader.GetString("StatusStaff");

                        Account CustomerInfor = new Account(UserName, Passcode, NameStaff, StatusStaff);
                        account.Add(CustomerInfor);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("EROR: " + ex.Message);
            }
            finally
            {
                conn.Close();
            }

            return account;
        }
    }
}