using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
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
using TranslatorUI.Models;
using TranslatorUI.Service;

namespace TranslatorUI.Windows
{
    /// <summary>
    /// SignUpWindow.xaml 的交互逻辑
    /// </summary>
    public partial class SignUpWindow : Window
    {
        public bool SignUpSuccess { get; set; }
        public SignUpWindow()
        {
            InitializeComponent();
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
        }

        private void SignUp_btn_Click(object sender, RoutedEventArgs e)
        {
            if (UserName.Text == "" || UserName.Text == null)
            {
                tipWindow tw = new tipWindow("请输入用户名");
                tw.ShowDialog();
                return;
            }
            if (!(System.Text.RegularExpressions.Regex.IsMatch(UserName.Text, @"^[a-zA-Z0-9]+$")))
            {
                tipWindow tw = new tipWindow("用户名只能为字母与数字");
                tw.ShowDialog();
                return;
            }
            else if (FloatingPasswordBox.Password == "" || FloatingPasswordBox.Password == null)
            {
                tipWindow tw = new tipWindow("请输入密码");
                tw.ShowDialog();
                return;
            }
            if (CommunityService.SignUp(UserName.Text,FloatingPasswordBox.Password))
            { SignUpSuccess = true; }
            else
            {
                SignUpSuccess = false;
                tipWindow tw = new tipWindow("用户名已注册");
                tw.ShowDialog();
            }
            if (SignUpSuccess)
                this.Close();
        }
    }
}
