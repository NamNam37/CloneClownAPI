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
        public string pfp { get; set; }
        public string email { get; set; }
        public int mailsSent { get; set; }
        public bool errors { get; set; }
        public bool successes { get; set; }
        public string schedule { get; set; }
    }
}