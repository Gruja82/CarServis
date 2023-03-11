using CarServis.Data.AbstractClasses;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarServis.Data.Entities
{
    public class Make:IdCodeEntity
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Country { get; set; }
        public string Web { get; set; } = string.Empty;
        public string ImageUrl { get; set; } = string.Empty;
        public virtual ICollection<Car> Cars { get; set; }
    }
}
