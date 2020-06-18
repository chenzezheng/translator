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
using System.Windows.Shapes;

namespace TranslatorUI.Windows
{
    /// <summary>
    /// tipWindow.xaml 的交互逻辑
    /// </summary>
    public partial class tipWindow : Window
    {
        public tipWindow()
        {
            InitializeComponent();
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
        }
        public tipWindow(string warning)
        {
            InitializeComponent();
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            this.Warning.Text = warning;
        }

        private void signUp_btn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
