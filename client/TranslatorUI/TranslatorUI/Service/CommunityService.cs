using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using TranslatorUI.Models;
using TranslatorUI.DBModels;
using Newtonsoft.Json;

namespace TranslatorUI.Service
{
    public class CommunityService
    {
        public static string BaseUrl = "http://localhost:5000/";
        static public bool SignUp(string username, string password)   //注册
        {
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            string url = CommunityService.BaseUrl + $"user/register?userid=" + username + "&password=" + password;
            var task = client.PostAsync(url,null);
            bool success = task.Result.IsSuccessStatusCode;
            return success;
        }
         static public List<Question> GetAllQuestions(int page)   //浏览社区问题,分页版
         {
            //获得所有提问与回答并返回到list  ,question里带List<answer>
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            string url = CommunityService.BaseUrl + $"question/questionQuery?page="+page;
            var task = client.GetAsync(url);
                if(task.Result.IsSuccessStatusCode==false)
                {
                    return null;
                }
                else
                {
                    string i = task.Result.Content.ReadAsStringAsync().Result;
                    List<DBQuestion> dbq = JsonConvert.DeserializeObject<List<DBQuestion>>(i);
                    List<Question> qlist = new List<Question>();
                    qlist = DBQuestion.ConvertToQList(dbq);
                    return qlist;
                }         
        }
        static public List<Question> SearchQuestion(string keyword,int page)
        {
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            string url = CommunityService.BaseUrl + $"question/questionQuery?keyword="+keyword+"&page=" + page;
            var task = client.GetAsync(url);
            if (task.Result.IsSuccessStatusCode == false)
            {
                return null;
            }
            else
            {
                string i = task.Result.Content.ReadAsStringAsync().Result;
                List<DBQuestion> dbq = JsonConvert.DeserializeObject<List<DBQuestion>>(i);
                List<Question> qlist = new List<Question>();
                qlist = DBQuestion.ConvertToQList(dbq);
                return qlist;
            }
        }
         
    }
}
