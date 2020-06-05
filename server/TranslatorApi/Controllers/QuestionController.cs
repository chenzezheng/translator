using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using TranslatorApi.Models;
using TranslatorApi.Services;

namespace TranslatorApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class QuestionController : ControllerBase
    {
        private QuestionService questionService;

        
        public QuestionController(DataContext context)
        {
            this.questionService = new QuestionService(context);
        }

        //查询回答 localhost:5000/question/answerQuery?userid=zdt
        [HttpGet("answerQuery")]
        public ActionResult<List<Answer>> GetAnswer(int questionid, string userid)
        {
            if (userid != null)
            {
                //根据用户名查询回答
                return questionService.QueryAnswerbyUserid(userid);
            }
            return questionService.QueryAllAnswers(questionid);
        }

        //查询问题 localhost:5000/question/questionQuery?userid=czz
        [HttpGet("questionQuery")]
        public ActionResult<List<Question>> GetQuestion(string userid)
        {
            if (userid == null)
            {
                //查询所有问题
                return questionService.GetAllQuestions();
            }
            //查询用户问题
            return questionService.QueryQuestionbyUserid(userid);
        }

        //创建新问题 localhost:5000/question/newQuestion
        [HttpPost("newQuestion")]
        public ActionResult<Question> PostNewQuestion(Question newQuestion)
        {
            return questionService.AddQuestion(newQuestion.Content, newQuestion.Reward, newQuestion.UserID);
        }

        //创建新回答 localhost:5000/question/newAnswer
        [HttpPost("newAnswer")]
        public ActionResult<Answer> PostNewAnswer(Answer newAnswer)
        {
            return questionService.AddAnswer(newAnswer.Content, newAnswer.UserID, newAnswer.QuestionID);
        }

        //采纳 localhost:5000/question/adopt?userid=czz&answerid=1&questionid=1
        [HttpPut("adopt")]
        public ActionResult<bool> PutAdopt(string userid, int answerid, int questionid)
        {
            return questionService.AdoptAnswer(userid, answerid, questionid);
        }
    }
}
