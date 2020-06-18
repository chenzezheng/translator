using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TranslatorUI.Models;

namespace TranslatorUI.DBModels
{
    public class DBQuestion
    {
        public int QuestionID { get; set; }
  
        public string Content { get; set; }

        public int Reward { get; set; }
        public DateTime Qcreatetime { get; set; }
        public bool Adopted { get; set; }
        public string UserID { get; set; }
        public List<DBAnswer> Answers { get; set; }
        public DBQuestion()
        { }
        public Question ConvertToQ()
        {
            Question question = new Question();
            question.QuestionId = this.QuestionID;
            question.Content = this.Content;
            question.Reward = this.Reward;
            question.CreateTime = this.Qcreatetime.ToString();
            question.Solved = this.Adopted;
            question.UserId = this.UserID;
            question.Answers = DBAnswer.ConvertToListA(this.Answers);
            return question;
        }

        public static List<Question> ConvertToQList(List<DBQuestion> dbqs)
        {
            List<Question> questions = new List<Question>();
            foreach(DBQuestion dbq in dbqs)
            {
                Question question = new Question();
                question = dbq.ConvertToQ();
                questions.Add(question);
            }
            return questions;
        }

    }
}
