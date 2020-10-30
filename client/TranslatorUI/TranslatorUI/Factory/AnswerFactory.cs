using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TranslatorUI.Abstract;
using TranslatorUI.DBModels;
using TranslatorUI.Models;

namespace TranslatorUI.Factory
{
    public class AnswerFactory
    {
        public static AbstractAnswer GetAnswer(DBAnswer dbAnswer)
        {
            Answer answer = new Answer();
            answer.AnswerId = dbAnswer.AnswerID;
            answer.Content = dbAnswer.Content;
            answer.CreateTime = dbAnswer.Acreatetime.ToString();
            answer.IsAdopted = dbAnswer.Isadopted;
            answer.UserId = dbAnswer.UserID;
            answer.QuestionId = dbAnswer.QuestionID;
            answer.Like = dbAnswer.Likes;
            return answer;
        }
       
        public static AbstractAnswer GetDisplayAnswer(Answer answer)
        {
            DisplayAnswer displayAnswer = new DisplayAnswer();
            displayAnswer.AnswerId = answer.AnswerId;
            displayAnswer.Content = answer.Content;
            displayAnswer.CreateTime = answer.CreateTime;
            displayAnswer.IsAdopted = answer.IsAdopted;
            displayAnswer.UserId = answer.UserId;
            displayAnswer.AnswerId = answer.AnswerId;
            displayAnswer.Like = answer.Like;

            displayAnswer.IsMyAnswer = false;
            displayAnswer.ShowAdoptBtn = false;
            return displayAnswer;
        }
    }
}
