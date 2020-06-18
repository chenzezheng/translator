using TranslatorApi.Models;

namespace TranslatorApi.Models
{
    public class TranslationResults
    {
        public Baidu.TranslationResult BaiduResult;
        public Youdao.TranslationResult YoudaoResult;
        public Tencent.TranslationResult TencentResult;
    }
}