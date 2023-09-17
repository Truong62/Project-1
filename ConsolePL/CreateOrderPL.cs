using System;
using BL;
using DAL;
using Persistence;
using Spectre.Console;
using System.Text.RegularExpressions;

namespace ConsolePL
{
    public class Order
    {
        public static void MenuSeller(string namestaff)
        {
            List<OrdersDetails> ordersDetails = new List<OrdersDetails>();
            var table1 = new Table();
            ItemBL itemBL = new ItemBL();
            string productCode = "";
            bool checkCode = true;
            int choiceDetailInfoProduct;
            int choiceContiniuOrder = 0;
            bool check = true;
            do
            {
                do
                {

                    GetAllProduct(namestaff);

                    Console.Write("\nENTER PRODUCT CODE: ");
                    productCode = Console.ReadLine() ?? "";
                    checkCode = itemBL.CheckProduct(productCode);
                    if (!checkCode)
                    {
                        Console.WriteLine("\u001b[31mProduct code does not exist...\u001b[0m");
                        Console.ReadLine();
                    }
                } while (!(itemBL.CheckProduct(productCode)));
                if (checkCode)
                {
                    do
                    {
                        choiceDetailInfoProduct = SelectAddProduct();
                        switch (choiceDetailInfoProduct)
                        {
                            case 1:
                                InputProduct(productCode, table1, ordersDetails, namestaff);
                                choiceContiniuOrder = SelectOrderMuch();
                                switch (choiceContiniuOrder)
                                {
                                    case 1:
                                        choiceDetailInfoProduct = 00;
                                        break;
                                    case 00:
                                        InputCustomer(productCode, namestaff, ordersDetails);
                                        choiceDetailInfoProduct = 00;
                                        check = false;
                                        break;
                                    default:
                                        break;
                                }
                                table1 = new Table();
                                break;
                            default:
                                break;
                        }
                    } while (choiceDetailInfoProduct != 00);
                }
            } while (check);


        }
        public static void GetAllProduct(string nameseller)
        {
            ItemBL itemBL = new ItemBL();
            List<Sneakers> ProductList = new List<Sneakers>();
            ProductList = itemBL.GetAllProduct();

            int itemsPerPage = 5;
            int currentPage = 0;

            while (true)
            {
                Console.Clear();
                var TableMain = new Table();

                TableMain.AddColumns(new TableColumn(UiConsole.LogoListBrand(nameseller)).Centered());
                TableMain.AddRow(UiConsole.TimeLineCreateOrder(1));

                var table = new Table();
                table.AddColumns("[darkslategray1]ID[/]", "[darkslategray1]PRODUCT NAME                                  [/]", "[darkslategray1]PRODUCT PRICE[/]", "[darkslategray1]COLOR              [/]", "[darkslategray1]STATUS            [/]");

                int startIndex = currentPage * itemsPerPage;
                int endIndex = Math.Min(startIndex + itemsPerPage, ProductList.Count);

                int index = startIndex + 1;
                foreach (Sneakers item in ProductList.GetRange(startIndex, endIndex - startIndex))
                {
                    table.AddRow(item.ProductCode.ToString(), item.NameSneaker.ToString(), item.Price.ToString("#,##0") + " VND", item.Color.ToString(), item.StatusSneaker.ToString());
                    index++;
                }

                TableMain.AddRow(table);
                TableMain.Border(TableBorder.DoubleEdge);
                TableMain.BorderColor(Color.Yellow);
                AnsiConsole.Write(TableMain);
                Console.WriteLine("- \u001b[38;5;238mPRESS 'ENTER' TO ENTER\u001b[0m");
                if (currentPage > 0)
                {
                    Console.Write("<< ");
                }
                int totalPages = (int)Math.Ceiling((double)ProductList.Count / itemsPerPage);
                Console.Write($"{currentPage + 1}/{totalPages}");
                if (endIndex < ProductList.Count)
                {
                    Console.Write(" >>");
                }

                ConsoleKeyInfo keyInfo = Console.ReadKey(true);
                if (keyInfo.Key == ConsoleKey.LeftArrow && currentPage > 0)
                {
                    currentPage--;
                }
                else if (keyInfo.Key == ConsoleKey.Enter)
                {
                    break;
                }
                else if (keyInfo.Key == ConsoleKey.RightArrow && endIndex < ProductList.Count)
                {
                    currentPage++;
                }
            }
        }

