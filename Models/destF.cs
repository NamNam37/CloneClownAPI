using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace CloneClownAPI.Models
{
    [Table("DestF")]
    public class DestF
    {
        public int id { get; set; }
        public int configID { get; set; }
        public string path { get; set; }
        public enum Type
        {
            local,
            ftp
        }
        public string type { get; set; }

        public string login { get; set; }
        public string password { get; set; }
        public string hostname { get; set; }
        public int port { get; set; }
        public virtual Configs config { get; set; }
    }
}