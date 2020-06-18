using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TranslatorUI.Models
{
    public class DisplayAnswer:Answer
    {
        public bool ShowAdoptBtn { get; set; }
        public bool IsMyAnswer { set; get; }
        public void convert(Answer answer)
        {
            this.AnswerId = answer.AnswerId;
            this.Content = answer.Content;
            this.CreateTime = answer.CreateTime;
            this.IsAdopted = answer.IsAdopted;
            this.UserId = answer.UserId;
            this.AnswerId = answer.AnswerId;
            this.Like = answer.Like;
        }
        public DisplayAnswer()
        {
            // this.adoptButton = false;
            // this.ismyanswer = false;
        }
    }
}
