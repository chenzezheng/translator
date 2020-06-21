using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TranslatorApi.Models
{
    [Table("Users")]
    public class User
    {
        [Key]
        public string UserID { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public int Wealth { get; set; }
        public List<Question> Questions { get; set; }
        public List<Answer> Answers { get; set; }
    }
}
