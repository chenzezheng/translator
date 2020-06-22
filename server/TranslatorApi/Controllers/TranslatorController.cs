using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using TranslatorApi.Models;
using TranslatorApi.Services;

namespace TranslatorApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TranslatorController : ControllerBase
    {
        string DecodeFromBase64(string str)
        {
            byte[] temp = Convert.FromBase64String(str);  
            return System.Text.Encoding.Default.GetString(temp);
        }
        //翻译 
        [HttpGet("translate")]
        public ActionResult<List<string>> GetTranslation(string text, string from, string to)
        {
            text = DecodeFromBase64(text);
            TranslationResults results = new TranslationResults();
            try
            {
                if (from == null) from = "auto";
                if (to == null) to = "auto";
                TranslatorService translator = new TranslatorService(text, from, to);
                results = translator.GetTranslationResult();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
            List<string> ret = new List<string>() {results.BaiduResult, results.YoudaoResult, results.TencentResult};
            Console.WriteLine(JsonConvert.SerializeObject(ret));
            return ret;
        }
    }
}
