using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TranslatorUI.Models
{
    public class Question
    {
        public int QuestionId { get; set; }
        public string Content { get; set; }
        public int Reward { get; set; }
        public string CreateTime { get; set; }
        public string UserId { get; set; }
        public bool Solved { get; set; }
        public List<Answer> Answers { get; set; }

        public Question()
        {
            Content = "测试";
            Reward = 1;
            UserId = "123";
            Solved = true;
            CreateTime = DateTime.UtcNow.ToString();
        }
        public Question(string content, int reward, string userid, bool solved, DateTime dateTime, List<Answer> alist)

        {
            this.Content = content;
            this.Reward = reward;
            this.UserId = userid;
            this.Solved = solved;
            this.CreateTime = dateTime.ToString();
            this.Answers = alist;

        }
    }
}
