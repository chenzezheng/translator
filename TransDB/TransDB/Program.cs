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
            //dbservice.UserRegister("wzf", "4444");
            //dbservice.UserRegister("zja", "12312");
            //dbservice.UserRegister("qyb", "2123");
            //dbservice.UserRegister("zxm", "234");
            //dbservice.UserLogin("zdt", "123");
            //dbservice.GetUserID("0XZL0Z");
            //dbservice.AddQuestion("why", 1, "zdt");
            //dbservice.AddQuestion("what", 4, "czz");
            //dbservice.AddAnswer("no", "czz", 1);
            //dbservice.AddAnswer("nothing", "yjb", 2);
            //dbservice.QueryAnswer("czz");
            //dbservice.GetUserIDbyAns(1);
            //dbservice.GetUserIDbyQues(1);
            dbservice.AdoptAnswer("czz", 2, 2);
        }
    }
}
