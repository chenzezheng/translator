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
using TranslatorUI.Models;
using TranslatorUI.Pages;

namespace TranslatorUI.Windows
{
    /// <summary>
    /// LogInPage.xaml 的交互逻辑
    /// </summary>
    public partial class LogInWindow : Window
    {
        public bool IsLogIn { get; set; }
        public User LoginUser { get; set; }
        public LogInWindow()
        {
            InitializeComponent();
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            LoginUser = new User();
        }

        private void SignUp_btn_Click(object sender, RoutedEventArgs e)
        {
            SignUpWindow signUp = new SignUpWindow();
            signUp.ShowDialog();
        }

        private void LogIn_btn_Click(object sender, RoutedEventArgs e)
        {
            IsLogIn = LoginUser.SignIn(inputUsername.Text, FloatingPasswordBox.Password);
            if(inputUsername.Text==""||inputUsername.Text==null)
            {
                tipWindow tw=new tipWindow("请输入用户名");
                tw.ShowDialog();
                return;
            }
            else if (FloatingPasswordBox.Password == "" || FloatingPasswordBox.Password == null)
            {
                tipWindow tw = new tipWindow("请输入密码");
                tw.ShowDialog();
                return;
            }
            if (IsLogIn == true)
                this.Close();
            else
            {
                tipWindow tw = new tipWindow("用户名或密码错误");
                tw.ShowDialog();
            }
            
        }
    }
}
