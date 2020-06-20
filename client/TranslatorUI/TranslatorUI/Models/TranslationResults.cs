using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TranslatorUI.Models
{
    public class TranslationResults
    {
        public string BaiduResult { get; set; }
        public string YoudaoResult { get; set; }
        public string TencentResult { get; set; }
        public TranslationResults()
        {  
            BaiduResult = "";
        YoudaoResult = "";
        TencentResult = "";
    }
    }
}
