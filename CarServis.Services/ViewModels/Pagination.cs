using CarServis.Services.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarServis.Services.ViewModels
{
    public class Pagination<T> where T : class
    {
        public HashSet<T> DataSet { get; set; }
        public int PageIndex { get; set; }
        public int PageCount { get; set; }
        public int PageSize { get; set; } = GlobalVariables.PAGE_SIZE;
    }
}
