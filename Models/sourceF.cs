using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace CloneClownAPI.Models
{
    [Table("SourceF")]
    public class SourceF
    {
        public int id { get; set; }
        public int configID { get; set; }
        public string path { get; set; }
    }
}