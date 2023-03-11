using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarServis.Services.ViewModels
{
    public class BaseViewModel
    {
        public int Id { get; set; }
        [Required]
        public string Code { get; set; }
        public IFormFile Image { get; set; } = null;
    }
}
