using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using TranslatorApi.Models;
using TranslatorApi.Services;

namespace TranslatorApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TranslatorController : ControllerBase
    {
        //翻译 
        [HttpGet("translate")]
        public ActionResult<TranslationResult> GetTranslation(string text)
        {
            return BaiduTranslator.Translate(text, "en", "zh");
        }
    }
}
