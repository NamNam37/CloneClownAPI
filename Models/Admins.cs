using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace CloneClownAPI.Models
{
    [Table("Admins")]
    public class Admins
    {
        public int id { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        [Column("profile picture")]
        public string pfp { get; set; }
        public string mail { get; set; }
        [Column("include errors")]
        public bool errors { get; set; }
        [Column("include successes")]
        public bool successes { get; set; }
        public string schedule { get; set; }
    }
}