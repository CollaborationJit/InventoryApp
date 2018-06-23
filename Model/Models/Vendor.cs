using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Model.Models
{
    public class Vendor
    {
        [Key]
        [ForeignKey("Vendor")]
        public string Name { get; set; }
        public virtual Contact Contact { get; set; }
    }
}