        public static void InputProduct(string productcode, Table table1, List<OrdersDetails> lis, string nameseller)
        {
            ItemBL itemBL = new ItemBL();
            List<Sneakers> ProductInfor = new List<Sneakers>();
            ProductInfor = itemBL.GetInforProducts(productcode);

            var tableMain = new Table();
            tableMain.AddColumns(new TableColumn(UiConsole.LogoInputProduct(nameseller)).Centered());
            tableMain.AddRow(UiConsole.TimeLineCreateOrder(2));
            tableMain.Border(TableBorder.DoubleEdge);
            table1.AddColumn("[darkslategray1]              PRODUCT INFORMATION               [/]").Centered();
            string nameproduct = "";
            foreach (Sneakers item in ProductInfor)
            {
                table1.AddRow($"- NAME SNEAKER: {item.NameSneaker}");
                nameproduct = item.NameSneaker;
            }
            Console.Clear();
            tableMain.AddRow(table1);
            AnsiConsole.Write(tableMain);

            Console.WriteLine("- INPUT SIZE:");
            var SizeSelection = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
            .PageSize(10)
            .AddChoices(new[] {
                                        "36", "37","38", "39","40","41","42","43",
            })
            .UseConverter(item => item.PadRight(5)));
            Console.Clear();
            table1.AddRow($"- SIZE: {SizeSelection}");
            tableMain.Rows.Clear();
            tableMain.AddRow(UiConsole.TimeLineCreateOrder(2));
            tableMain.AddRow(table1);
            AnsiConsole.Write(tableMain);

            int quantityinStock = 0;
            foreach (Sneakers item in ProductInfor)
            {
                quantityinStock = item.Quantity;
            }
            string Size = SizeSelection;
            bool check = false;
            int Quantity = 0;
            string input;
            while (!check)
            {
                Console.Write("- INPUT QUANTITY: ");
                input = Console.ReadLine() ?? "";
                check = int.TryParse(input, out Quantity);
                if (!check)
                {
                    Console.WriteLine("\u001b[31mInput Wrong Value...\u001b[0m");
                }
                else
                {
                    if (Quantity > quantityinStock)
                    {
                        check = false;
                        Console.WriteLine("\u001b[31mOver quantity in stock \u001b[0m");
                        Console.ReadLine();
                    }
                    else if (Quantity < 1){
                        check = false;
                        Console.WriteLine("\u001b[31mInput Wrong Value...\u001b[0m");
                        Console.ReadLine();
                    }
                    else
                    {
                        table1.AddRow($"- QUANTITY: {Quantity}");
                    }
                }

            }
            tableMain.Rows.Clear();
            tableMain.AddRow(UiConsole.TimeLineCreateOrder(2));
            Console.Clear();
            tableMain.AddRow(table1);
            AnsiConsole.Write(tableMain);

