using CarServis.Data.AbstractClasses;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarServis.Data.Entities
{
    public class Car:IdCodeEntity
    {
        public int MakeId { get; set; }
        [Required]
        public string Model { get; set; }
        public string Year { get; set; }
        public int CustomerId { get; set; }
        public string ImageUrl { get; set; } = string.Empty;
        public virtual Make Make { get; set; } = null!;
        public virtual Customer Customer { get; set; } = null!;
    }
}
