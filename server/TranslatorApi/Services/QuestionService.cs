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

        public QuestionService(DataContext context)
        {
            _context = context;
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
        public List<Answer> QueryAnswerbyUserid(string userid)
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

        //根据问题查询回答*
        public List<Answer> QueryAllAnswers(int question_id)
        {
            IQueryable<Answer> query = _context.Answers;
            if (query != null)
            {
                query = query.Where(a => a.QuestionID == question_id);
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
        public bool AdoptAnswer(string userid,int answerid,int questionid)
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
                    return true;
                }
                else
                {
                    Console.WriteLine("不可以采纳自己的回答");
                }
            }
            return false;
        }
    }
}
