using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarServis.Services.ViewModels
{
    public class MakeViewModel:BaseViewModel
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Country { get; set; }
        public string Web { get; set; } = string.Empty;
        public string ImageUrl { get; set; } = string.Empty;
    }
}
