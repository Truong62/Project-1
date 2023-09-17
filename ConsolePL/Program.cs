using System;
using System.Globalization;
namespace ConsolePL
{
    class Program
    {
        public static void Main(string[] args)
        {
            CultureInfo culture = new CultureInfo("en-US");
            culture.NumberFormat.NumberGroupSeparator = ".";
            culture.NumberFormat.NumberDecimalSeparator = ".";
            CultureInfo.DefaultThreadCurrentCulture = culture;
            // DbConfig dbManager = new DbConfig();
            // dbManager.CreateAndInsertData();
            do
            {
                StaffPL.Login();
                Console.Clear();
            } while (true);
        }
    }
}
