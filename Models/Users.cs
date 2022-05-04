using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace CloneClownAPI.Models
{
    [Table("Users")]
    public class Users
    {
        public int id { get; set; }
        public string username { get; set; }
        public string IP { get; set; }
        public bool online { get; set; }
        [Column("last backup")]
        public DateTime last_backup { get; set; }

        public virtual ICollection<Configs> configs { get; set; }
        public virtual ICollection<ConfigsUsers> configsUsers { get; set; }
        public virtual ICollection<Logs> logs { get; set; }
        
    }
}