using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Net;
using System.Text;
using System.Web;
//using System.Web.Script.Serialization;
using System.Security.Cryptography;
using Newtonsoft.Json;

namespace Baidu
{
    class Program
    {
        public string a;
        public string b;
        static void Main(string[] args)
        {
            Program A = new Program();
            Console.WriteLine("语言类型，简体中文：zh；英语：en；繁体中文：cht；日语：jp；韩语：kor；系统自动识别：auto（更多请参考帮助文档）");
            Console.WriteLine("请输入当前语言类型:");
            A.a = Console.ReadLine();
            Console.WriteLine("请输入目标语言类型:");
            A.b = Console.ReadLine();
            Console.WriteLine("请输入需要翻译的文本");
            TranslationResult result = GetTranslationFromBaiduFanyi(Console.ReadLine(), A.a, A.b);
            Console.WriteLine("翻译结果为");
            //判断是否出错
            if (result.Error_code == null)
            {
                Console.WriteLine(result.Trans_result[0].Dst);
            }
            else
            {
                //检查appid和密钥是否正确
                Console.WriteLine("翻译出错，错误码：" + result.Error_code + "，错误信息：" + result.Error_msg);
            }

        }

        private static TranslationResult GetTranslationFromBaiduFanyi(string q, string languageFrom, string languageTo)
        {
            string appId = "20200526000471917";
            string password = "2xyqypZkJ_am2OMp7Cqk";

            string jsonResult = String.Empty;
            //随机数
            string randomNum = System.DateTime.Now.Millisecond.ToString();
            //md5加密
            string md5Sign = GetMD5WithString(appId + q + randomNum + password);
            //url
            string url = String.Format("http://api.fanyi.baidu.com/api/trans/vip/translate?q={0}&from={1}&to={2}&appid={3}&salt={4}&sign={5}",
                HttpUtility.UrlEncode(q, Encoding.UTF8),
                languageFrom,
                languageTo,
                appId,
                randomNum,
                md5Sign
                );
            WebClient wc = new WebClient();
            try
            {
                jsonResult = wc.DownloadString(url);
            }
            catch
            {
                jsonResult = string.Empty;
            }
            //解析json
            //JavaScriptSerializer jss = new JavaScriptSerializer();
            TranslationResult result = JsonConvert.DeserializeObject<TranslationResult>(jsonResult);
            return result;
        }
        //对字符串做md5加密
        private static string GetMD5WithString(string input)
        {
            if (input == null)
            {
                return null;
            }
            MD5 md5Hash = MD5.Create();
            //将输入字符串转换为字节数组并计算哈希数据  
            byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));
            //创建一个 Stringbuilder 来收集字节并创建字符串  
            StringBuilder sBuilder = new StringBuilder();
            //循环遍历哈希数据的每一个字节并格式化为十六进制字符串  
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }
            //返回十六进制字符串  
            return sBuilder.ToString();
        }

    }
    public class Translation
    {
        public string Src { get; set; }
        public string Dst { get; set; }
    }
    public class TranslationResult
    {
        //错误码，翻译结果无法正常返回
        public string Error_code { get; set; }
        public string Error_msg { get; set; }
        public string From { get; set; }
        public string To { get; set; }
        public string Query { get; set; }
        //翻译正确，返回的结果
        //这里是数组的原因是百度翻译支持多个单词或多段文本的翻译，在发送的字段q中用换行符（\n）分隔
        public Translation[] Trans_result { get; set; }
    }
}
