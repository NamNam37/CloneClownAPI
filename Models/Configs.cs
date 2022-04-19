using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace CloneClownAPI.Models
{
    [Table("Configs")]
    public class Configs
    {
        public int id { get; set; }
        [Column("last used")]
        public DateTime last_used { get; set; }
        public string schedule { get; set; }
        public enum Type
        {
            full,
            differencial,
            incremental
        }
        public string type { get; set; }
        public int backupCount { get; set; }
        public int packageCount { get; set; }
        public bool isZIP { get; set; }

        //public virtual List<Users> users { get; set; }
        public virtual List<Logs> logs { get; set; }
        public virtual List<SourceF> sources { get; set; }
        public virtual List<DestF> dests { get; set; }
    }
}