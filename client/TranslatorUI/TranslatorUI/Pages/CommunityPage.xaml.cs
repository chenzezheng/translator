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
using TranslatorUI.Windows;
using TranslatorUI.Service;
using System.Security.Cryptography;

namespace TranslatorUI.Pages
{
    /// <summary>
    /// CommunityPage.xaml 的交互逻辑
    /// </summary>
    public partial class CommunityPage : Page
    {
        public User User = new User();

        public List<Question> QuestionList { get; set; }
        public List<DisplayAnswer> DisplayAnswers { get; set; }
        public int CurrentQuesIndex { get; set; }
        public Question CurrentQuestion { get; set; }
     public int Page { get; set; }
        public int CurrentQList { get; set; }  //1:全部提问 2：我的提问 3：我的回答 4：搜索
        public CommunityPage()
        {
            InitializeComponent();
            this.CurrentQList = 1;
            this.Page =1;
            PageLabel.Text = "当前页面:"+Page.ToString();
            this.QuestionList = CommunityService.GetAllQuestions(Page);
            this.QuesItem.ItemsSource = QuestionList;
            //  this.QuesItem.ItemsSource = QuestionList;
            //  this.AnsItem.ItemsSource = AnswerList;
            // this.ques_info.DataContext = QuestionList[0];
        }
        private void Login_Click(object sender, RoutedEventArgs e)                                 //登录按钮
        {
            LogInWindow logIn = new LogInWindow();
            logIn.ShowDialog();
            if (logIn.IsLogIn == true)
            {
                this.User = logIn.LoginUser;
                LoginButton.Visibility = Visibility.Hidden;
                userInfo.Visibility = Visibility.Visible;
                coin.Visibility = Visibility.Visible;
                userInfo.Text = "当前用户：" + User.UserId;
                coin.Text = "积分：" + User.Coin.ToString();
            }
        }

        private void QuesItem_SelectionChanged(object sender, SelectionChangedEventArgs e)           //显示问题详情与回答
        {
            if (QuesItem.SelectedIndex < 0)
                return;
            CurrentQuesIndex = QuesItem.SelectedIndex;
            CurrentQuestion = QuestionList[QuesItem.SelectedIndex];
            this.ques_info.DataContext = QuestionList[QuesItem.SelectedIndex];
            this.DisplayAnswers = new List<DisplayAnswer>();
            foreach(Answer answer in QuestionList[QuesItem.SelectedIndex].Answers)
            {
                DisplayAnswer displayAnswer = new DisplayAnswer();
                displayAnswer.convert(answer);
                if (QuestionList[QuesItem.SelectedIndex].UserId == User.UserId && QuestionList[QuesItem.SelectedIndex].Solved == false)
                {
                    displayAnswer.ShowAdoptBtn = true;
                }
                else { displayAnswer.ShowAdoptBtn = false; }
                if (answer.UserId == User.UserId)
                {
                    displayAnswer.IsMyAnswer = true;
                }
                else { displayAnswer.IsMyAnswer = false; }
                this.DisplayAnswers.Add(displayAnswer);
            }
            this.AnsItem.ItemsSource = this.DisplayAnswers;
        }

        private void Adopt_btn_Click(object sender, RoutedEventArgs e)                       //采纳按钮
        {
            var curItem = ((ListBoxItem)AnsItem.ContainerFromElement((Button)sender)).Content;
            DisplayAnswer disp = curItem as DisplayAnswer;
            bool success = User.Adopt(disp.AnswerId,CurrentQuestion.QuestionId);
            if (success == true)
            {
                foreach (DisplayAnswer a in DisplayAnswers)
                {
                    a.ShowAdoptBtn = false;
                    if (a.AnswerId == disp.AnswerId)
                    { a.IsAdopted = true; }
                }
                QuestionList[QuesItem.SelectedIndex].Solved = true;
                foreach (Answer a in QuestionList[QuesItem.SelectedIndex].Answers)
                {
                    if (a.AnswerId == disp.AnswerId)
                        a.IsAdopted = true;
                }
                this.QuesItem.ItemsSource = null;
                this.AnsItem.ItemsSource = null;
                this.AnsItem.ItemsSource = this.DisplayAnswers;
                this.QuesItem.ItemsSource = this.QuestionList;
            }
            else
            {
                tipWindow warning = new tipWindow("未知错误");
                warning.ShowDialog();
                return;
            }
        }

