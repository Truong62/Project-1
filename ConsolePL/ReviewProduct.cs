using BL; 
using DAL;
using Persistence;
using Spectre.Console;

namespace ConsolePL{
    public class ReviewProduct{
        public static void PeviewPrpducts(string namseStaff)
        {
            int choiceA = 0;
            do
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

                    TableMain.AddColumns(new TableColumn(UiConsole.LogoListBrand(namseStaff)).Centered());

                    var table = new Table();
                    table.AddColumns("[darkslategray1]ID[/]", "[darkslategray1]PRODUCT NAME                                  [/]", "[darkslategray1]PRODUCT PRICE[/]", "[darkslategray1]COLOR              [/]", "[darkslategray1]MATERIAL                [/]");

                    int startIndex = currentPage * itemsPerPage;
                    int endIndex = Math.Min(startIndex + itemsPerPage, ProductList.Count);

                    int index = startIndex + 1;
                    foreach (Sneakers item in ProductList.GetRange(startIndex, endIndex - startIndex))
                    {
                        table.AddRow(item.ProductCode.ToString(), item.NameSneaker.ToString(), item.Price.ToString("#,##0") + " VND", item.Color.ToString(), item.Material.ToString());
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
                string productcode;
                do
                {
                    Console.Write("\nENTER PRODUCT CODE: ");
                    productcode = Console.ReadLine() ?? "";
                    bool checkCode = itemBL.CheckProduct(productcode);
                    if (!checkCode)
                    {
                        Console.WriteLine("\u001b[31mProduct code does not exist...\u001b[0m");
                        Console.ReadLine();
                    }
                } while (!(itemBL.CheckProduct(productcode)));
                List<Sneakers> ProductInfor = new List<Sneakers>();

                var tableROWinfo = new Table();
                tableROWinfo.AddColumns("[darkslategray1]INFORMATION[/]");

                if (itemBL.CheckProduct(productcode))
                {
                    ProductInfor = itemBL.GetInforProducts(productcode);
                    foreach (Sneakers item in ProductInfor)
                    {
                        tableROWinfo.AddRow($"- NAME: {item.NameSneaker}");
                        tableROWinfo.AddRow($"- QUANTITY: {item.Quantity}");
                        tableROWinfo.AddRow($"- BRAND: {item.Brand}");
                        tableROWinfo.AddRow($"- COLOR: {item.Color}");
                        tableROWinfo.AddRow($"- ORIGIN: {item.Origin}");
                        tableROWinfo.AddRow($"- MATERIAL: {item.Material}");
                        tableROWinfo.AddRow($"- STATUS: {item.StatusSneaker}");
                        tableROWinfo.AddRow($"- PRODUCT CODE - {item.ProductCode}");
                        tableROWinfo.AddRow($"- Size: {item.Size}");
                        tableROWinfo.AddRow($"- PRICE: {item.Price.ToString("#,##0")}");
                        tableROWinfo.BorderColor(Color.White);
                    }
                    tableROWinfo.Border(TableBorder.DoubleEdge);
                    AnsiConsole.Write(tableROWinfo);
                }
                bool check = false;
                

                while (!check)
                {
                    Console.Write("SEE OTHER PRODUCTS \u001b[252;57;31m'1'= YES - '0'= NO\u001b[0m: ");
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
                        check = false;
                    }
                }

            } while (choiceA != 0);
            Console.Clear();
        }
    }
}