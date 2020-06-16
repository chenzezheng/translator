using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TranslatorApi.Models;

namespace TranslatorApi.Services
{
    public class UserService
    {
        private readonly DataContext _context;

        public UserService(DataContext context)
        {
            _context = context;
        }

        //注册*
        public User UserRegister(string userid, string password)
        {
            var user = new User();
            if (userid != "")
            {
                try
                {
                    user.UserID = userid;
                    user.Password = password;
                    user.Wealth = 100;
                    _context.Users.Add(user);
                    _context.SaveChanges();
                }
                catch (Exception e)
                {
                    throw e;
                }
            }
            else
            {
                throw new Exception("Not Null UserID");
            }
            return user;
        }

        //登录*
        public string UserLogin(string userid, string password)
        {
            var user = _context.Users.FirstOrDefault(u => u.UserID == userid);
            if (password == user.Password)
            {
                return userid;
            }
            else
            {
                throw new Exception("Failed to login");
            }
        }
    }
}
