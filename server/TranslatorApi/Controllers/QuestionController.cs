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

        //查询问题 localhost:5000/question/questionQuery?userid=czz&page=1
        [HttpGet("questionQuery")]
        public ActionResult<List<Question>> GetQuestion(string userid, string answerdbyuserid, string keyword, int page)
        {
            List<Question> questions = null;
            try
            {
                if (userid == null)
                {
                    if (keyword == null)
                    {
                        if (answerdbyuserid == null)
                        {
                            //查询所有问题
                            questions = questionService.GetAllQuestions(page);
                        }
                        else
                        {
                            //查询用户回答的问题
                            questions = questionService.QueryQuestionAnswerdByUser(answerdbyuserid, page);
                        }
                    }
                    else
                    {
                        //搜索
                        questions = questionService.Search(keyword, page);
                    }
                }
                else
                {
                    //查询用户问题
                    questions = questionService.QueryQuestionbyUserid(userid, page);
                }
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
            
            return questions;
        }

        //创建新问题 localhost:5000/question/newQuestion?userid=czz&content=xxx&reward=123
        [HttpPost("newQuestion")]
        public ActionResult<Question> PostNewQuestion(Question newQuestion)
        {
            try
            {
                newQuestion = questionService.AddQuestion(newQuestion.UserID, newQuestion.Content, newQuestion.Reward);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
            return newQuestion;
        }

        //创建新回答 localhost:5000/question/newAnswer?userid=czz&content=xxx&questionid=1
        [HttpPost("newAnswer")]
        public ActionResult<Answer> PostNewAnswer(Answer newAnswer)
        {
            try
            {
                newAnswer = questionService.AddAnswer(newAnswer.UserID, newAnswer.Content, newAnswer.QuestionID);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
            return newAnswer;
        }

        //采纳 localhost:5000/question/adopt?userid=czz&answerid=1&questionid=1
        [HttpPut("adopt")]
        public ActionResult<bool> PutAdopt(string userid, int answerid, int questionid)
        {
            bool result;
            try
            {
                result = questionService.AdoptAnswer(userid, answerid, questionid);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
            return result;
        }

        //点赞 localhost:5000/question/like?userid=czz&answerid=1
        [HttpPut("like")]
        public ActionResult<bool> PutLike(string userid, int answerid)
        {
            bool result;
            try
            {
                result = questionService.Addlikes(userid, answerid);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
            return result;
        }
    }
}
