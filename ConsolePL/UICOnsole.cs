using BL;
using Persistence;
namespace ConsolePL
{
        public class UiConsole
        {
                public static string TimeLineCreateOrder(int number)
                {
                        string tiemline = "";
                        switch (number)
                        {
                                case 1:
                                        tiemline = "[tan]CHOOSE PRODUCT[/] => [grey]ENTER PRODUCT INFORMATION[/] => [grey]ENTER CUSTOMER INFORMATION[/] => [grey]COMPLETE PAYMENT[/]";
                                        break;
                                case 2:
                                        tiemline = "[gray]CHOOSE PRODUCT[/] => [tan]ENTER PRODUCT INFORMATION[/] => [grey]ENTER CUSTOMER INFORMATION[/] => [grey]COMPLETE ORDER[/]";
                                        break;
                                case 3:
                                        tiemline = "[gray]CHOOSE PRODUCT[/] => [gray]ENTER PRODUCT INFORMATION[/] => [tan]ENTER CUSTOMER INFORMATION[/] => [grey]COMPLETE ORDER[/]";
                                        break;
                                case 4:
                                        tiemline = "[gray]CHOOSE PRODUCT[/] => [gray]ENTER PRODUCT INFORMATION[/] => [grey]ENTER CUSTOMER INFORMATION[/] => [tan]COMPLETE ORDER[/]";
                                        break;
                        }
                        return tiemline;
                }
                public static string TimeLinePayment(int number)
                {
                        string tiemline = "";
                        switch (number)
                        {
                                case 1:
                                        tiemline = "[tan]CHOOSE ORDERS[/]";
                                        break;
                                case 2:
                                        tiemline = "[gray]CHOOSE ORDERS[/] => [tan]CHECK ORDERS[/] ";
                                        break;
                                case 3:
                                        tiemline = "[gray]CHOOSE ORDERS[/] => [gray]CHECK ORDERS[/] => [tan]PAYMENT ORDERS[/] ";
                                        break;
                                case 4:
                                        tiemline = "[gray]CHOOSE ORDERS[/] => [gray]CHECK ORDERS[/] => [grey]PAYMENT ORDERS[/] => [tan]COMPLETE PAYMENT[/]";
                                        break;
                        }
                        return tiemline;
                }
                public static string TimeLineCancel(int number)
                {
                        string tiemline = "";
                        switch (number)
                        {
                                case 1:
                                        tiemline = "[gray]CHOOSE ORDERS[/] => [gray]CHECK ORDERS[/] => [tan]COMPLETE CANCEL ORDERS[/]";
                                        break;
                        }
                        return tiemline;
                }
                public static string LogoLogin = @"[darkslategray1]
             ███████╗███╗   ██╗███████╗ █████╗ ██╗  ██╗███████╗██████╗              
             ██╔════╝████╗  ██║██╔════╝██╔══██╗██║ ██╔╝██╔════╝██╔══██╗             
             ███████╗██╔██╗ ██║█████╗  ███████║█████╔╝ █████╗  ██████╔╝             
             ╚════██║██║╚██╗██║██╔══╝  ██╔══██║██╔═██╗ ██╔══╝  ██╔══██╗             
             ███████║██║ ╚████║███████╗██║  ██║██║  ██╗███████╗██║  ██║             
             ╚══════╝╚═╝  ╚═══╝╚══════╝╚═╝  ╚═╝╚═╝  ╚═╝╚══════╝╚═╝  ╚═╝             
                                                                                    
