using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CloneClownAPI.Models
{
    [Table("ConfigsUsers")]
    public class ConfigsUsers
    {
        public int userID { get; set; }
        public int configID { get; set; }
        public virtual Configs config { get; set; }
        public virtual Users user { get; set; }
    }
}