using System;
using DAL;
using Persistence;

namespace BL{
    public class StaffBL
    {
        public LoginDb LoginDAL = new LoginDb();
        public bool loginBL(string UserName, string PassWord)
        {
            return LoginDAL.LoginAccount(UserName, PassWord);
        }
        public bool CheckUserBL(string UserName)
        {
            return LoginDAL.CheckAccount(UserName);
        }
        public bool CheckPassword(string password){
            return LoginDAL.GetPassword(password);
        }
        public List<Account> GetStaffName(string namestaff){
            return LoginDAL.GetNameStaff(namestaff);
        }
    }
}