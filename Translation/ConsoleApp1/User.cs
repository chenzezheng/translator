using System;



namespace Translation
{
    [Serializable]
    public class User
    {
        public string UserID { get; set; }
        public string Password { get; set; }
        public int Wealth { get; set; }
        public User()
        {
            UserID = "null";
            Password = "null";
            Wealth = 0;
        }
        public User(string userid, string password,int wealth)
        {
            UserID = userid;
            Password = password;
            Wealth = wealth;
        }
        public override string ToString()
        {
            return "用户名:" + UserID + "密码:" + Password + "资产:" + Wealth;
        }
    }
}
