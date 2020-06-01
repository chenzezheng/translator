using System;
using TransDB.Models;

namespace TransDB
{
    class Program
    {
        static void Main(string[] args)
        {
            var context = new DataContext();
            DBService dbservice = new DBService(context);
            Console.WriteLine("database:");
            //dbservice.UserRegister("zdt", "123");
            //dbservice.UserRegister("czz", "123456");
            //dbservice.UserRegister("yjb", "789");
            //dbservice.UserRegister("dzq", "13322");
            //dbservice.UserLogin("zdt", "123");
            //dbservice.GetUserID("0XZL0Z");
            //dbservice.AddQuestion("why", 1, "zdt");
            //dbservice.AddQuestion("what", 4, "czz");
            //dbservice.AddAnswer("no", "czz", 1);
            //dbservice.AddAnswer("nothing", "yjb", 2);
            //dbservice.QueryAnswer("czz");
        }
    }
}
