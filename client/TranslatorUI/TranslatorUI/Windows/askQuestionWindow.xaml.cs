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
using TranslatorUI.Models;

namespace TranslatorUI.Windows
{
    /// <summary>
    /// askQuestionWindow.xaml 的交互逻辑
    /// </summary>
    public partial class askQuestionWindow : Window
    {
        public bool HasAsked { get; set; }
        public int Reward { get; set; }
        public string QContent { get; set; }
        public int Coin { get; set; }
        public askQuestionWindow(User user, string text)
        {
            InitializeComponent();
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            HasAsked = false;
            Coin = user.Coin;
            ContentTextBox.Text = text;
        }

        private void Ask_Click(object sender, RoutedEventArgs e)
        {
            if (RewardTextBox.Text == "" || RewardTextBox.Text == null)
            {
                tipWindow tw = new tipWindow("请输入正确的积分");
                tw.ShowDialog(); 
                return;
            }
            else 
            { 
                int i;
                if (!int.TryParse(RewardTextBox.Text, out i))
                {
                    tipWindow tw = new tipWindow("请输入正确的积分");
                    tw.ShowDialog();
                    return;
                }
                else
                {
                    if (i <= 0)
                    {
                        tipWindow tw = new tipWindow("请输入正确的积分");
                        tw.ShowDialog();
                        return;
                    }
                }
            }
            if (ContentTextBox.Text == "" || ContentTextBox.Text == null)
            {
                tipWindow tw = new tipWindow("请输入正确的提问");
                tw.ShowDialog();
                return;
            }
            Reward = Convert.ToInt32(RewardTextBox.Text);
            if(Reward>Coin)
            {
                tipWindow tw = new tipWindow("剩余积分不足,当前剩余:"+Coin);
                tw.ShowDialog();
                return;
            }
            QContent = ContentTextBox.Text;
            HasAsked = true;
            this.Close();
        }
    }
}
