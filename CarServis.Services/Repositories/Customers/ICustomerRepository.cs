using CarServis.Data.Entities;
using CarServis.Services.Repositories.Generic;
using CarServis.Services.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarServis.Services.Repositories.Customers
{
    // Interface which contains declarations of all necessary methods for working with Car
    public interface ICustomerRepository:IGenericRepository<Customer>
    {
        // Custom model validation
        Task<Dictionary<string, string>> ValidateCustomerAsync(CustomerViewModel model);
        // Return all Customer records
        Task<Pagination<CustomerViewModel>> GetAllCustomersAsync();
        // Return filtered Customer records, and used when page number is chnaged or items shown per page is changed
        Task<Pagination<CustomerViewModel>> GetFilteredCustomersAsync(string searchText, int pageIndex, int pageSize);
        // Return single Customer
        Task<CustomerViewModel> GetSingleCustomerAsync(int id);
        // Create new Customer
        Task CreateNewCustomerAsync(CustomerViewModel model);
        // Update existing Customer
        Task UpdateCustomerAsync(CustomerViewModel model);
        // Delete existing Customer
        Task DeleteCustomerAsync(int id);
    }
}
