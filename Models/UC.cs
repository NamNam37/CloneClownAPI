using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace CloneClownAPI.Models
{
    [Table("UC")]
    [Keyless]
    public class UC
    {
        public int userID { get; set; }
        public int configID { get; set; }
    }
}