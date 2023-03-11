using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarServis.Services.ViewModels
{
    public class CarViewModel:BaseViewModel
    {
        [Required]
        public string Make { get; set; }
        [Required]
        public string Model { get; set; }
        [Required]
        public string Year { get; set; }
        [Required]
        public string Customer { get; set; }
        public string ImageUrl { get; set; } = string.Empty;
    }
}
