using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TransDB.Models;

namespace TransDB
{
    public class DBService
    {
        private readonly DataContext _context;

        public DBService(DataContext context)
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

        //查看所有问题*
        public List<Question> GetAllQuestions()
        {
            IQueryable<Question> query = _context.Questions;
            var result = query.ToList();
            foreach (var question in query)
            {
                Console.WriteLine("提问内容:{0}，问题号:{1}，问题赏金:{2}，问题创建时间:{3}，问题用户名:{4}", question.Content, question.QuestionID, question.Reward, question.Qcreatetime, question.UserID);
            }
            return result;
        }

        //创建新问题*
        public Question AddQuestion(string content,int reward , string userid)
        {
            var question = new Question();
            try
            {
                question.Qcreatetime = DateTime.Now;
                question.UserID = userid;
                question.Content = content;
                question.Reward = reward;
                _context.Questions.Add(question);
                _context.SaveChanges();
            }
            catch (Exception e)
            {
                Console.WriteLine("异常信息：{0}", e.InnerException.Message);
            }
            return question;
        }

        //创建新回答*
        public Answer AddAnswer(string content, string userid, int questionid)
        {
            var answer = new Answer();
            try
            {
                answer.Acreatetime = DateTime.Now;
                answer.Isadopted = false;
                answer.Content = content;
                answer.UserID = userid;
                answer.QuestionID = questionid;
                _context.Answers.Add(answer);
                _context.SaveChanges();
            }
            catch (Exception e)
            {
                Console.WriteLine("异常信息：{0}", e.InnerException.Message);
            }
            return answer;
        }

        //查询用户问题*
        public List<Question> QueryQuestionbyUserid(string userid)
        {
            IQueryable<Question> query = _context.Questions;
            if (query != null)
            {
                query = query.Where(q => q.UserID == userid);
            }
            //Console.WriteLine(query);
            foreach(var question in query)
            {
                Console.WriteLine("提问内容:{0}，问题号:{1}，问题赏金:{2}，问题创建时间:{3}，问题用户名:{4}", question.Content, question.QuestionID, question.Reward, question.Qcreatetime, question.UserID);
            }
            return query.ToList();
        }

        //根据用户查询回答*
        public List<Answer> QueryAnswer(string userid)
        {
            IQueryable<Answer> query = _context.Answers;
            if (query != null)
            {
                query = query.Where(a => a.UserID == userid);
            }
            foreach(var answer in query)
            {
                Console.WriteLine("回答:{0},回答者:{1}", answer.Content, answer.UserID);
            }
            return query.ToList();
        }

        //提问者采纳某一回答
        //根据回答号找到回答者*
        public string GetUserIDbyAns(int answerid)
        {
            var user = _context.Answers.FirstOrDefault(u => u.AnswerID == answerid);
            if (user == null)
            {
                Console.WriteLine("无此回答");
            }
            Console.WriteLine("回答用户名:{0}", user.UserID);
            return user.UserID;
        }
        //根据问题号找到提问者*
        public string GetUserIDbyQues(int questionid)
        {
            var user = _context.Questions.FirstOrDefault(u => u.QuestionID== questionid);
            if (user == null)
            {
                Console.WriteLine("无此问题");
            }
            Console.WriteLine("提问用户名:{0}", user.UserID);
            return user.UserID;
        }
        public void Adopt(string userid,int questionid)
        {
            var user = _context.Users.FirstOrDefault(t => t.UserID == userid);
            var question= _context.Questions.FirstOrDefault(q => q.QuestionID == questionid);
            if (user.Wealth >= question.Reward)
            {
                user.Wealth -=  question.Reward;
            }
            _context.SaveChanges();
            return;
        }
        public void AdoptAnswer(string userid,int answerid,int questionid)
        {
            string QUserid = GetUserIDbyQues(questionid);
            string AUserid = GetUserIDbyAns(answerid);
            var Ans = _context.Answers.FirstOrDefault(a => a.AnswerID == answerid);
            var Ques = _context.Questions.FirstOrDefault(q => q.QuestionID == questionid);
            var user = _context.Users.FirstOrDefault(u => u.UserID == AUserid);

            if (userid == QUserid)
            {
                if(AUserid != QUserid)
                {
                    Ans.Isadopted = true;
                    user.Wealth += Ques.Reward;
                    Adopt(userid, questionid);
                    _context.SaveChanges();
                }
                else
                {
                    Console.WriteLine("不可以采纳自己的回答");
                }
            }

            return;
        }
    }
}