                     ███████╗████████╗ ██████╗ ██████╗ ███████╗                     
                     ██╔════╝╚══██╔══╝██╔═══██╗██╔══██╗██╔════╝                     
                     ███████╗   ██║   ██║   ██║██████╔╝█████╗                       
                     ╚════██║   ██║   ██║   ██║██╔══██╗██╔══╝                       
                     ███████║   ██║   ╚██████╔╝██║  ██║███████╗                     
                     ╚══════╝   ╚═╝    ╚═════╝ ╚═╝  ╚═╝╚══════╝                      [/]";
                public static string LogoMenuseller(string nameseller)
                {
                        StaffBL staffBL = new StaffBL();
                        List<Account> accounts = new List<Account>();
                        accounts = staffBL.GetStaffName(nameseller);
                        string Nameseller = "";
                        foreach (Account item in accounts)
                        {
                                Nameseller = item.NameStaff;
                        }
                        string LogoSeller = @$"[darkslategray1]
            ██╗    ██╗███████╗██╗      ██████╗ ██████╗ ███╗   ███╗███████╗
            ██║    ██║██╔════╝██║     ██╔════╝██╔═══██╗████╗ ████║██╔════╝
            ██║ █╗ ██║█████╗  ██║     ██║     ██║   ██║██╔████╔██║█████╗  
            ██║███╗██║██╔══╝  ██║     ██║     ██║   ██║██║╚██╔╝██║██╔══╝  
            ╚███╔███╔╝███████╗███████╗╚██████╗╚██████╔╝██║ ╚═╝ ██║███████╗
             ╚══╝╚══╝ ╚══════╝╚══════╝ ╚═════╝ ╚═════╝ ╚═╝     ╚═╝╚══════╝

                  ███████╗███████╗██╗     ██╗     ███████╗██████╗               
                  ██╔════╝██╔════╝██║     ██║     ██╔════╝██╔══██╗
                  ███████╗█████╗  ██║     ██║     █████╗  ██████╔╝
                  ╚════██║██╔══╝  ██║     ██║     ██╔══╝  ██╔══██╗
                  ███████║███████╗███████╗███████╗███████╗██║  ██║
                  ╚══════╝╚══════╝╚══════╝╚══════╝╚══════╝╚═╝  ╚═╝[/] 
[tan] STAFF SELLER[/]: [grey]{Nameseller.ToUpper()} [/]";
                        return LogoSeller;
                }