        private void ToggleButton_Click(object sender, RoutedEventArgs e)                             //点赞
        {
            if(User.UserId==null)
            {
                tipWindow warning = new tipWindow("请先登录");
                warning.ShowDialog();
                return;
            }

            var curItem = ((ListBoxItem)AnsItem.ContainerFromElement((System.Windows.Controls.Primitives.ToggleButton)sender)).Content;
            DisplayAnswer disp = curItem as DisplayAnswer;

            if(disp.UserId==User.UserId)
            {
                tipWindow warning = new tipWindow("无法给自己点赞");
                warning.ShowDialog();
                return;
            }
            bool success = User.Like(disp.AnswerId);
            if (success)
            {
                foreach (DisplayAnswer da in DisplayAnswers)
                {
                    if (da.AnswerId == disp.AnswerId)
                        disp.Like++;
                }
                foreach (Answer a in QuestionList[CurrentQuesIndex].Answers)
                {
                    if (a.AnswerId == disp.AnswerId)
                        a.Like++;
                }

                this.AnsItem.ItemsSource = null;
                this.AnsItem.ItemsSource = this.DisplayAnswers;
            }
            else
            {
                tipWindow warning = new tipWindow("已经点过赞了");
                warning.ShowDialog();
                return;
            }
        }

        private void Answer_Click(object sender, RoutedEventArgs e)                            //回答
        {
            
            if (CurrentQuestion == null)
                return;
            else if(User.UserId==null)
            {
                tipWindow warning = new tipWindow("请先登录");
                warning.ShowDialog();
                return;
            }
            else if(CurrentQuestion.UserId==User.UserId)
            {
                tipWindow warning = new tipWindow("不能回答自己的提问");
                warning.ShowDialog();
                return;
            }
            else if(AnswerContent.Text==null||AnswerContent.Text=="")
            {
                tipWindow warning = new tipWindow("回答不能为空");
                warning.ShowDialog();
                return;
            }
            else if(DisplayAnswers.Count>=15)
            {
                tipWindow warning = new tipWindow("回答已满");
                warning.ShowDialog();
                return;
            }
            bool success = User.Answer(AnswerContent.Text,CurrentQuestion.QuestionId);
            if (success)
            {
                Answer newAnswer = new Answer(AnswerContent.Text, DateTime.Now, User.UserId, false, -1, 0);
                QuestionList[CurrentQuesIndex].Answers.Add(newAnswer);
                DisplayAnswer disp = new DisplayAnswer();
                disp.convert(newAnswer);
                disp.ShowAdoptBtn = false;
                disp.IsMyAnswer = true;
                this.DisplayAnswers.Add(disp);
                AnswerContent.Text = "";
                this.AnsItem.ItemsSource = null;
                this.AnsItem.ItemsSource = this.DisplayAnswers;
            }
            else
            {
                tipWindow warning = new tipWindow("未知错误");
                warning.ShowDialog();
                return;
            }

        }

        private void AskQuestionBtn_Click(object sender, RoutedEventArgs e)         //提问
        {
            if(User.UserId==null)
            {
                tipWindow warning = new tipWindow("请先登录");
                warning.ShowDialog();
                return;
            }
            askQuestionWindow ask = new askQuestionWindow(User, Keyword.Text);
            ask.ShowDialog();
            if (ask.HasAsked == true)
            {
                bool success = User.Ask(ask.QContent, ask.Reward);
                if (success)
                {
                    Question newQuestion = new Question(ask.QContent, ask.Reward, User.UserId, false, DateTime.Now, new List<Answer>());
                    QuestionList.Insert(0, newQuestion);
                    if (QuestionList.Count > 15)
                        QuestionList.RemoveAt(14);
                    this.User.Coin -= ask.Reward;
                    this.QuesItem.ItemsSource = null;
                    this.QuesItem.ItemsSource = QuestionList;
                    CurrentQuestion = QuestionList[0];
                    this.ques_info.DataContext = QuestionList[0];
                    this.AnsItem.ItemsSource = new List<DisplayAnswer>();
                    coin.Text = "积分：" + User.Coin.ToString();
                }
                else
                {
                    tipWindow warning = new tipWindow("未知错误");
                    warning.ShowDialog();
                    return;
                }
            }
        }

        private void AllQuestion_Click(object sender, RoutedEventArgs e)            //所有提问
        {
            this.Page = 1;
            this.CurrentQList = 1;
            Keyword.Text = "";
            PageLabel.Text = "当前页面:" + Page.ToString();
            this.QuestionList = CommunityService.GetAllQuestions(Page);
            this.QuesItem.ItemsSource = null;
            this.QuesItem.ItemsSource = QuestionList;
        }

