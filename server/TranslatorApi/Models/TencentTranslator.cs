using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Net;
using System.IO;
using Newtonsoft.Json;

namespace TranslatorApi.Models.Tencent
{
    public class Translation
    {
        public string source_text { get; set; }
        public string target_text { get; set; }
    }

    public class TranslationResult
    {
        public string ret { get; set; }
        public string msg { get; set; }
        public Translation data { get; set; }
    }

    public class Translator
    {
        private const string appId = "2149663649";
        private const string appKey = "7DBzl7mGEklY3Zqj";
        public static string source;
        public static string target;
        
        public static TranslationResult Translate(string text, string from, string to)
        {
            source = from;
            target = to;
            var data = GetResult(text.Trim());
            return Post("https://api.ai.qq.com/fcgi-bin/nlp/nlp_texttranslate", data); 
          
        }

        public static string GetResult(string value)
        {
            value = HttpUtility.UrlEncode(value, Encoding.UTF8).ToUpper();
            var sdic = new SortedDictionary<string, string>
            {
                { "app_id", appId },
                { "time_stamp", GetTimeStamp() },
                { "nonce_str", GetRandomCode() },
                { "text", value },
                { "source", source },
                { "target", target }
            };
            sdic.Add("sign", GetSign(sdic));
            return GetUrlValue(sdic);
        }
        private static string GetTimeStamp()
        {
            return DateTimeOffset.Now.ToUnixTimeSeconds().ToString();
        }

        private static string GetRandomCode()
        {
            return Guid.NewGuid().ToString("N");
        }

        private static string GetSign(SortedDictionary<string, string> sdic)
        {
            var str = $"{GetUrlValue(sdic)}&app_key={appKey}";
            using (var md5csp = new MD5CryptoServiceProvider())
            {
                var temp = Encoding.UTF8.GetBytes(str);
                temp = md5csp.ComputeHash(temp);
                return BitConverter.ToString(temp).Replace("-", "");
            }
        }

        private static string GetUrlValue(SortedDictionary<string, string> sdic)
        {
            var sb = new StringBuilder();
            foreach (var item in sdic)
            {
                if (sb.Length > 0)
                {
                    sb.Append('&');
                }
                sb.Append($"{item.Key}={item.Value}");
            }
            return sb.ToString();
        }
        private static TranslationResult Post(string url, string data)
        {
            try
            {
                var request = (HttpWebRequest)WebRequest.Create(url);
                request.AllowAutoRedirect = true;
                request.Method = "POST";
                request.ContentType = "application/x-www-form-urlencoded";
                var temp = Encoding.UTF8.GetBytes(data);
                request.ContentLength = temp.Length;
                using (var stream = request.GetRequestStream())
                {
                    stream.Write(temp, 0, temp.Length);
                }
                var response = (HttpWebResponse)request.GetResponse();
                using (var reader = new StreamReader(response.GetResponseStream(), Encoding.GetEncoding(response.CharacterSet)))
                {                   
                    TranslationResult result = JsonConvert.DeserializeObject<TranslationResult>(reader.ReadToEnd());
                    return result;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}