                public static string LogoListBrand(string nameseller)
                {
                        StaffBL staffBL = new StaffBL();
                        List<Account> accounts = new List<Account>();
                        accounts = staffBL.GetStaffName(nameseller);
                        string Nameseller = "";
                        foreach (Account item in accounts)
                        {
                                Nameseller = item.NameStaff;
                        }
                        string LogoSellerVer3 = @$"[darkslategray1]
             ███████╗███╗   ██╗███████╗ █████╗ ██╗  ██╗███████╗██████╗              
             ██╔════╝████╗  ██║██╔════╝██╔══██╗██║ ██╔╝██╔════╝██╔══██╗             
             ███████╗██╔██╗ ██║█████╗  ███████║█████╔╝ █████╗  ██████╔╝             
             ╚════██║██║╚██╗██║██╔══╝  ██╔══██║██╔═██╗ ██╔══╝  ██╔══██╗             
             ███████║██║ ╚████║███████╗██║  ██║██║  ██╗███████╗██║  ██║             
             ╚══════╝╚═╝  ╚═══╝╚══════╝╚═╝  ╚═╝╚═╝  ╚═╝╚══════╝╚═╝  ╚═╝             
                                                                                    
                     ███████╗████████╗ ██████╗ ██████╗ ███████╗                     
                     ██╔════╝╚══██╔══╝██╔═══██╗██╔══██╗██╔════╝                     
                     ███████╗   ██║   ██║   ██║██████╔╝█████╗                       
                     ╚════██║   ██║   ██║   ██║██╔══██╗██╔══╝                       
                     ███████║   ██║   ╚██████╔╝██║  ██║███████╗                     
                      ╚══════╝   ╚═╝    ╚═════╝ ╚═╝  ╚═╝╚══════╝                      [/]
[tan] LIST OF PRODUCTS [/]- [grey]SELLER: {Nameseller.ToUpper()}[/]";
                        return LogoSellerVer3;
                }
                public static string LogoInputProduct(string nameseller)
                {
                        StaffBL staffBL = new StaffBL();
                        List<Account> accounts = new List<Account>();
                        accounts = staffBL.GetStaffName(nameseller);
                        string Nameseller = "";
                        foreach (Account item in accounts)
                        {
                                Nameseller = item.NameStaff;
                        }
                        string LogoSellerVer3 = @$"[darkslategray1]
             ███████╗███╗   ██╗███████╗ █████╗ ██╗  ██╗███████╗██████╗              
             ██╔════╝████╗  ██║██╔════╝██╔══██╗██║ ██╔╝██╔════╝██╔══██╗             
             ███████╗██╔██╗ ██║█████╗  ███████║█████╔╝ █████╗  ██████╔╝             
             ╚════██║██║╚██╗██║██╔══╝  ██╔══██║██╔═██╗ ██╔══╝  ██╔══██╗             
             ███████║██║ ╚████║███████╗██║  ██║██║  ██╗███████╗██║  ██║             
             ╚══════╝╚═╝  ╚═══╝╚══════╝╚═╝  ╚═╝╚═╝  ╚═╝╚══════╝╚═╝  ╚═╝             
                                                                                    
                     ███████╗████████╗ ██████╗ ██████╗ ███████╗                     
                     ██╔════╝╚══██╔══╝██╔═══██╗██╔══██╗██╔════╝                     
                     ███████╗   ██║   ██║   ██║██████╔╝█████╗                       
                     ╚════██║   ██║   ██║   ██║██╔══██╗██╔══╝                       
                     ███████║   ██║   ╚██████╔╝██║  ██║███████╗                     
                      ╚══════╝   ╚═╝    ╚═════╝ ╚═╝  ╚═╝╚══════╝                      [/]
[tan] CREATE ORDER [/]- [grey]SELLER: {Nameseller.ToUpper()}[/]";
                        return LogoSellerVer3;
                }
                public static string LogoCashier(string namecashier)
                {
                        StaffBL staffBL = new StaffBL();
                        List<Account> accounts = new List<Account>();
                        accounts = staffBL.GetStaffName(namecashier);
                        string NameCashier = "";
                        foreach (Account item in accounts)
                        {
                                NameCashier = item.NameStaff;
                        }
                        string LogoCashier = @$"[darkslategray1]
            ██╗    ██╗███████╗██╗      ██████╗ ██████╗ ███╗   ███╗███████╗
            ██║    ██║██╔════╝██║     ██╔════╝██╔═══██╗████╗ ████║██╔════╝
            ██║ █╗ ██║█████╗  ██║     ██║     ██║   ██║██╔████╔██║█████╗  
            ██║███╗██║██╔══╝  ██║     ██║     ██║   ██║██║╚██╔╝██║██╔══╝  
            ╚███╔███╔╝███████╗███████╗╚██████╗╚██████╔╝██║ ╚═╝ ██║███████╗
             ╚══╝╚══╝ ╚══════╝╚══════╝ ╚═════╝ ╚═════╝ ╚═╝     ╚═╝╚══════╝
                                                                          
                 ██████╗ █████╗ ███████╗██╗  ██╗██╗███████╗██████╗                       
                ██╔════╝██╔══██╗██╔════╝██║  ██║██║██╔════╝██╔══██╗       
                ██║     ███████║███████╗███████║██║█████╗  ██████╔╝       
                ██║     ██╔══██║╚════██║██╔══██║██║██╔══╝  ██╔══██╗       
                ╚██████╗██║  ██║███████║██║  ██║██║███████╗██║  ██║       
                 ╚═════╝╚═╝  ╚═╝╚══════╝╚═╝  ╚═╝╚═╝╚══════╝╚═╝  ╚═╝[/]
[tan] STAFF CASHIER [/]- [grey]CASHIER: {NameCashier.ToUpper()}[/]";
                        return LogoCashier;
                }
                public static string LogoPaymentbyDay(string namecashier)
                {
                        StaffBL staffBL = new StaffBL();
                        List<Account> accounts = new List<Account>();
                        accounts = staffBL.GetStaffName(namecashier);
                        string NameCashier = "";
                        foreach (Account item in accounts)
                        {
                                NameCashier = item.NameStaff;
                        }
                        string LogoCashierVer2 = @$"[darkslategray1]
             ███████╗███╗   ██╗███████╗ █████╗ ██╗  ██╗███████╗██████╗              
             ██╔════╝████╗  ██║██╔════╝██╔══██╗██║ ██╔╝██╔════╝██╔══██╗             
             ███████╗██╔██╗ ██║█████╗  ███████║█████╔╝ █████╗  ██████╔╝             
             ╚════██║██║╚██╗██║██╔══╝  ██╔══██║██╔═██╗ ██╔══╝  ██╔══██╗             
             ███████║██║ ╚████║███████╗██║  ██║██║  ██╗███████╗██║  ██║             
             ╚══════╝╚═╝  ╚═══╝╚══════╝╚═╝  ╚═╝╚═╝  ╚═╝╚══════╝╚═╝  ╚═╝             
                                                                                    
                     ███████╗████████╗ ██████╗ ██████╗ ███████╗                     
                     ██╔════╝╚══██╔══╝██╔═══██╗██╔══██╗██╔════╝                     
                     ███████╗   ██║   ██║   ██║██████╔╝█████╗                       
                     ╚════██║   ██║   ██║   ██║██╔══██╗██╔══╝                       
                     ███████║   ██║   ╚██████╔╝██║  ██║███████╗                     
                     ╚══════╝   ╚═╝    ╚═════╝ ╚═╝  ╚═╝╚══════╝                     [/]
[tan] ORDER OF THE DAY [/]- [grey]CASHIER: {NameCashier.ToUpper()}[/]";
                        return LogoCashierVer2;
                }
        }
}
