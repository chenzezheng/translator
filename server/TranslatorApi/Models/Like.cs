using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TranslatorApi.Models
{
    [Table("Likes")]
    public class Like
    {
        [Key]
        public string Userid { get; set; }
        [Key]
        public int AnswerID { get; set; }
    }
}
