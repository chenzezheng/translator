using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TranslatorDB.Models
{
    [Table("Answers")]
    public class Answer
    {
        [Key]
        public int AnswerID { get; set; }
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
        public User Users { get; set; }
        public Question Questions { set; get; }
    }
}
