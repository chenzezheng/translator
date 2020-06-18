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
        public MainWindow()
        {
            InitializeComponent();
            mainFrame.Navigate(new Uri("Pages/TransPage.xaml",UriKind.Relative));
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
                mainFrame.Content = new TransPage();
            }

              //      CommunityPage = new Uri("Pages/" + btn.Tag.ToString() + ".xaml", UriKind.Relative);
             //       mainFrame.Navigate(CommunityPage);

        }
    }

}
