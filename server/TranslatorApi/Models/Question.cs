using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TranslatorApi.Models
{
    [Table("Questions")]
    public class Question
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]  //设置自增
        public int QuestionID { get; set; }
        [Required]
        public string Content { get; set; }
        [Required]
        public int Reward { get; set; }
        [Required]
        public DateTime Qcreatetime { get; set; }
        [Required]
        public bool Adopted { get; set; }
        public string UserID { get; set; }
        public List<Answer> Answers { get; set; }
    }
}
