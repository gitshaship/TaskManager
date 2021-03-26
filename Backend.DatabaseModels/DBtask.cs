using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.Models
{
    public class DBtask
    {

        [Key]
        public int Id { get; set; }

        [Column(TypeName = "nvarchar(50)")]
        public string TaskName { get; set; }
        [Column(TypeName = "nvarchar(100)")]
        public string Description { get; set; }
        public int Status { get; set; }
       

    }
}
