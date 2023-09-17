using MySqlConnector;
// using System.Data.SQLite;
using System.IO;

namespace DAL
{
    public class DbConfig
    {
        private static MySqlConnection connection = new MySqlConnection();
        public DbConfig() { }
        public static MySqlConnection GetDefaultConnection()
        {
            connection.ConnectionString = "server=localhost;user id=thvtc;password=vtcacademy;port=3306;database=Sneaker_shop;IgnoreCommandTransaction=true";
            return connection;
        }
        public static MySqlConnection GetDBConnection()
        {

            MySqlConnection conn = new MySqlConnection("server=localhost;user id=thvtc;password=vtcacademy;port=3306;database=Sneaker_shop;IgnoreCommandTransaction=true");
            return conn;
        }

        // public void CreateAndInsertData()
        // {
        //     string connectionString = "Data Source=database.db;Version=3;";
        //     using (SQLiteConnection connection = new SQLiteConnection(connectionString))
        //     {

        //         connection.Open();

        //         Console.WriteLine("tesst");

        //         string filePath = @"..\Database.sql";

        //         string sqlScript = File.ReadAllText(filePath);
        //         using (SQLiteCommand createTableCommand = new SQLiteCommand(sqlScript, connection))
        //         {
        //             createTableCommand.ExecuteNonQuery();
        //         }
        //     }
        // }
    }
}