using CarServis.Data.AbstractClasses;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarServis.Data.Entities
{
    public class Repair:IdCodeEntity
    {
        public int CarId { get; set; }
        public int PartId { get; set; }
        [Required]
        public DateTime RepairDate { get; set; }
        [Required]
        public int Qty { get; set; }
        public int CustomerId { get; set; }
        [Required]
        public double WorkCost { get; set; }
        public virtual Part Part { get; set; } = null!;
        public virtual Car Car { get; set; } = null!;
        public virtual Customer Customer { get; set; } = null!;
    }
}
