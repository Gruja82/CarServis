using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarServis.Data.AbstractClasses
{
    public abstract class IdCodeEntity
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Code { get; set; }
    }
}
