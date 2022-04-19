using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CloneClownAPI.Models
{
    [Table("FTP")]
    public class FTP
    {
        
        public int id { get; set; }
        public int destID { get; set; }
        public string login { get; set; }
        public string password { get; set; }
        public string hostname { get; set; }
        public virtual DestF DestF { get; set; }
    }
}