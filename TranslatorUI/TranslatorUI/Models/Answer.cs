using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TranslatorUI.Models
{
    public class Answer
    {
        public int AnswerId { get; set; }
        public string Content { get; set; }
        public string CreateTime { get; set; }
        public bool IsAdopted { get; set; }
        public string UserId { get; set; }
        public int QuestionId { get; set; }
        public int Like { get; set; }
        public Answer()
        { }
        public Answer(string content, DateTime date, string userid, bool isAdopted, int answerid,int like)
        {
            this.Content = content;
            this.CreateTime = date.ToString();
            this.UserId = userid;
            this.IsAdopted = isAdopted;
            this.AnswerId = answerid;
            this.Like = like;
        }
    }
}
