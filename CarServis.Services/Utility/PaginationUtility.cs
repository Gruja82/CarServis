using CarServis.Services.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarServis.Services.Utility
{
    // The purpose of this static class is to calculate number of pages based on number
    // of records
    public static class PaginationUtility<T> where T : class
    {
        public static Pagination<T> GetPagination(in HashSet<T> dataSet,int currentPage=GlobalVariables.CURRENT_PAGE,int pageSize = GlobalVariables.PAGE_SIZE)
        {
            var pagination = new Pagination<T>();

            pagination.DataSet = (from c in dataSet select c)
                .Skip((currentPage - 1) * pageSize)
                .Take(pageSize)
                .ToHashSet();

            double pageCount = (double)((decimal)dataSet.Count / Convert.ToDecimal(pageSize));

            pagination.PageCount = (int)Math.Ceiling(pageCount);

            pagination.PageIndex = currentPage;

            return pagination;
        }
    }
}
