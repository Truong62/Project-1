using System;

namespace Persistence
{
    public class Account
    {
        public string UserName { set; get; }
        public string PassCode { set; get; }
        public string NameStaff {set; get;}
        public string Status{set; get;}

        public Account(string userName, string passCode, string namestaff, string status)
        {
            this.UserName = userName;
            this.PassCode = passCode;
            this.NameStaff = namestaff;
            this.Status = status;
        }
    }
}