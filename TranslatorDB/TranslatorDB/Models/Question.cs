using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TranslatorDB.Models
{
    [Table("Questions")]
    public class Question
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
        public List<Answer> Answers { get; set; }
        public User Users { get; set; }
    }
}
