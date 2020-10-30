using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TranslatorUI.Models;

namespace TranslatorUI.Abstract
{
    abstract public class AbstractAnswer
    {
        /* Answer类成员*/
        public int AnswerId { get; set; }
        public string Content { get; set; }
        public string CreateTime { get; set; }
        public bool IsAdopted { get; set; }
        public string UserId { get; set; }
        public int QuestionId { get; set; }
        public int Like { get; set; }
        /*DisplayAnswer类成员*/
        public bool ShowAdoptBtn { get; set; }
        public bool IsMyAnswer { set; get; }

        /*DisplayAnswer类方法*/
        public abstract void convert(Answer answer);
    }
}
