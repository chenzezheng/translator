using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TranslatorUI.Models
{
    public class DisplayAnswer:Answer
    {
        /*已在抽象类中声明
        public bool ShowAdoptBtn { get; set; }
        public bool IsMyAnswer { set; get; }*/
        public override void convert(Answer answer)     //已在factory中实现，等待删除
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
