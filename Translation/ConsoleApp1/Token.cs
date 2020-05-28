using System;


namespace Translation
{
    [Serializable]
    public class Token
    {
        public string TokenID { get; set; }
        public string UserID { get; set; }

        public Token()
        {
            TokenID = "null";
            UserID = "null";
        }
        public Token(string tokenid,string userid)
        {
            TokenID = tokenid;
            UserID = userid;
        }
        public override string ToString()
        {
            return "令牌:" + TokenID + "用户名:" + UserID;
        }
    }
}
