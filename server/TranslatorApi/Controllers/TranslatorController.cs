using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace TranslatorApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        [HttpGet("login")]
        public ActionResult<Token> GetLogin(string username, string password)
        {
        }

        [HttpGet("allQuestions")]
        public ActionResult<List<Question>> GetAllQuestions()
        {
        }

        [HttpGet("answerQuery/{question_id}")]
        public ActionResult<List<Answer>> GetAnswer(int question_id)
        {
        }

        [HttpGet("questionQuery/{user_id}")]
        public ActionResult<List<Question>> GetQuestion(int user_id)
        {
        }

        [HttpPost("register")]
        public ActionResult<User> PostRegister(User newUser)
        {
        }

        [HttpPost("newQuestion")]
        public ActionResult<Question> PostNewQuestion(Question newQuestion)
        {
        }

        [HttpPost("newAnswer")]
        public ActionResult<Answer> PostNewAnswer(Answer newAnswer)
        {
        }
    }
}
