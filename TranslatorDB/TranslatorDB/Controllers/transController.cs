using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TranslatorDB.Models;

namespace TranslatorDB.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class transController: ControllerBase
    {
        private readonly DataContext _context;

        public transController(DataContext context)
        {
            _context = context;
        }
        [HttpPost]
        //注册
        public ActionResult<User> UserRegister(string userid, string password)
        {
            var user = new User();
            if (userid != "")
            {
                user.UserID = userid;
                user.Password = password;
                user.Wealth = 0;
                _context.Users.Add(user);
                _context.SaveChanges();
            }
            else
            {
                Console.WriteLine("用户名不可为空");
            }
            return user;
        }

        //生成随机不重复数字字符串方法
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

        [HttpGet]
        //登录
        public ActionResult<Token> UserLogin(string userid, string password)
        {
            IQueryable<User> query = _context.Users;
            var token = new Token();
            if (userid != null || password != null)
            {
                query = query.Where(u => u.UserID == userid);
                query = query.Where(u => u.Password == password);
            }
            if (query != null)
            {
                token.TokenID = GenerateCheckCode(1);
                token.UserID = userid;
                _context.Tokens.Add(token);
                _context.SaveChanges();
            }

            return token;
        }

        [HttpGet("token/{tokenid}")]
        //根据tokenid返回userid
        public ActionResult<string> GetUserID(string tokenid)
        {
            var user = _context.Tokens.FirstOrDefault(t => t.TokenID == tokenid);
            if (user == null)
            {
                return "无此登录记录";
            }
            return user.UserID;
        }
        [HttpGet("All")]
        //查看所有问题*
        public ActionResult<List<Question>> GetAllQuestions()
        {
            IQueryable<Question> query = _context.Questions;
            var result = query.ToList();
            return result;
        }

        [HttpPost]
        //创建新问题
        public ActionResult<Question> AddQuestion(Question question)
        {
            question.Qcreatetime = DateTime.Now;
            question.AnswerID = 0;
            try
            {
                _context.Questions.Add(question);
                _context.SaveChanges();
            }
            catch (Exception e)
            {
                return BadRequest(e.InnerException.Message);
            }
            return question;
        }
        [HttpPost]
        //创建新回答
        public ActionResult<Answer> AddAnswer(Answer answer)
        {
            answer.Acreatetime = DateTime.Now;
            answer.Isadopted = false;
            try
            {
                _context.Answers.Add(answer);
                _context.SaveChanges();
            }
            catch (Exception e)
            {
                return BadRequest(e.InnerException.Message);
            }
            return answer;
        }
        [HttpGet("question/{userid}")]
        //查询用户问题
        public ActionResult<List<Question>> QueryQuestionbyUserid(string userid)
        {
            IQueryable<Question> query = _context.Questions;
            if (query != null)
            {
                query = query.Where(q => q.UserID == userid);
            }
            return query.ToList();
        }
        [HttpGet("answer/{userid}")]
        //根据用户查询回答
        public ActionResult<List<Answer>> QueryAnswer(string userid)
        {
            IQueryable<Answer> query = _context.Answers;
            if (query != null)
            {
                query = query.Where(a => a.UserID == userid);
            }
            return query.ToList();
        }
        //根据回答找到提问
        [HttpGet("question/{answerid}")]
        public ActionResult<List<Question>> QueryQuestionbyAnswerid(int answerid)
        {
            IQueryable<Question> query = _context.Questions;
            if (query != null)
            {
                query = query.Where(q => q.AnswerID == answerid);
            }
            return query.ToList();
        }
    }
}