using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using Newtonsoft.Json;
using System.Web.Script.Serialization;
using Newtonsoft.Json.Linq;

namespace XunfeiOCR
{
    public class OCR
    {
        public static String Md5(string s)
        {
            System.Security.Cryptography.MD5 md5 = new System.Security.Cryptography.MD5CryptoServiceProvider();
            byte[] bytes = System.Text.Encoding.UTF8.GetBytes(s);
            bytes = md5.ComputeHash(bytes);
            md5.Clear();
            string ret = "";
            for (int i = 0; i < bytes.Length; i++)
            {
                ret += Convert.ToString(bytes[i], 16).PadLeft(2, '0');
            }
            return ret.PadLeft(32, '0');
        }
        public static string GetOCRResult(string file)
        {
            //  应用ID  (必须为webapi类型应用，并印刷文字识别多语种服务，参考帖子如何创建一个webapi应用：http://bbs.xfyun.cn/forum.php?mod=viewthread&tid=36481)
            string x_appid = "5e1ae316";
            //  接口密钥 (webapi类型应用开通印刷文字识别多语种服务后，控制台--我的应用---印刷文字识别多语种---服务的apikey)
            string api_key = "571516fdf49395d0bcd3589532bdc93d";
            //  上传图片地址,格式jpg和jpeg，base64编码之后图片大小不超过4M
            string path = file;
            //  引擎类型
            string param = "{\"engine_type\":\"recognize_document\"}";

            System.Text.Encoding encode = System.Text.Encoding.ASCII;
            byte[] bytedata = encode.GetBytes(param);
            string x_param = Convert.ToBase64String(bytedata);


            TimeSpan ts = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0);
            string curTime = Convert.ToInt64(ts.TotalSeconds).ToString();

            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
            string result = string.Format("{0}{1}{2}", api_key, curTime, x_param);
            //Console.WriteLine(Program.Md5(x_param));
            string X_checksum = OCR.Md5(result);
            //Console.WriteLine(X_checksum);

            byte[] arr = File.ReadAllBytes(path);
            string cc = Convert.ToBase64String(arr);
            string data = "image=" + cc;
            //  印刷文字识别多语种webapi的地址
            string Url = "http://webapi.xfyun.cn/v1/service/v1/ocr/recognize_document";
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(Url);
            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";
            request.Headers["X-Appid"] = x_appid;
            request.Headers["X-CurTime"] = curTime;
            request.Headers["X-Param"] = x_param;
            request.Headers["X-CheckSum"] = X_checksum;

            request.ContentLength = Encoding.UTF8.GetByteCount(data);
            Stream requestStream = request.GetRequestStream();
            StreamWriter streamWriter = new StreamWriter(requestStream, Encoding.GetEncoding("gb2312"));
            streamWriter.Write(data);
            streamWriter.Close();

            string htmlStr = string.Empty;
            HttpWebResponse response = request.GetResponse() as HttpWebResponse;
            Stream responseStream = response.GetResponseStream();
            using (StreamReader reader = new StreamReader(responseStream, Encoding.GetEncoding("UTF-8")))
            {
                htmlStr = reader.ReadToEnd();
            }
            responseStream.Close();

            return GetTextFromJson(htmlStr);
            
        }

        private static string GetTextFromJson(string json)
        {
            JObject jObj = JObject.Parse(json);
            string text = "";
            foreach (JObject block in jObj["data"]["document"]["blocks"])
            {
                foreach (JObject line in block["lines"])
                {
                    foreach (JObject character in line["characters"])
                    {
                        text += character["text"].ToString();
                    }
                }
            }
            return text;
        }
    }
}
