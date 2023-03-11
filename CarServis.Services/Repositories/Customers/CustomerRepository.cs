using CarServis.Data.Database;
using CarServis.Data.Entities;
using CarServis.Services.Extensions;
using CarServis.Services.Repositories.Generic;
using CarServis.Services.Utility;
using CarServis.Services.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;

namespace CarServis.Services.Repositories.Customers
{
    // Implementation class for customers
    // It inherits GenericRepository<> class which contains all basic CRUD methods,
    // and implements ICustomerRepository interface for specific implementation 
    // concerning specific Entity
    public class CustomerRepository:GenericRepository<Customer>,ICustomerRepository
    {
        // Through class constructor, pass AppDbContext instance to parent class constructor
        public CustomerRepository(AppDbContext context):base(context)
        {

        }

        // Create new Customer
        public async Task CreateNewCustomerAsync(CustomerViewModel model)
        {
            // Create new Customer object
            Customer newCustomer = new();

            // Set it's properties to the ones in model
            newCustomer.Code = model.Code;
            newCustomer.FirstName = model.FirstName;
            newCustomer.LastName = model.LastName;
            newCustomer.Address = model.Address;
            newCustomer.City = model.City;
            newCustomer.Postal = model.Postal;
            newCustomer.Phone = model.Phone;
            newCustomer.Email = model.Email;

            // Cal parent class method for creating new Customer
            await CreateNewAsync(newCustomer);
        }

        // Delete existing Customer
        public async Task DeleteCustomerAsync(int id)
        {
            // Call parent class method for deleting existing Customer
            await DeleteAsync(id);
        }

        // Return all Customer records
        public async Task<Pagination<CustomerViewModel>> GetAllCustomersAsync()
        {
            // Create new HashSet with CustomerViewModel objects
            HashSet<CustomerViewModel> viewModelSet = new();

            // Iterate through all Customer records by calling parent class's generic method for returning all records
            // and populate HashSet by calling extension method for converting Customer to CustomerViewModel
            foreach (var customer in await GetAllAsync())
            {
                viewModelSet.Add(customer.ConvertCustomerToCustomerViewModel());
            }

            // Call static method for returning Pagination class with populated HashSet
            return PaginationUtility<CustomerViewModel>.GetPagination(in viewModelSet);
        }

        // Return customers filtered by search therm, when page number is changed or when number of shown items per page is changed
        public async Task<Pagination<CustomerViewModel>> GetFilteredCustomersAsync(string searchText, int pageIndex, int pageSize)
        {
            // Create and populate HashSet with all Customer records
            HashSet<Customer> allCustomers=(await GetAllAsync()).ToHashSet();

            // Check if the Search operation is performed
            // If it is, then filter allCustomers by searchText
            if (searchText != null && searchText != string.Empty)
            {
                allCustomers=allCustomers.Where(e=>e.Code.ToLower().Contains(searchText.ToLower())
                    || e.FirstName.ToLower().Contains(searchText.ToLower())
                    || e.LastName.ToLower().Contains(searchText.ToLower()))
                    .ToHashSet();
            }

            // Create new HashSet with CustomerViewModel objects
            HashSet<CustomerViewModel> viewModelSet = new();

            // Iterate through all Customer records by calling parent class's generic method for returning all records
            // and populate HashSet by calling extension method for converting Customer to CustomerViewModel
            foreach (var customer in allCustomers)
            {
                viewModelSet.Add(customer.ConvertCustomerToCustomerViewModel());
            }

            // Call static method for returning Pagination class with populated HashSet
            return PaginationUtility<CustomerViewModel>.GetPagination(in viewModelSet, pageIndex, pageSize);
        }

        // Return single Customer record
        public async Task<CustomerViewModel> GetSingleCustomerAsync(int id)
        {
            // Call parent class method for returning single Customer record
            Customer customer = await GetSingleAsync(id);

            // Return CustomerViewModel by calling extension method for converting Customer to CustomerViewModel
            return customer.ConvertCustomerToCustomerViewModel();
        }

        // Update existing Customer
        public async Task UpdateCustomerAsync(CustomerViewModel model)
        {
            // Find Customer record by Id
            Customer customer=await GetSingleAsync(model.Id);

            // Set customer properties to the ones contained in model
            customer.Code = model.Code;
            customer.FirstName=model.FirstName;
            customer.LastName=model.LastName;
            customer.Address=model.Address;
            customer.City=model.City;
            customer.Postal = model.Postal;
            customer.Phone = model.Phone;
            customer.Email = model.Email;

            // Call parent class method for updating selected Customer record
            UpdateRecord(customer);
        }

        // Custom model validation
        public async Task<Dictionary<string, string>> ValidateCustomerAsync(CustomerViewModel model)
        {
            // Create new Dictionary<string, string> which will hold possible model errors
            Dictionary<string, string> errors = new();

            // Create and populate HashSet with all Customer records
            HashSet<Customer> allCustomers = (await GetAllAsync()).ToHashSet();

            // If model contains Id (i.e Id is larger than 0), it means that Customer is used in Update operation
            if (model.Id > 0)
            {
                // Find single Customer record by Id
                Customer customerRecord = await GetSingleAsync(model.Id);

                // Check if the Customer's Code value is changed
                if (customerRecord.Code != model.Code)
                {
                    // If it is changed, then check for Code uniqueness within all Customer records in database
                    if (allCustomers.Select(e => e.Code.ToLower()).Contains(model.Code.ToLower()))
                    {
                        // If supplied Code is already used by another Customer, then add error to errors Dictionary
                        errors.Add("Code", "There is already Customer with this Code in database. Please provide different one!");
                    }
                }
            }
            // Otherwise, it is used in Create operation
            else
            {
                if (allCustomers.Select(e => e.Code.ToLower()).Contains(model.Code.ToLower()))
                {
                    // If supplied Code is already used by another Customer, then add error to errors Dictionary
                    errors.Add("Code", "There is already Customer with this Code in database. Please provide different one!");
                }
            }

            // Return errors Dictionary
            return errors;
        }
    }
}
