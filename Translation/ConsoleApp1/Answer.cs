using System;


namespace Translation
{
    [Serializable]
    public class Answer
    {
        public int AnswerID { get; set; }
        public string Content { get; set; }
        public DateTime Acreatetime { get; set; }
        public bool Isadopted { get; set; }
        public string UserID { get; set; }
        public int QuestionID { get; set; }

        public Answer()
        {
            AnswerID = 0;
            Content = "";
            Acreatetime = DateTime.Now;
            UserID = "";
            Isadopted = false;
        }
        public Answer(int answerid, string content, DateTime acreatetime,string userid,bool isadopted)
         {
            AnswerID = answerid;
            Content = content;
            Acreatetime = acreatetime;
            UserID = userid;
            Isadopted = isadopted;
         }
        public override string ToString()
        {
            return "回答编号：" + AnswerID + "回答内容：" + Content + "回答创建时间：" + Acreatetime + "回答用户名：" + UserID + "是否被采纳：" + Isadopted;
        }
    }
}
