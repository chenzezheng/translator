﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TranslatorDB.Models
{
    [Table("Tokens")]
    public class Token
    {
        [Key]
        public string TokenID { get; set; }
        public string UserID { get; set; }
        [ForeignKey("UserID")]
        public User Users { get; set; }
    }
}
