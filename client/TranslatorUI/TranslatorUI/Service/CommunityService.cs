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
        //public static string BaseUrl = "http://localhost:5000/";
        public static string BaseUrl = "http://39.108.211.7/";
        static public bool SignUp(string username, string password)   //注册
        {
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            string url = CommunityService.BaseUrl + $"user/register";
            DBUser dbUser = new DBUser() {UserID=username,Password=password };
            HttpContent user = new StringContent(JsonConvert.SerializeObject(dbUser), Encoding.UTF8, "application/json");
            var task = client.PostAsync(url,user);
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
        static public List<Question> SearchQuestion(string keyword,int page)  //查询问题
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
