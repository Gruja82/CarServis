using CarServis.Data.AbstractClasses;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarServis.Data.Entities
{
    public class Part:IdCodeEntity
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public double Price { get; set; }
        [Required]
        public int Quantity { get; set; }
        public string ImageUrl { get; set; } = string.Empty;
        public virtual ICollection<Repair> Repairs { get; set; }
    }
}
