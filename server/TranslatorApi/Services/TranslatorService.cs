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
        string text;
        string from;
        string to;
        public TranslatorService(string text, string from = "auto", string to = "auto")
        {
            this.text = text;
            this.from = from;
            this.to = to;
        }

        public TranslationResults GetTranslationResult()
        {
            TranslationResults results = new TranslationResults();
            results.BaiduResult = TranslatorApi.Models.Baidu.Translator.Translate(text, from, to);
            results.YoudaoResult = TranslatorApi.Models.Youdao.Translator.Translate(text, from, to);
            results.TencentResult = TranslatorApi.Models.Tencent.Translator.Translate(text, from, to);
            return results;
        }
    }
}