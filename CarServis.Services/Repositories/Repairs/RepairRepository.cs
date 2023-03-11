using CarServis.Data.Database;
using CarServis.Data.Entities;
using CarServis.Services.Extensions;
using CarServis.Services.Repositories.Generic;
using CarServis.Services.Utility;
using CarServis.Services.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarServis.Services.Repositories.Repairs
{
    // Implementation class for repairs
    // It inherits GenericRepository<> class which contains all basic CRUD methods,
    // and implements IRepairRepository interface for specific implementation 
    // concerning specific Entity
    public class RepairRepository:GenericRepository<Repair>,IRepairRepository
    {
        // Through class constructor, pass AppDbContext instance to parent class constructor
        public RepairRepository(AppDbContext context):base(context)
        {

        }

        // Return car make and model
        public async Task<HashSet<string>> ReturnCarMakeAndModel()
        {
            var allCars = (await context.Cars
                .Include(e => e.Make)
                .ToListAsync())
                .ToHashSet();

            HashSet<string> cars = new();

            foreach (var car in allCars)
            {
                StringBuilder sb = new();
                sb.Append(car.Make.Name);
                sb.Append(" ");
                sb.Append(car.Model);

                cars.Add(sb.ToString());
            }

            return cars;
        }

        // Create new Repair
        public async Task CreateNewRepairAsync(RepairViewModel model)
        {
            // Create new Repair object
            Repair newRepair = new();

            // Set it's properties to the ones in model
            newRepair.Code = model.Code;
            newRepair.Car = await context.Cars.FirstOrDefaultAsync(e => (e.Make.Name + " " + e.Model) == model.Car);
            newRepair.Part = await context.Parts.FirstOrDefaultAsync(e => e.Name == model.Part);
            newRepair.RepairDate = model.RepairDate;
            newRepair.Qty = model.Qty;
            newRepair.WorkCost = model.WorkCost;
            newRepair.Customer=await context.Customers.FirstOrDefaultAsync(e=>(e.FirstName + " " + e.LastName + " " + e.Code)==model.Customer);

            // Cal parent class method for creating new Repair
            await CreateNewAsync(newRepair);
        }

        // Delete existing Repair
        public async Task DeleteRepairAsync(int id)
        {
            // Call parent class method for deleting existing Repair
            await DeleteAsync(id);
        }

        // Return all Repair records
        public async Task<Pagination<RepairViewModel>> GetAllRepairsAsync()
        {
            // Create and populate HashSet with Repair records
            HashSet<Repair> allRepairs = (await context.Repairs
                .Include(e => e.Car)
                .ThenInclude(e => e.Make)
                .Include(e => e.Part)
                .Include(e => e.Customer)
                .AsNoTracking()
                .ToListAsync())
                .ToHashSet();

            // Create new HashSet with RepairViewModel objects
            HashSet<RepairViewModel> viewModelSet = new();

            // Iterate through all Repair records by calling parent class's generic method for returning all records
            // and populate HashSet by calling extension method for converting Repair to RepairViewModel
            foreach (var repair in allRepairs)
            {
                viewModelSet.Add(repair.ConvertRepairToRepairViewModel());
            }

            // Call static method for returning Pagination class with populated HashSet
            return PaginationUtility<RepairViewModel>.GetPagination(in viewModelSet);
        }

        // Return repairs filtered by search therm, by customer and when page number is changed or when number of shown items per page is changed
        public async Task<Pagination<RepairViewModel>> GetFilteredRepairsAsync(string searchText, string customer, int pageIndex, int pageSize)
        {
            // Create and populate HashSet with Repair records
            HashSet<Repair> allRepairs = (await context.Repairs
                .Include(e => e.Car)
                .ThenInclude(e => e.Make)
                .Include(e => e.Part)
                .Include(e => e.Customer)
                .AsNoTracking()
                .ToListAsync())
                .ToHashSet();

            // Check if the Search operation is performed
            // If it is, then filter allRepairs by searchText
            if (searchText != null && searchText != string.Empty)
            {
                allRepairs = allRepairs.Where(e => e.Code.ToLower().Contains(searchText.ToLower()))
                    .ToHashSet();
            }

            // Check if the Search operation is performed
            // by Customer
            if(customer!=null&&customer!="Select Customer")
            {
                allRepairs = allRepairs.Where(e => (e.Customer.FirstName + " " + e.Customer.LastName + " " + e.Customer.Code) == customer)
                    .ToHashSet();
            }

            // Create new HashSet with RepairViewModel objects
            HashSet<RepairViewModel> viewModelSet = new();

            // Iterate through all Repair records by calling parent class's generic method for returning all records
            // and populate HashSet by calling extension method for converting Repair to RepairViewModel
            foreach (var repair in allRepairs)
            {
                viewModelSet.Add(repair.ConvertRepairToRepairViewModel());
            }

            // Call static method for returning Pagination class with populated HashSet
            return PaginationUtility<RepairViewModel>.GetPagination(in viewModelSet,pageIndex,pageSize);
        }

        // Return single Repair record
        public async Task<RepairViewModel> GetSingleRepairAsync(int id)
        {
            // Find Repair record by id
            Repair repair=await context.Repairs
                .Include(e=>e.Car)
                .ThenInclude(e => e.Make)
                .Include(e=>e.Part)
                .Include(e=>e.Customer)
                .AsNoTracking()
                .FirstOrDefaultAsync(e=>e.Id==id);

            // Return RepairViewModel by calling extension method for converting Repair to RepairViewModel
            return repair.ConvertRepairToRepairViewModel();
        }

        // Update existing Repair
        public async Task UpdateRepairAsync(RepairViewModel model)
        {
            // Find Repair record by Id
            Repair repair = await context.Repairs
                .Include(e => e.Car)
                .ThenInclude(e => e.Make)
                .Include(e => e.Part)
                .Include(e => e.Customer)
                .FirstOrDefaultAsync(e => e.Id == model.Id);

            // Set it's properties to the ones contained in model
            repair.Code = model.Code;
            repair.Car = await context.Cars.FirstOrDefaultAsync(e => (e.Make.Name + " " + e.Model + " " + e.Code) == model.Car);
            repair.Part = await context.Parts.FirstOrDefaultAsync(e => e.Name == model.Part);
            repair.RepairDate = model.RepairDate;
            repair.Qty = model.Qty;
            repair.WorkCost = model.WorkCost;
            repair.Customer = await context.Customers.FirstOrDefaultAsync(e => (e.FirstName + " " + e.LastName + " " + e.Code) == model.Customer);

            // Call parent class method for updating selected Repair record
            UpdateRecord(repair);
        }

        // Custom model validation
        public async Task<Dictionary<string, string>> ValidateRepairAsync(RepairViewModel model)
        {
            // Create new Dictionary<string, string> which will hold possible model errors
            Dictionary<string, string> errors = new();

            // Create and populate HashSet with all Repair records
            HashSet<Repair> allRepairs = (await GetAllAsync()).ToHashSet();

            // If model contains Id (i.e Id is larger than 0), it means that Repair is used in Update operation
            if (model.Id > 0)
            {
                // Find single Repair record by Id
                Repair repairRecord = await GetSingleAsync(model.Id);

                // Check if the Repair's Code value is changed
                if (repairRecord.Code != model.Code)
                {
                    // If it is changed, then check for Code uniqueness within all Repair records in database
                    if (allRepairs.Select(e => e.Code.ToLower()).Contains(model.Code.ToLower()))
                    {
                        // If supplied Code is already used by another Repair, then add error to errors Dictionary
                        errors.Add("Code", "There is already Reapir with this Code in database. Please provide different one!");
                    }
                }
                
                // Check if model's Repair Date is larger than current date
                if (model.RepairDate > DateTime.Now)
                {
                    // If it is, then add error to errors
                    errors.Add("RepairDate", "Repair date must not be larger than current date!");
                }
            }
            // Otherwise, it is used in Create operation
            else
            {
                if (allRepairs.Select(e => e.Code.ToLower()).Contains(model.Code.ToLower()))
                {
                    // If supplied Code is already used by another Repair, then add error to errors Dictionary
                    errors.Add("Code", "There is already Reapir with this Code in database. Please provide different one!");
                }

                // Check if model's Repair Date is larger than current date
                if (model.RepairDate > DateTime.Now)
                {
                    // If it is, then add error to errors
                    errors.Add("RepairDate", "Repair date must not be larger than current date!");
                }
            }

            // Return errors Dictionary
            return errors;
        }
    }
}
