using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace TranslatorUI.Pages
{
    /// <summary>
    /// TransPage.xaml 的交互逻辑
    /// </summary>
    public partial class TransPage : Page
    {
        private readonly RisCaptureLib.ScreenCaputre screenCaputre = new RisCaptureLib.ScreenCaputre();
        private Size? lastSize;
        List<string> language = new List<string>();
        public TransPage()
        {
            InitializeComponent();
            language.Add("Chinese");
            language.Add("English");
            language.Add("Spanish");
            language.Add("Japanese");
            language.Add("French");
            this.languageBeforeTrans.DataContext = language;
            this.languageAfterTrans.ItemsSource = language;

            screenCaputre.ScreenCaputred += OnScreenCaputred;
            screenCaputre.ScreenCaputreCancelled += OnScreenCaputreCancelled;
        }

        private void OnScreenCaputreCancelled(object sender, System.EventArgs e)
        {
            var parentWin = Window.GetWindow(this);
            parentWin.Show(); //这里是显示当前页面
            Focus();
        }

        private void OnScreenCaputred(object sender, RisCaptureLib.ScreenCaputredEventArgs e)
        {
            //set last size
            lastSize = new Size(e.Bmp.Width, e.Bmp.Height);


            var parentWin = Window.GetWindow(this);
            parentWin.Show(); //这里是显示当前页面

            //文字识别OCR
            XunfeiOCR.ScreenShotImage.Save(e.Bmp, "temp.jpg");
            textBeforeTrans.Text = XunfeiOCR.OCR.GetOCRResult("temp.jpg");
        }
        private void cutScreen_btn_Click(object sender, RoutedEventArgs e)
        {
             //这里是隐藏当前页面
            var parentWin = Window.GetWindow(this);
            parentWin.Hide();
            Thread.Sleep(300);
            screenCaputre.StartCaputre(30, lastSize);
        }

        private void CommunityTrans_btn_Click(object sender, RoutedEventArgs e)
        {

        }
    }

}
