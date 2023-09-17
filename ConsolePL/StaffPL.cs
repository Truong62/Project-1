using System;
using System.Text;
using BL;
using Spectre.Console;
using System.Text.RegularExpressions;

namespace ConsolePL
{
    public class StaffPL
    {
        public static void Login()
        {
            StaffBL loginBL = new StaffBL();
            string UserName = "";
            string PassWord = "";
            var table = new Table();
            table.AddColumns(new TableColumn(UiConsole.LogoLogin).Centered());
            table.AddRow("-[tan] LOGIN [/]-");
            table.BorderColor(Color.White);
            table.Border(TableBorder.Double);
            AnsiConsole.Write(table);
            do
            {
                Console.Write(" ->  User Name: ");
                UserName = Console.ReadLine() ?? "";
                Console.Write(" ->  Password: ");
                PassWord = GetPassword();
            } while (!(loginBL.loginBL(UserName, PassWord)));
            Console.Clear();
            bool CheckPassword = loginBL.CheckPassword(PassWord);
            if (loginBL.loginBL(UserName, PassWord))
            {
                bool checkUs;
                checkUs = loginBL.CheckUserBL(UserName);

                if (checkUs)
                {
                    if (CheckPassword)
                    {
                        int _Input;
                        do
                        {
                            _Input = MenuSeller(UserName);
                            switch (_Input)
                            {
                                case 1:
                                    Order.MenuSeller(UserName);
                                    break;
                                case 2:
                                    ReviewProduct.PeviewPrpducts(UserName);
                                    break;
                                case 3:
                                    Console.WriteLine("Sign out the app successfully...");
                                    Console.ReadLine();
                                    break;
                                default:
                                    Console.WriteLine("Error");
                                    Console.ReadLine();
                                    break;
                            }
                        } while (_Input != 3);
                    }
                }
                else
                {
                    if (CheckPassword)
                    {
                        int _Input;
                        do
                        {
                            _Input = MenuCashier(UserName);
                            switch (_Input)
                            {
                                case 1:
                                    Processingorder.DisplayOrderListDay(UserName);
                                    break;
                                case 2:
                                    Console.WriteLine("Sign out the app successfully...");
                                    Console.ReadLine();
                                    break;
                                default:
                                    Console.WriteLine("Error");
                                    Console.ReadLine();
                                    break;
                            }
                        } while (_Input != 2);
                    }
                }
            }
        }
        public static string GetPassword()
        {
            StringBuilder pass = new StringBuilder();
            while (true)
            {
                int x = Console.CursorLeft;
                int y = Console.CursorTop;
                ConsoleKeyInfo key = Console.ReadKey(true);
                if (key.Key == ConsoleKey.Enter)
                {
                    Console.WriteLine();
                    break;
                }
                if (key.Key == ConsoleKey.Backspace && pass.Length > 0)
                {
                    pass.Remove(pass.Length - 1, 1);
                    Console.SetCursorPosition(x - 1, y);
                    Console.Write(" ");
                    Console.SetCursorPosition(x - 1, y);
                }
                else if (key.Key != ConsoleKey.Backspace)
                {
                    pass.Append(key.KeyChar);
                    Console.Write("*");
                }
            }
            return pass.ToString();
        }
        public static int MenuSeller(string nameseller)
        {
            string admin = UiConsole.LogoMenuseller(nameseller);
            var table1 = new Table();
            table1.AddColumns(admin);
            table1.BorderColor(Color.White);
            table1.Border(TableBorder.Double);
            AnsiConsole.Write(table1);

            int input_Order = 0;
            var tableOrder = new Table();
            tableOrder.AddColumns("[blue]                             MENU SELLER                                        [/]");
            AnsiConsole.Write(tableOrder);
            AnsiConsole.WriteLine("PLEASE CHOOSE FUNCTION: ");

            var fruit = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .PageSize(10)
                .AddChoices(new[] {
                    "CREATE ORDER","PRODUCTS REVIEW", "LOG OUT",
                })
            );
            switch (fruit)
            {
                case "CREATE ORDER":
                    input_Order = 1;
                    break;
                case "PRODUCTS REVIEW":
                    input_Order = 2;
                    break;
                case "LOG OUT":
                    input_Order = 3;
                    break;
            }
            Console.Clear();
            return input_Order;
        }
        public static int MenuCashier(string namecashier)
        {
            string admin1 = UiConsole.LogoCashier(namecashier);
            var table2 = new Table();
            table2.AddColumns(admin1);
            table2.Border(TableBorder.Double);
            AnsiConsole.Write(table2);

            int input = 0;
            var tableOrder = new Table();
            tableOrder.AddColumns("[blue]                                 MENU CASHIER                                            [/]");
            tableOrder.Border(TableBorder.Rounded);
            AnsiConsole.Write(tableOrder);
            AnsiConsole.WriteLine("PLEASE CHOOSE FUNCTION: ");

            var fruit = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .PageSize(10)
                .AddChoices(new[] {
                    "ORDER PROCESSING", "LOG OUT",
                })
            );
            switch (fruit)
            {
                case "ORDER PROCESSING":
                    input = 1;
                    break;
                case "LOG OUT":
                    input = 2;
                    break;
            }
            Console.Clear();
            return input;
        }
    }
}