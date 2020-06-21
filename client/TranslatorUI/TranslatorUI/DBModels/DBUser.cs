using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TranslatorUI.Models;

namespace TranslatorUI.DBModels
{
    public class DBUser
    {
        public string UserID { get; set; }
        public string Password { get; set; }
        public int Wealth { get; set; }
        public DBUser() { }
        public static User ConvertToUser(DBUser dBUser)
        {
            User user = new User();
            user.UserId = dBUser.UserID;
            user.Coin = dBUser.Wealth;
            return user;
        }
    }
}
