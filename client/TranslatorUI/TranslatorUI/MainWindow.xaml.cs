using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Permissions;
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
using TranslatorUI.Pages;

namespace TranslatorUI
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public Uri CommunityPage { get; set; }
        public CommunityPage CP { get; set; }
        public TransPage TP { get; set; }
        public MainWindow()
        {
            InitializeComponent();
            TP = new TransPage();
            mainFrame.Content = TP;
        }
        private void btnNav_Click(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            if (btn.Tag.ToString() == "CommunityPage")
            {
                if (CP == null)
                {
                    CP = new CommunityPage();
                    mainFrame.Content = CP;
                }
                else
                {
                    mainFrame.Content = CP;
                }
            }
            else
            {
                if (TP == null)
                {
                    TP = new TransPage();
                    mainFrame.Content = TP;
                }
                else
                {
                    mainFrame.Content = TP;
                }
            }

            //      CommunityPage = new Uri("Pages/" + btn.Tag.ToString() + ".xaml", UriKind.Relative);
            //       mainFrame.Navigate(CommunityPage);

        }
        public void SearchInCommunity(string keyword)
        {
            if (CP == null)
            {
                CP = new CommunityPage();
                mainFrame.Content = CP;
                CP.SearchKeyWord(keyword);
            }
            else
            {
                mainFrame.Content = CP;
                CP.SearchKeyWord(keyword);
            }
        }

        //      CommunityPage = new Uri("Pages/" + btn.Tag.ToString() + ".xaml", UriKind.Relative);
        //       mainFrame.Navigate(CommunityPage);

    
    }

}
