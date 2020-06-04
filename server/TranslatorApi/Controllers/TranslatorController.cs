using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using TransDB.Models;
using TranslatorApi.Models;

namespace TranslatorApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TranslatorApiController : ControllerBase
    {
        private readonly DataContext transContext;
        private DBService transService;

        
        public TranslatorApiController(DataContext context)
        {
            this.transContext = context;
            this.transService = new DBService(context);
        }

        //翻译 
        [HttpGet("translate")]
        public ActionResult<TranslationResult> GetTranslation(string text)
        {
            return Translator.Translate(text, "en", "zh");
        }

        //登录 localhost:5000/translatorapi/login?userid=czz&password=123456
        [HttpGet("login")]
        public ActionResult<Token> GetLogin(string userid, string password)
        {
            return transService.UserLogin(userid, password);
        }

        //根据tokenid返回userid localhost:5000/translatorapi/userid?token=R864B2
        [HttpGet("userid")]
        public ActionResult<string> GetUserID(string token)
        {
            return transService.GetUserID(token);
        }

        //查询回答 localhost:5000/translatorapi/answerQuery?userid=zdt
        [HttpGet("answerQuery")]
        public ActionResult<List<Answer>> GetAnswer(int questionid, string userid)
        {
            if (userid != null)
            {
                //根据用户名查询回答
                return transService.QueryAnswerbyUserid(userid);
            }
            return transService.QueryAllAnswers(questionid);
        }

        //查询问题 localhost:5000/translatorapi/questionQuery?userid=czz2
        [HttpGet("questionQuery")]
        public ActionResult<List<Question>> GetQuestion(string userid)
        {
            if (userid == null)
            {
                //查询所有问题
                return transService.GetAllQuestions();
            }
            //查询用户问题
            return transService.QueryQuestionbyUserid(userid);
        }

        //注册 localhost:5000/translatorapi/register
        [HttpPost("register")]
        public ActionResult<User> PostRegister(User newUser)
        {
            return transService.UserRegister(newUser.UserID, newUser.Password);
        }

        //创建新问题 localhost:5000/translatorapi/newQuestion
        [HttpPost("newQuestion")]
        public ActionResult<Question> PostNewQuestion(Question newQuestion)
        {
            return transService.AddQuestion(newQuestion.Content, newQuestion.Reward, newQuestion.UserID);
        }

        //创建新回答 localhost:5000/translatorapi/newAnswer
        [HttpPost("newAnswer")]
        public ActionResult<Answer> PostNewAnswer(Answer newAnswer)
        {
            return transService.AddAnswer(newAnswer.Content, newAnswer.UserID, newAnswer.QuestionID);
        }

        //采纳 localhost:5000/translatorapi/adopt?userid=czz&answerid=1&questionid=1
        [HttpPut("adopt")]
        public ActionResult<bool> PutAdopt(string userid, int answerid, int questionid)
        {
            return transService.AdoptAnswer(userid, answerid, questionid);
        }
    }
}
