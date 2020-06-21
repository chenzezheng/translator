using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Windows.Media.Imaging;

namespace XunfeiOCR
{
    public class ScreenShotImage
    {
        /*public static void Save(BitmapSource source, string filename)
        {
            Bitmap myBitmap;
            using (System.IO.MemoryStream ms = new System.IO.MemoryStream())
            {
                BitmapEncoder encoder = new BmpBitmapEncoder();
                encoder.Frames.Add(BitmapFrame.Create(source));
                encoder.Save(ms);
                myBitmap = new System.Drawing.Bitmap(ms);
            }
            ImageCodecInfo myImageCodecInfo;
            Encoder myEncoder;
            EncoderParameter myEncoderParameter;
            EncoderParameters myEncoderParameters;

            // 获取JPEG编码
            myImageCodecInfo = GetEncoderInfo("image/jpeg");

            myEncoder = Encoder.Quality;
            myEncoderParameters = new EncoderParameters(1);

            // 保存bitmap
            myEncoderParameter = new EncoderParameter(myEncoder, 75L);
            myEncoderParameters.Param[0] = myEncoderParameter;
            myBitmap.Save(filename, myImageCodecInfo, myEncoderParameters);
        }
        private static ImageCodecInfo GetEncoderInfo(String mimeType)
        {
            int j;
            ImageCodecInfo[] encoders;
            encoders = ImageCodecInfo.GetImageEncoders();
            for (j = 0; j < encoders.Length; ++j)
            {
                if (encoders[j].MimeType == mimeType)
                    return encoders[j];
            }
            return null;
        }*/

        /// <summary>
        /// 保存图片到文件
        /// </summary>
        /// <param name="image">图片数据</param>
        /// <param name="filePath">保存路径</param>
        public static void SaveImageToFile(BitmapSource image, string filePath)
        {
            BitmapEncoder encoder = GetBitmapEncoder(filePath);
            encoder.Frames.Add(BitmapFrame.Create(image));

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                encoder.Save(stream);
            }
        }

        /// <summary>
        /// 根据文件扩展名获取图片编码器
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <returns>图片编码器</returns>
        private static BitmapEncoder GetBitmapEncoder(string filePath)
        {
            var extName = Path.GetExtension(filePath).ToLower();
            if (extName.Equals(".png"))
            {
                return new PngBitmapEncoder();
            }
            else
            {
                return new JpegBitmapEncoder();
            }
        }
    }
}