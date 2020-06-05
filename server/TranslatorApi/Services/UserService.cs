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
                user.UserID = userid;
                user.Password = password;
                user.Wealth = 100;
                _context.Users.Add(user);
                _context.SaveChanges();
            }
            else
            {
                Console.WriteLine("用户名不可为空");
            }
            return user;
        }

        //生成随机不重复字符串*
        private int rep = 0;
        private string GenerateCheckCode(int codeCount)
        {
            string str = string.Empty;
            long num2 = DateTime.Now.Ticks + this.rep;
            this.rep++;
            Random random = new Random(((int)(((ulong)num2) & 0xffffffffL)) | ((int)(num2 >> this.rep)));
            for (int i = 0; i < codeCount; i++)
            {
                char ch;
                int num = random.Next();
                if ((num % 2) == 0)
                {
                    ch = (char)(0x30 + ((ushort)(num % 10)));
                }
                else
                {
                    ch = (char)(0x41 + ((ushort)(num % 0x1a)));
                }
                str = str + ch.ToString();
            }
            return str;
        }

        //登录*
        public Token UserLogin(string userid, string password)
        {
            IQueryable<User> query = _context.Users;
            var token = new Token();
            foreach(var user in query)
            {
                if (userid == user.UserID && password == user.Password)
                {
                    token.TokenID = GenerateCheckCode(6);
                    token.UserID = userid;
                }
            }
            _context.Tokens.Add(token);
            _context.SaveChanges();
            Console.WriteLine("用户名:{0},登录令牌号:{1}", token.UserID, token.TokenID);
            return token;
        }

        //根据tokenid返回userid*
        public string GetUserID(string tokenid)
        {
            var user = _context.Tokens.FirstOrDefault(t => t.TokenID == tokenid);
            if (user == null)
            {
                Console.WriteLine("无此登录记录");
            }
            Console.WriteLine("用户名:{0}", user.UserID);
            return user.UserID;
        }

    }
}
