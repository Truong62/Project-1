namespace PrimeService.Tests;
using BL;
using Persistence;
public class UnitTest1
{
    [Fact]
    public void LoginTest()
    {
        StaffBL sBL = new StaffBL();

        Assert.True(sBL.loginBL("seller@1", "12345678"));
    }

    [Theory]
    [InlineData("THA005", 1, "Adidas Originals Drop Step Low Grey", 3, "40", 2570000)]
    public void TestCreateOrderBL(string orderCode, int productId, string productName, int quantity, string size, int price)
    {
        List<OrdersDetails> ordersdetails = new List<OrdersDetails>();

        StaffBL sBL = new StaffBL();
        Assert.True(sBL.loginBL("seller@1", "12345678"));

        StaffBL staffBL = new StaffBL();
        List<Account> accounts = new List<Account>();
        accounts = staffBL.GetStaffName("seller@1");
        string namestaff = "";
        foreach (Account item in accounts)
        {
            namestaff = item.NameStaff;
        }

        OrdersDetails ordersDetails1 = new OrdersDetails(orderCode, productId, productName, quantity, size, price, "", "", "");
        ordersdetails.Add(ordersDetails1);

        OrderBL sOrderBL = new OrderBL();

        sOrderBL.CreateOrderBL(ordersdetails, "Nguyen van a", "123 Main St", "0987654321", namestaff);

        Assert.NotEmpty(ordersdetails);
    }

}