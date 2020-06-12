using System;
using System.IO;
using System.Net;
using System.Text;
using System.Collections.Generic;
using System.Security.Cryptography;
using Newtonsoft.Json;


namespace zhiyun_csharp_demo
{
    class FanyiV3DemoInternalTest
    {
        public static void Main()
        {
            Dictionary<String, String> dic = new Dictionary<String, String>();
            string url = "https://openapi.youdao.com/api";

            Console.WriteLine("请输入待翻译的文本");
            string q = Console.ReadLine();
            Console.WriteLine("请输入当前语言类型");
            string from = Console.ReadLine();
            Console.WriteLine("请输入目标语言类型");
            string to = Console.ReadLine();
            Console.WriteLine("翻译结果为");

            string appKey = "0797a9a0a3fab4c7";
            string appSecret = "W5JU1CsEEpGCnbIoReV2TT3d4rhKaYNc";
            string salt = DateTime.Now.Millisecond.ToString();
            dic.Add("from", from);
            dic.Add("to", to);
            dic.Add("signType", "v3");
            TimeSpan ts = (DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc));
            long millis = (long)ts.TotalMilliseconds;
            string curtime = Convert.ToString(millis / 1000);
            dic.Add("curtime", curtime);
            string signStr = appKey + Truncate(q) + salt + curtime + appSecret; ;
            string sign = ComputeHash(signStr, new SHA256CryptoServiceProvider());
            dic.Add("q", System.Web.HttpUtility.UrlEncode(q));
            dic.Add("appKey", appKey);
            dic.Add("salt", salt);
            dic.Add("sign", sign);
            Post(url, dic);
        }

        protected static string ComputeHash(string input, HashAlgorithm algorithm)
        {
            Byte[] inputBytes = Encoding.UTF8.GetBytes(input);
            Byte[] hashedBytes = algorithm.ComputeHash(inputBytes);
            return BitConverter.ToString(hashedBytes).Replace("-", "");
        }

        protected static void Post(string url, Dictionary<String, String> dic)
        {
            string result = "";
            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);
            req.Method = "POST";
            req.ContentType = "application/x-www-form-urlencoded";
            StringBuilder builder = new StringBuilder();
            int i = 0;
            foreach (var item in dic)
            {
                if (i > 0)
                    builder.Append("&");
                builder.AppendFormat("{0}={1}", item.Key, item.Value);
                i++;
            }
            byte[] data = Encoding.UTF8.GetBytes(builder.ToString());
            req.ContentLength = data.Length;
            using (Stream reqStream = req.GetRequestStream())
            {
                reqStream.Write(data, 0, data.Length);
                reqStream.Close();
            }
            HttpWebResponse resp = (HttpWebResponse)req.GetResponse();
            if (resp.ContentType.ToLower().Equals("audio/mp3"))
            {
                SaveBinaryFile(resp, "合成的音频存储路径");
            }
            else
            {
                Stream stream = resp.GetResponseStream();
                using (StreamReader reader = new StreamReader(stream, Encoding.UTF8))
                {
                    result = reader.ReadToEnd();
                }
                TranslationResult ak = JsonConvert.DeserializeObject<TranslationResult>(result);
                Console.WriteLine(ak.translation[0]);
            }
        }

        protected static string Truncate(string q)
        {
            if (q == null)
            {
                return null;
            }
            int len = q.Length;
            return len <= 20 ? q : (q.Substring(0, 10) + len + q.Substring(len - 10, 10));
        }

        private static bool SaveBinaryFile(WebResponse response, string FileName)
        {
            string FilePath = FileName + DateTime.Now.Millisecond.ToString() + ".mp3";
            bool Value = true;
            byte[] buffer = new byte[1024];

            try
            {
                if (File.Exists(FilePath))
                    File.Delete(FilePath);
                Stream outStream = System.IO.File.Create(FilePath);
                Stream inStream = response.GetResponseStream();

                int l;
                do
                {
                    l = inStream.Read(buffer, 0, buffer.Length);
                    if (l > 0)
                        outStream.Write(buffer, 0, l);
                }
                while (l > 0);

                outStream.Close();
                inStream.Close();
            }
            catch
            {
                Value = false;
            }
            return Value;
        }

        public class TranslationResult
        {
            //错误码，翻译结果无法正常返回
            public string errorCode { get; set; }
            public string query { get; set; }

            public string[] translation { get; set; }
            //public Translation XXX { get; set; }

            /*public string basic { get; set; }
            public Web[] web { get; set; }
            public string l { get; set; }
            public string dict { get; set; }
            public string webdict { get; set; }
            public string tSpeakUrl { get; set; }
            public string speakUrl { get; set; }
            public returnPhrase[] ReturnPhrase { get; set; }
            //翻译正确，返回的结果*/

        }
    }
}