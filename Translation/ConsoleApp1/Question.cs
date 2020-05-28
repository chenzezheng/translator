using System;
using System.Collections.Generic;

namespace Translation
{
    [Serializable]
    public class Question
    {
        public  int QuestionID { get; set; }
        public  string Content { get; set; }
        public int Reward { get; set; }
        public DateTime Qcreatetime { get; set; }
        public string UserID { get; set; }
        public int AnswerID { get; set; }
        public List<Answer> Answers { get; set; }

        public Question()
        {
            Content = "";
            Reward = 0;
            QuestionID = 0;
            Qcreatetime = DateTime.Now;
            UserID = "";
            Answers = new List<Answer> { };
        }

        public Question(string content,int reward,DateTime qcreatetime,string userid, List<Answer> answers)
        {
            Content = content;
            Reward = reward;
            Qcreatetime = qcreatetime;
            UserID = userid;
            this.Answers = answers;
        }
        public override string ToString()
        {
            return "问题创建者用户名：" + UserID + "问题内容：" + Content + "问题创建时间：" + Qcreatetime + "问题赏金：" + Reward + "回答：" + this.Answers;
        }
    }
}