        private void MyQuestion_Click(object sender, RoutedEventArgs e)     //我的提问
        {
            if (User.UserId == null)
            {
                tipWindow warning = new tipWindow("请先登录");
                warning.ShowDialog();
                return;
            }
            Keyword.Text = "";
            this.Page = 1;
            this.CurrentQList = 2;
            PageLabel.Text = "当前页面:" + Page.ToString();
            this.QuestionList = this.User.GetMyQuestions(this.Page);
            this.QuesItem.ItemsSource = null;
            this.QuesItem.ItemsSource = QuestionList;
        }
        private void myAnswer_Click(object sender, RoutedEventArgs e)   //我回答过的提问
        {
            if (User.UserId == null)
            {
                tipWindow warning = new tipWindow("请先登录");
                warning.ShowDialog();
                return;
            }
            Keyword.Text = "";
            this.Page = 1;
            this.CurrentQList = 3;
            PageLabel.Text = "当前页面:" + Page.ToString();
            this.QuestionList = this.User.GetMyAnswers(this.Page);
            this.QuesItem.ItemsSource = null;
            this.QuesItem.ItemsSource = QuestionList;
        }
        private void PreviewPage_Click(object sender, RoutedEventArgs e)   //上一页
        {
            if(this.Page==1)
            {
                tipWindow warning = new tipWindow("已经是第一页了");
                warning.ShowDialog();
                return;
            }
            this.Page--;
            PageLabel.Text = "当前页面:" + Page.ToString();
            switch (this.CurrentQList)
            {
                case 1:
                    this.QuestionList = CommunityService.GetAllQuestions(Page);
                    break;
                case 2:
                    this.QuestionList = this.User.GetMyQuestions(this.Page);
                    break;
                case 3:
                    this.QuestionList = this.User.GetMyAnswers(this.Page);
                    break;
                case 4:
                    this.QuestionList = CommunityService.SearchQuestion(Keyword.Text,this.Page);
                    break;
            }
            this.QuesItem.ItemsSource = null;
            this.QuesItem.ItemsSource = QuestionList;
        }
        private void NextPage_Click(object sender, RoutedEventArgs e)            //下一页
        {
            this.Page++;
            List<Question> qlist;
            bool isSuccess=true;
            switch (this.CurrentQList)
            {
                case 1:
                    qlist= CommunityService.GetAllQuestions(Page);
                    if (qlist == null)
                    {
                        isSuccess = false;
                        break;
                    }
                    this.QuestionList = qlist;
                    isSuccess = true;
                    break;
                case 2:
                    qlist = this.User.GetMyQuestions(this.Page);
                    if (qlist == null)
                    {
                        isSuccess = false;
                        break;
                    }
                    this.QuestionList = qlist;
                    isSuccess = true;
                    break;
                case 3:
                    qlist = this.User.GetMyAnswers(this.Page);
                    if(qlist==null)
                    {
                        isSuccess = false;
                        break;
                    }
                    this.QuestionList = qlist;
                    isSuccess = true;
                    break;
                case 4:
                    qlist = CommunityService.SearchQuestion(Keyword.Text, this.Page);
                    if (qlist == null)
                    {
                        isSuccess = false;
                        break;
                    }
                    this.QuestionList = qlist;
                    isSuccess = true;
                    break;
            }
            if(isSuccess==false)
            {
                this.Page--;
                tipWindow warning = new tipWindow("没有更多了");
                warning.ShowDialog();
                return;
            }
            PageLabel.Text = "当前页面:" + Page.ToString();
            this.QuesItem.ItemsSource = null;
            this.QuesItem.ItemsSource = QuestionList;
        }
        private void Search_Click(object sender, RoutedEventArgs e)            //搜索
        {
            if (Keyword.Text == null || Keyword.Text == "")
                return;
            this.Page = 1;
            this.CurrentQList = 4;
            PageLabel.Text = "当前页面:" + Page.ToString();
            this.QuestionList = CommunityService.SearchQuestion(Keyword.Text,this.Page);
            this.QuesItem.ItemsSource = null;
            this.QuesItem.ItemsSource = QuestionList;
        }
        public void SearchKeyWord(string keyword)
        {
            Keyword.Text = keyword;
            this.Page = 1;
            this.CurrentQList = 4;
            PageLabel.Text = "当前页面:" + Page.ToString();
            this.QuestionList = CommunityService.SearchQuestion(Keyword.Text, this.Page);
            this.QuesItem.ItemsSource = null;
            this.QuesItem.ItemsSource = QuestionList;
        }

    }

}
