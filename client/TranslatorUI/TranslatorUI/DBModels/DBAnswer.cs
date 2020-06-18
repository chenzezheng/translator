using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TranslatorUI.Models;

namespace TranslatorUI.DBModels
{
    public class DBAnswer
    {
        public int AnswerID { get; set; }
        public string Content { get; set; }
        public DateTime Acreatetime { get; set; }
        public bool Isadopted { get; set; }
        public string UserID { get; set; }
        public int QuestionID { get; set; }
        public int Likes { get; set; }
        public DBAnswer() { }
        public Answer ConvertToA()
        {
            Answer answer = new Answer();
            answer.AnswerId = this.AnswerID;
            answer.Content = this.Content;
            answer.CreateTime = this.Acreatetime.ToString();
            answer.IsAdopted = this.Isadopted;
            answer.UserId = this.UserID;
            answer.QuestionId = this.QuestionID;
            answer.Like = this.Likes;
            return answer;
        }
        public static List<Answer> ConvertToListA(List<DBAnswer> dbas)
        {
            List<Answer> alist = new List<Answer>();
            foreach(DBAnswer dba in dbas )
            {
                Answer answer = new Answer();
                answer = dba.ConvertToA();
                alist.Add(answer);
            }
            return alist;
        }
    }
}
