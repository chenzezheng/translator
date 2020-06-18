using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
    public partial class TransPage : UserControl
    {
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
        }
    }

}
