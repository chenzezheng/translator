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
            dbservice.AddAnswer("1", "zdt", "8JJ0886T");

        }
    }
}
