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
using System.Security.Cryptography;
using Newtonsoft.Json;
using TranslatorApi.Models;

namespace TranslatorApi.Services
{
    public class TranslatorService
    {
        Dictionary <string, string> BaiduTable = new Dictionary<string, string>{
            {"Chinese", "zh"},
            {"English", "en"},
            {"Japanese", "jp"},
            {"Spanish", "spa"},
            {"French", "fra"},
            {"auto", "auto"}
        };
        Dictionary <string, string> YoudaoTable = new Dictionary<string, string>{
            {"Chinese", "zh-CHS"},
            {"English", "en"},
            {"Japanese", "ja"},
            {"Spanish", "es"},
            {"French", "fr"}
        };
        Dictionary <string, string> TencentTable = new Dictionary<string, string>{
            {"Chinese", "zh"},
            {"English", "en"},
            {"Japanese", "jp"},
            {"Spanish", "es"},
            {"French", "fr"}
        };
        string text;
        string from;
        string to;
        public TranslatorService(string text, string from = "auto", string to = "auto")
        {
            this.to = BaiduTable[to];
            this.from = BaiduTable[from];
            this.text = text;
        }

        public TranslationResults GetTranslationResult()
        {
            TranslationResults results = new TranslationResults();
            TranslatorApi.Models.Baidu.TranslationResult BaiduResult = TranslatorApi.Models.Baidu.Translator.Translate(text, from, to);
            foreach(string key in BaiduTable.Keys)
            {
                if (BaiduTable[key] == BaiduResult.From) from = key;
                if (BaiduTable[key] == BaiduResult.To) to = key;
            }
            TranslatorApi.Models.Youdao.TranslationResult YoudaoResult = TranslatorApi.Models.Youdao.Translator.Translate(text, YoudaoTable[from], YoudaoTable[to]);
            TranslatorApi.Models.Tencent.TranslationResult TencentResult = TranslatorApi.Models.Tencent.Translator.Translate(text, TencentTable[from], TencentTable[to]);
            foreach (TranslatorApi.Models.Baidu.Translation str in BaiduResult.Trans_result) results.BaiduResult += str.Dst;
            foreach (string str in YoudaoResult.translation) results.YoudaoResult += str;
            results.TencentResult = TencentResult.data.target_text;
            return results;
        }
    }
}