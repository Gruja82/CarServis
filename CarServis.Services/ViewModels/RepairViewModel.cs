using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarServis.Services.ViewModels
{
    public class RepairViewModel:BaseViewModel
    {
        [Required]
        public string Car { get; set; }
        [Required]
        public string Part { get; set; }
        [Required]
        public DateTime RepairDate { get; set; }
        [Required]
        public int Qty { get; set; }
        [Required]
        public string Customer { get; set; }
        [Required]
        public double WorkCost { get; set; }
        public double Charge { get; set; } = 0.0;
    }
}
