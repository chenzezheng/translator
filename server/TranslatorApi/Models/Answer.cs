using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TranslatorApi.Models
{
    [Table("Answers")]
    public class Answer
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]  //设置自增
        public int AnswerID { get; set; }
        [Required]
        public string Content { get; set; }
        [Required]
        public DateTime Acreatetime { get; set; }
        [Required]
        public bool Isadopted { get; set; }
        public string UserID { get; set; }
        public int QuestionID { get; set; }
        public int Likes { get; set; }
    }
}