            int total = 0;
            int price = 0;
            foreach (Sneakers item in ProductInfor)
            {
                price = item.Price;
                total = item.Price * Quantity;
            }
            table1.AddRow($"- TOTAL MONEY: {total.ToString("#,##0")}");
            tableMain.Rows.Clear();
            tableMain.AddRow(UiConsole.TimeLineCreateOrder(2));
            Console.Clear();
            tableMain.AddRow(table1);
            AnsiConsole.Write(tableMain);
            OrdersDetails ordersDetails1 = new OrdersDetails(productcode, 0, nameproduct, Quantity, SizeSelection, price, "", "", "");
            lis.Add(ordersDetails1);
        }
        public static void InputCustomer(string productcode, string namestaff, List<OrdersDetails> lis)
        {
            ItemBL itemBL = new ItemBL();
            List<Sneakers> ProductInfor = new List<Sneakers>();
            ProductInfor = itemBL.GetInforProducts(productcode);
            bool isPhoneNumberValid = false;
            string Phone = "";

            var tableMain = new Table();
            tableMain.AddColumns(new TableColumn(UiConsole.LogoInputProduct(namestaff)).Centered());
            tableMain.AddRow(UiConsole.TimeLineCreateOrder(3));
            tableMain.Border(TableBorder.DoubleEdge);
            var table1 = new Table();
            while (!isPhoneNumberValid)
            {
                AnsiConsole.Write(tableMain);
                table1.AddColumn("[darkslategray1]              CUSTOMER INFORMATION               [/]").Centered();
                Console.Write("- INPUT PHONE CUSTOMER(+84): ");
                Phone = Console.ReadLine() ?? "";
                string pattern = @"^(0|\+?84|0084)(3[2-9]|5[6|8|9]|7[0|6|7|8|9]|8[1-6|8|9]|9[0-9])\d{7}$";
                isPhoneNumberValid = Regex.IsMatch(Phone, pattern);
                if (!isPhoneNumberValid)
                {
                    Console.WriteLine("\u001b[31mInput Wrong Value... \u001b[0m");
                    Console.ReadLine();
                    table1 = new Table();
                    Console.Clear();
                }
                else
                {
                    if (Phone.ToString().Length == 10)
                    {
                        table1.AddRow($"- PHONE: {Phone}");
                        break;
                    }
                    else
                    {
                        isPhoneNumberValid = false;
                        Console.WriteLine("\u001b[31m Wrong phone number \u001b[0m");
                        Console.ReadLine();
                        table1 = new Table();
                        Console.Clear();
                    }
                }
            }
            Console.Clear();
            tableMain.AddRow(table1);
            AnsiConsole.Write(tableMain);

            ItemBL ItemBL = new ItemBL();
            string NameCustomer = "";
            string Address = "";
            string Nameseller = "";
            if (ItemBL.CheckPhoneCustomerBL(Phone))
            {
                List<Customer> inforCustomer = new List<Customer>();
                inforCustomer = ItemBL.GetInforWithPhoneBL(Phone);
                foreach (Customer item in inforCustomer)
                {
                    NameCustomer = item.NameCustomer;
                    table1.AddRow($"- NAME CUSTOMER: {NameCustomer}");
                    Address = item.Address;
                    table1.AddRow($"- ADDRESS CUSTOMER: {Address}");
                }

                tableMain.Rows.Clear();
                tableMain.AddRow(UiConsole.TimeLineCreateOrder(3));
                tableMain.AddRow(table1);
                Console.Clear();
                AnsiConsole.Write(tableMain);
                StaffBL staffBL = new StaffBL();
                List<Account> accounts = new List<Account>();
                accounts = staffBL.GetStaffName(namestaff);
                foreach (Account item in accounts)
                {
                    Nameseller = item.NameStaff;
                }
                Console.ReadKey();
            }
            else
            {
                do
                {
                    Console.Write("- INPUT NAME CUSTOMER: ");
                    NameCustomer = Console.ReadLine() ?? "";
                    if (NameCustomer.Count() < 0)
                    {
                        Console.WriteLine("\u001b[31m Not to be missed \u001b[0m");
                    }
                } while (NameCustomer.Count() < 0);
                table1.AddRow($"- NAME CUSTOMER: {NameCustomer}");
                tableMain.Rows.Clear();
                tableMain.AddRow(UiConsole.TimeLineCreateOrder(3));
                tableMain.AddRow(table1);
                Console.Clear();
                AnsiConsole.Write(tableMain);

                do
                {
                    Console.Write("- INPUT ADDRESS CUSTOMER: ");
                    Address = Console.ReadLine() ?? "";
                    if (Address.Count() < 0)
                    {
                        Console.WriteLine("\u001b[31m Not to be missed \u001b[0m");
                    }
                } while (Address.Count() < 0);
                table1.AddRow($"- ADDRESS CUSTOMER: {Address}");
                tableMain.Rows.Clear();
                tableMain.AddRow(UiConsole.TimeLineCreateOrder(3));
                tableMain.AddRow(table1);
                Console.Clear();
                AnsiConsole.Write(tableMain);
                StaffBL staffBL = new StaffBL();
                List<Account> accounts = new List<Account>();
                accounts = staffBL.GetStaffName(namestaff);

                foreach (Account item in accounts)
                {
                    Nameseller = item.NameStaff.ToString();
                }
                Console.ReadKey();
            }
            var tableOrderList = new Table();
            tableOrderList.AddColumn("[darkslategray1]ORDER[/]");
            tableOrderList.AddRow(UiConsole.TimeLineCreateOrder(4));
            tableOrderList.Border(TableBorder.DoubleEdge);
            tableOrderList.AddRow($"[darkslategray1]CUSTOMER: [/]{NameCustomer}");
            tableOrderList.AddRow($"[darkslategray1]PHONE CUSTOMER: [/]{Phone}");
            tableOrderList.AddRow($"[darkslategray1]ADDRESS: [/]{Address}");
            var tableOrderList1 = new Table();
            tableOrderList1.AddColumns("[darkslategray1]NO[/]", "[darkslategray1]PRODUCT[/]", "[darkslategray1]SIZE[/]", "[darkslategray1]QUANTITY[/]", "[darkslategray1]UNIT PRICE(VND)[/]", "[darkslategray1]TOTAL PRICE(VND)[/]");
            int STT = 1;
            foreach (OrdersDetails item in lis)
            {
                tableOrderList1.AddRow(STT.ToString(), item.NameProduct, item.Size.ToString(), item.Quantity.ToString(), item.Price.ToString("#,##0"), (item.Quantity * item.Price).ToString("#,##0"));
                STT++;
            }
            tableOrderList.AddRow(tableOrderList1);
            tableOrderList.Border(TableBorder.DoubleEdge);
            Console.Clear();
            AnsiConsole.Write(tableOrderList);

            do
            {
                Console.Write("CORRECT ORDER INFORMATION \u001b[31m'1'\u001b[0m = Yes - \u001b[31m'0'\u001b[0m = No :");
                string accuracy = Console.ReadLine() ?? "";
                if (accuracy == "1")
                {
                    OrderBL orderBL = new OrderBL();
                    orderBL.CreateOrderBL(lis, NameCustomer, Phone, Address, Nameseller);
                    Console.ReadLine();
                    break;
                }
                else if (accuracy == "0")
                {
                    lis.Clear();
                    break;
                }
                else{
                    Console.WriteLine("\u001b[31m Enter wrong value \u001b[0m");
                    Console.ReadLine();
                }

            } while (true);
            Console.Clear();
        }

