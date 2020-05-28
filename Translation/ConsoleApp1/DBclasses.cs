using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Translation
{
    [Table("Users")]
    public class DB_User
    {
        [Key]
        public string UserID { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public int Wealth { get; set; }
        public List<DB_Token> Token { get; set; }
        public List<DB_Question> Question { get; set; }
        public List<DB_Answer> Answer { get; set; }
    }

    [Table("Tokens")]
    public class DB_Token
    {
        [Key]
        public string TokenID { get; set; }
        public string UserID { get; set; }
        [ForeignKey("UserID")]
        public DB_User User { get; set; }
    }

    [Table("Questions")]
    public class DB_Question
    {
        [Key]
        public int QuestionID { get; set; }
        [Required]
        public string Content { get; set; }
        [Required]
        public int Reward { get; set; }
        [Required]
        public DateTime Qcreatetime { get; set; }
        public string UserID { get; set; }
        [ForeignKey("UserID")]
        public int AnswerID { get; set; }
        [ForeignKey("AnswerID")]
        public List<DB_Answer> Answer { get; set; }
        public DB_User User { get; set; }
    }

    [Table("Answers")]
    public class DB_Answer
    {
        [Key]
        public int AnswerID { get; set;}
        [Required]
        public string Content { get; set; }
        public string UserID { get; set; }
        [ForeignKey("UserID")]
        public int QuestionID { get; set; }
        [ForeignKey("QuestionID")]
        [Required]
        public DateTime Acreatetime { get; set; }
        [Required]
        public bool Isadopted { get; set; }
        public DB_User User { get; set; }
        public DB_Question Question { set; get; }
    }
}
