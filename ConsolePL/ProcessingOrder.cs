using System;
using BL;
using Persistence;
using Spectre.Console;
using DAL;


namespace ConsolePL
{
    public class Processingorder
    {

        public static List<Orders> DisplayOrderListDay(string NameStaff)
        {
            string? inputIdOrder;
            string admin1 = UiConsole.LogoPaymentbyDay(NameStaff);
            var table2 = new Table();
            table2.AddColumns(new TableColumn(admin1).Centered());
            table2.AddRow(UiConsole.TimeLinePayment(1));
            table2.Border(TableBorder.DoubleEdge);
            // AnsiConsole.Write(table2);
            ProcessingorderBL payMentBL = new ProcessingorderBL();
            List<Orders> ListOder = new List<Orders>();
            ListOder = payMentBL.GetOrderListDayBL();

            if (ListOder.Count() == 0)
            {
                table2.AddRow("[red]No orders today[/]");
                AnsiConsole.Write(table2);
                Console.ReadKey();
            }
            else
            {
                if (ListOder.Count() <= 5)
                {
                    var table = new Table();
                    table.AddColumns("[darkslategray1]ID ORDER[/]", "[darkslategray1]NAME CUSTOMER[/]", "[darkslategray1]TIME ORDER[/]", "[darkslategray1]TOTAL QUANTITY[/]", "[darkslategray1]TOTAL PRICE(VND)[/]");
                    table.BorderColor(Color.White);
                    foreach (Orders item in ListOder)
                    {
                        table.AddRow(item.OrderID.ToString(), item.Name_Customer.ToString(), item.Order_Time.ToString("HH:mm:ss"), item.TotalQuantity.ToString(), item.TotalPrice.ToString("#,##0"));
                    }
                    table2.AddRow(table.Centered());
                    AnsiConsole.Write(table2);
                }
                else
                {
                    int itemsPerPage = 5;
                    int currentPage = 0;

                    while (true)
                    {
                        Console.Clear();

                        var table = new Table();
                        table.AddColumns("[darkslategray1]ID ORDER[/]", "[darkslategray1]NAME CUSTOMER[/]", "[darkslategray1]PRODUCT[/]", "[darkslategray1]QUANTITY[/]", "[darkslategray1]TOTAL PRICE(VND)[/]", "[darkslategray1]ORDER STATUS[/]");

                        int startIndex = currentPage * itemsPerPage;
                        int endIndex = Math.Min(startIndex + itemsPerPage, ListOder.Count);

                        int index = startIndex + 1;
                        foreach (Orders item in ListOder)
                        {
                            table.AddRow(item.OrderID.ToString(), item.Name_Customer.ToString(), item.Order_Time.ToString("HH:mm:ss"), item.TotalQuantity.ToString(), item.TotalPrice.ToString("#,##0"));
                        }

                        table2.AddRow(table.Centered());
                        table2.Border(TableBorder.DoubleEdge);
                        table2.BorderColor(Color.Yellow);
                        AnsiConsole.Write(table2);
                        Console.WriteLine("- \u001b[38;5;238mPRESS 'ENTER' TO ENTER\u001b[0m");
                        if (currentPage > 0)
                        {
                            Console.Write("<< ");
                        }
                        int totalPages = (int)Math.Ceiling((double)ListOder.Count / itemsPerPage);
                        Console.Write($"{currentPage + 1} of {totalPages}");
                        if (endIndex < ListOder.Count)
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
                        else if (keyInfo.Key == ConsoleKey.RightArrow && endIndex < ListOder.Count)
                        {
                            currentPage++;
                        }
                    }
                }
                do
                {
                    List<OrdersDetails> ListOderDetails = new List<OrdersDetails>();
                    Console.Write("INPUT ID ORDER: ");
                    inputIdOrder = Console.ReadLine() ?? "";
                    if (payMentBL.CheckIdOrderBL(inputIdOrder))
                    {
                        DisplayDetailOrder(NameStaff, inputIdOrder,2);
                        MenuPROCESSINGOrders(NameStaff, inputIdOrder);
                        break;
                    }
                    else
                    {
                        Console.WriteLine("\u001b[31mID Order does not exist... \u001b[0m");
                        Console.ReadKey();
                    }
                } while (!(payMentBL.CheckIdOrderBL(inputIdOrder)));
            }
            Console.Clear();
            return ListOder;
        }
        public static List<OrdersDetails> DisplayDetailOrder(string NameCashier, string idorder,int timeline)
        {
            Console.Clear();
            var tableMain = new Table();
            tableMain.AddColumn(new TableColumn(UiConsole.LogoPaymentbyDay(NameCashier)).Centered());
            tableMain.AddRow(UiConsole.TimeLinePayment(timeline));
            tableMain.Border(TableBorder.DoubleEdge);
            tableMain.BorderColor(Color.Yellow);
            ProcessingorderBL payMentBL = new ProcessingorderBL();
            List<OrdersDetails> ListOder = new List<OrdersDetails>();
            ListOder = payMentBL.GetOrderDetailbyID(idorder);

            var table = new Table();
            var table2 = new Table();
            table.AddColumns("[darkslategray1]DETAIL ORDER[/]");

            foreach (OrdersDetails item in ListOder)
            {
                table.AddRow($"-[darkslategray1] Name Customer[/]: {item.NameCustomer}");
                table.AddRow($"-[darkslategray1] Phone[/]: {item.PhoneCustomer}");
                table.AddRow($"-[darkslategray1] Address[/]: {item.AddressCustomer}");
                break;
            }
            int TOTAL_AMOUNT = 0;
            table2.AddColumns("[darkslategray1]ID PRODUCT[/]", "[darkslategray1]NAME PRODUCT[/]", "[darkslategray1]QUANTITY[/]", "[darkslategray1]SIZE[/]", "[darkslategray1]TOTAL PRICE(VND)[/]");
            foreach (OrdersDetails item in ListOder)
            {
                table2.AddRow(item.ProductCode.ToString(), item.NameProduct, item.Quantity.ToString(), item.Size, (item.Quantity * item.Price).ToString("#,##0"));
                TOTAL_AMOUNT = TOTAL_AMOUNT + (item.Quantity * item.Price);
            }
            table.AddRow(table2.Centered());
            tableMain.AddRow(table.Centered());
            tableMain.AddRow($"-[darkslategray1] TOTAL AMOUNT(VND)[/]: {TOTAL_AMOUNT.ToString("#,##0")}");
            AnsiConsole.Write(tableMain);
            return ListOder;
        }
        public static List<OrdersDetails> MakePayments(string namecashier, string idorder)
        {
            Console.Clear();
            ProcessingorderBL payMentBL = new ProcessingorderBL();
            List<OrdersDetails> ListOder = new List<OrdersDetails>();
            ListOder = payMentBL.GetOrderDetailbyID(idorder);

            int Money = 0;
            bool CheckMoney = false;
            string stringMoney;

            DisplayDetailOrder(namecashier, idorder ?? "",3);

            do
            {
                bool checkInput = false;
                while (!checkInput)
                {
                    Console.Write(" - AMOUNT PAID BY CUSTOMER(VND): ");
                    stringMoney = Console.ReadLine() ?? "";
                    checkInput = int.TryParse(stringMoney, out Money);
                    if (!checkInput)
                    {
                        Console.WriteLine("\u001b[31mInput Wrong Value... \u001b[0m");
                    }
                }

                int TotalAmount = 0;
                foreach (OrdersDetails item in ListOder)
                {
                    TotalAmount = TotalAmount + (item.Quantity * item.Price);
                }
                if (Money >= TotalAmount)
                {
                    Console.WriteLine("\u001b[31mPAYMENT SUCCESS\u001b[0m");
                    int price = TotalAmount;
                    Console.WriteLine($"RETURN AMOUNT: {(Money - price).ToString("#,##0")} VND");
                    Console.ReadKey();
                    CheckMoney = true;
                }
                else
                {
                    Console.WriteLine("\u001b[31mPlease check payment amount...\u001b[0m");
                    Console.ReadKey();
                }
            } while (!CheckMoney);
            return ListOder;
        }
        public static void MenuPROCESSINGOrders(string NameStaff, string idorder)
        {
            ProcessingorderDAL payMentDAL = new ProcessingorderDAL();
            int inputPm;
            do
            {
                inputPm = InputMenuPROCESSINGOrders();
                switch (inputPm)
                {
                    case 1:
                        MakePayments(NameStaff, idorder ?? "");
                        ShowInvoice(idorder ?? "", NameStaff);
                        payMentDAL.UpdateStatusOrder(idorder ?? "", "Complete");
                        Console.ReadLine();
                        inputPm = 3;
                        // Environment.Exit(0);
                        break;
                    case 2:
                        payMentDAL.UpdateStatusOrder(idorder ?? "", "Cancel");
                        var tableTimeLineCancelorers = new Table();
                        tableTimeLineCancelorers.AddColumn(new TableColumn(UiConsole.TimeLineCancel(1)).Centered());
                        tableTimeLineCancelorers.Border(TableBorder.DoubleEdge);
                        tableTimeLineCancelorers.Expand();
                        AnsiConsole.Write(tableTimeLineCancelorers.Centered());
                        Console.WriteLine("\u001b[31mYou have canceled your order\u001b[0m");
                        Console.ReadKey();
                        inputPm = 3;
                        break;
                    case 3:
                        Console.WriteLine("\u001b[31mLogout..... \u001b[0m");
                        break;
                    default:
                        Console.WriteLine("\u001b[31mError \u001b[0m");
                        break;
                }
            } while (inputPm != 3);
        }
        public static int InputMenuPROCESSINGOrders()
        {
            bool check = false;
            int choicepayment = 0;
            string input;
            while (!check)
            {
                var table = new Table();
                table.AddColumns($"1: PAYMENT ORDERS\n2: CANCEL ORDERS \n3: BACK");
                table.Border = TableBorder.Double;
                Console.ReadLine();
                AnsiConsole.Write(table);
                table.BorderColor(Color.White);
                Console.WriteLine("- ENTER THE FUNCTION YOU NEED: ");
                input = Console.ReadLine() ?? "";
                check = int.TryParse(input, out choicepayment);
                if (!check)
                {
                    Console.WriteLine("\u001b[31mInput Wrong Value... \u001b[0m");
                }
            }
            return choicepayment;
        }
        public static List<OrdersDetails> ShowInvoice(string idorder, string NameCashier)
        {
            Console.Clear();
            var tableTimeLine = new Table();
            tableTimeLine.AddColumns(UiConsole.TimeLinePayment(4));
            tableTimeLine.Border(TableBorder.DoubleEdge);
            tableTimeLine.Expand();
            AnsiConsole.Write(tableTimeLine.Centered());


            var tableMain = new Table();
            tableMain.AddColumn("[skyblue1]SNEAKER STORE[/]");
            tableMain.AddRow($"[darkslategray1]STORE ADDRESS[/]: 18 TAM TRINH, HA BA TRUNG, HA NOI                     [darkslategray1]HOTLINE[/]: 0999999999 \n[darkslategray1]EMAIL[/]: sneaker@gmail.com.vn                                          [darkslategray1]TAX CODE[/]: 0101010101");
            tableMain.Border(TableBorder.DoubleEdge);
            tableMain.BorderColor(ConsoleColor.DarkGray);

            var tableInvoice = new Table();
            DateTime timeOrder = DateTime.Now;
            tableInvoice.AddColumns(@$"[salmon1]
                     ██ ███    ██ ██    ██  ██████  ██  ██████ ███████               
                     ██ ████   ██ ██    ██ ██    ██ ██ ██      ██                    
                     ██ ██ ██  ██ ██    ██ ██    ██ ██ ██      █████                 
                     ██ ██  ██ ██  ██  ██  ██    ██ ██ ██      ██                    
                     ██ ██   ████   ████    ██████  ██  ██████ ███████               [/]
                     [darkslategray1]TIME[/]: {timeOrder.ToString()}"              ).Centered();

            ProcessingorderBL payMentBL = new ProcessingorderBL();
            List<OrdersDetails> orders = new List<OrdersDetails>();
            orders = payMentBL.GetOrderDetailbyID(idorder);
            var tablein = new Table();
            string nameCustomer ="";
            foreach (OrdersDetails item in orders)
            {
                tableInvoice.AddRow($"                               [salmon1]CODE ORDER[/]: #{item.IDorder} \n- [darkslategray1]CUSTOMER NAME[/]: {item.NameCustomer} \n- [darkslategray1]PHONE[/]: {item.PhoneCustomer} \n- [darkslategray1]ADDRESS[/]: {item.AddressCustomer}");
                nameCustomer = item.NameCustomer;
                break;
            }

            tablein.AddColumns("[darkslategray1]NO[/]", "[darkslategray1]NAME PRODUCT[/]", "[darkslategray1]SIZE[/]", "[darkslategray1]QUANTITY[/]", "[darkslategray1]UNIT PRICE(VND)[/]", "[darkslategray1]TOTAL PRICE(VND)[/]");
            int Total = 0;
            int NO = 1;
            foreach (OrdersDetails item in orders)
            {
                tablein.AddRow(NO.ToString(), item.NameProduct, item.Size, item.Quantity.ToString(),item.Price.ToString("#,##0"), (item.Quantity * item.Price).ToString("#,##0"));
                NO++;
                Total = Total + (item.Quantity * item.Price);
            }
            tableInvoice.AddRow(tablein);
            tableInvoice.AddRow($"[darkslategray1]TOTAL[/]: {Total.ToString("#,##0")} (VND)");
            tableMain.AddRow(tableInvoice.Centered());

            StaffBL staffBL = new StaffBL();
            List<Account> accounts = new List<Account>();
            accounts = staffBL.GetStaffName(NameCashier);
            string Cashier = "";
            foreach (Account item in accounts)
            {
                Cashier = item.NameStaff;
            }

            List<Orders> orders1 = new List<Orders>();
            orders1 = payMentBL.GetOrderByIdBL(idorder);
            string seller ="";
            foreach(Orders item in orders1){
                seller= item.NameSeller;
            }
            tableMain.AddRow(@$"
          [darkslategray1]SELLER[/]                          [darkslategray1]CASHIER[/]                       [darkslategray1]CUSTOMER[/]




       {seller.ToUpper()}                      {Cashier.ToUpper()}                  {nameCustomer.ToUpper()}");
            tableMain.AddRow("[salmon1]\n                                           - THANK YOU! -[/]").Centered();
            AnsiConsole.Write(tableMain);
            return orders;
        }

    }
}