        public static int SelectAddProduct()
        {
            bool check = false;
            int choiceA = 0;

            while (!check)
            {
                Console.Write("DO YOU WANT TO ADD THIS PRODUCT TO CART \u001b[252;57;31m'1'= YES - '0'= NO\u001b[0m: ");
                string input = Console.ReadLine() ?? "";
                check = int.TryParse(input, out choiceA);
                if (!check)
                {
                    Console.WriteLine("\u001b[31mInput Wrong Value...\u001b[0m");
                }
                else if (choiceA == 1 || choiceA == 0)
                {
                    break;
                }
                else
                {
                    Console.WriteLine("\u001b[31mInput Wrong Value...\u001b[0m");
                    check = false;
                }
            }
            Console.Clear();
            return choiceA;
        }
        public static int SelectOrderMuch()
        {
            bool check = false;
            int choiceA = 0;

            while (!check)
            {
                Console.Write("DO YOU WANT TO BUY MORE \u001b[252;57;31m'1'= YES - '0'= NO\u001b[0m: ");
                string input = Console.ReadLine() ?? "";
                check = int.TryParse(input, out choiceA);
                if (!check)
                {
                    Console.WriteLine("\u001b[31mInput Wrong Value...\u001b[0m");
                }
                else if (choiceA == 1 || choiceA == 0)
                {
                    break;
                }
                else
                {
                    Console.WriteLine("\u001b[31mInput Wrong Value...\u001b[0m");
                    check = false;
                }
            }
            Console.Clear();
            return choiceA;
        }

    }
}