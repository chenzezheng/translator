using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using TranslatorUI.Models;

namespace TranslatorUI.Service
{
    public class TranslateService
    {
        //public static string BaseUrl = "http://localhost:5000/";
        public static string BaseUrl = "http://39.108.211.7/";
        public static List<string> GetResult(string text, string from, string to)
        {
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            string url;
            byte[] a = System.Text.Encoding.Default.GetBytes(text);
            string Text = Convert.ToBase64String(a);
            if (from == "" || to == "")
                url = TranslateService.BaseUrl + $"translator/translate?text=" + Text;
            else
                url = TranslateService.BaseUrl + $"translator/translate?text=" + Text + "&from=" + from + "&to=" + to;
            var task = client.GetAsync(url);
            if (task.Result.IsSuccessStatusCode ==false)
                return null;
            string i = task.Result.Content.ReadAsStringAsync().Result;
           // string i = task.Result;
            List<string> tr = JsonConvert.DeserializeObject<List<string>>(i);
            return tr;
        }
    }
}
