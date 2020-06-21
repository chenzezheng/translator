using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TranslatorApi.Models;

namespace TranslatorApi.Services
{
    public class QuestionService
    {
        private readonly DataContext _context;
        private readonly int pageSize = 15;

        public QuestionService(DataContext context)
        {
            _context = context;
        }

        //查看所有问题*
        public List<Question> GetAllQuestions(int page)
        {
            IQueryable<Question> query = _context.Questions.Include("Answers").OrderByDescending(query=>query.Qcreatetime);
            var result = query.Skip((page - 1) * pageSize).Take(pageSize).ToList();
            if (result.Count == 0 && page > 1)
            {
                throw new Exception("Page number is out of range");
            }
            return result;
        }

        //创建新问题*
        public Question AddQuestion(string userid, string content, int reward)
        {
            var question = new Question();
            try
            {
                User user = _context.Users.FirstOrDefault(u=>u.UserID==userid);
                user.Wealth -= reward;
                question.Qcreatetime = DateTime.Now;
                question.UserID = userid;
                question.Content = content;
                question.Adopted = false;
                question.Reward = reward;
                _context.Questions.Add(question);
                _context.SaveChanges();
            }
            catch (Exception e)
            {
                throw new Exception(e.InnerException.Message);
            }
            return question;
        }

        //创建新回答*
        public Answer AddAnswer(string userid, string content, int questionid)
        {
            var answer = new Answer();
            try
            {
                answer.Acreatetime = DateTime.Now;
                answer.Isadopted = false;
                answer.Content = content;
                answer.UserID = userid;
                answer.Likes = 0;
                answer.QuestionID = questionid;
                _context.Answers.Add(answer);
                _context.SaveChanges();
            }
            catch (Exception e)
            {
                throw new Exception(e.InnerException.Message);
            }
            return answer;
        }

        //搜索
        public List<Question> Search(string key, int page)
        {
            IQueryable<Question> query = _context.Questions.Include("Answers").Where(q => q.Content.Contains(key)).OrderByDescending(query=>query.Qcreatetime);
            List<Question> result = query.Skip((page - 1) * pageSize).Take(pageSize).ToList();
            if (result.Count == 0 && page > 1)
            {
                throw new Exception("Page number is out of range");
            }
            return result;
        }

        //点赞*
        public bool Addlikes(string userid, int answerid)
        {
            var answer = _context.Answers.FirstOrDefault(a => a.AnswerID == answerid);
            var like = new Like();
            try
            {
                like.Userid = userid;
                like.AnswerID = answerid;
                answer.Likes += 1;
                _context.Likes.Add(like);
                _context.SaveChanges();
            }
            catch (Exception e)
            {
                throw new Exception(e.InnerException.Message);
            }
            return true;
        }

        //查询用户问题*
        public List<Question> QueryQuestionbyUserid(string userid, int page)
        {
            IQueryable<Question> query = _context.Questions.Include("Answers").OrderByDescending(query=>query.QuestionID);
            if (query != null)
            {
                query = query.Where(q => q.UserID == userid);
            }
            else
            {
                throw new Exception("No such query record");
            }

            List<Question> result = query.Skip((page - 1) * pageSize).Take(pageSize).ToList();
            if (result.Count == 0 && page > 1)
            {
                throw new Exception("Page number is out of range");
            }
            return result;
        }

        //根据用户查询回答*
        public List<Answer> QueryAnswerbyUserid(string userid, int page)
        {
            IQueryable<Answer> query = _context.Answers;
            if (query != null)
            {
                query = query.Where(a => a.UserID == userid);
            }
            else
            {
                throw new Exception("No such query record");
            }
            List<Answer> result = query.Skip((page - 1) * pageSize).Take(pageSize).ToList();
            if (result.Count == 0 && page > 1)
            {
                throw new Exception("Page number is out of range");
            }
            return result;
        }

        //查询用户回答过的问题
        public List<Question> QueryQuestionAnswerdByUser(string userid,int page)
        {
            IEnumerable<Question> query = _context.Questions.Include("Answers");
            query = query.Where(q => q.Answers.Exists(answer => answer.UserID == userid)).OrderByDescending(query=>query.Qcreatetime);
            /*List<Question> questions = new List<Question>();
            foreach(Answer a in query)
            {
                Question question = _context.Questions.Include("Answers").FirstOrDefault(b=>b.QuestionID==a.QuestionID);
                questions.Add(question);
            }*/
            List<Question> result = query.Skip((page-1)*pageSize).Take(pageSize).ToList();
            if (result.Count == 0 && page > 1)
            {
                throw new Exception("Page number is out of range");
            }
            return result;
        }

        //提问者采纳某一回答
        //根据回答号找到回答者*
        public string GetUserIDbyAns(int answerid)
        {
            var user = _context.Answers.FirstOrDefault(u => u.AnswerID == answerid);
            if (user == null)
            {
                throw new Exception("No such answer");
            }
            //Console.WriteLine("回答用户名:{0}", user.UserID);
            return user.UserID;
        }

        //根据问题号找到提问者*
        public string GetUserIDbyQues(int questionid)
        {
            var user = _context.Questions.FirstOrDefault(u => u.QuestionID == questionid);
            if (user == null)
            {
                throw new Exception("No such question");
            }
            //Console.WriteLine("提问用户名:{0}", user.UserID);
            return user.UserID;
        }

        //采纳
         public bool AdoptAnswer(string userid, int answerid, int questionid)
        {
            string QUserid = GetUserIDbyQues(questionid);
            string AUserid = GetUserIDbyAns(answerid);
            var Ans = _context.Answers.FirstOrDefault(a => a.AnswerID == answerid);
            var Ques = _context.Questions.FirstOrDefault(q => q.QuestionID == questionid);
            var user = _context.Users.FirstOrDefault(u => u.UserID == AUserid);

            if (userid == QUserid)
            {
                if (AUserid != QUserid)
                {
                    Ans.Isadopted = true;
                    user.Wealth += Ques.Reward;
                    Ques.Adopted = true;
                  //  Adopt(userid, questionid);
                    _context.SaveChanges();
                }
                else
                {
                    throw new Exception("Can not adopt your answer");
                }
            }
            else
            {
                throw new Exception("Do not have this permission");
            }
            return true;
        }

    }
}
