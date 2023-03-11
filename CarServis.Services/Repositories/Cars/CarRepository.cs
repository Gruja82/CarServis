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

namespace CarServis.Services.Repositories.Cars
{
    // Implementation class for cars
    // It inherits GenericRepository<> class which contains all basic CRUD methods,
    // and implements ICarRepository interface for specific implementation 
    // concerning specific Entity
    public class CarRepository:GenericRepository<Car>,ICarRepository
    {
        // Through class constructor, pass AppDbContext instance to parent class constructor
        public CarRepository(AppDbContext context):base(context)
        {

        }

        // Create new Car
        public async Task CreateNewCarAsync(CarViewModel model, string imagesFolder)
        {
            // Create new Car object
            Car newCar = new();

            // Set it's properties to the ones in model
            newCar.Code = model.Code;
            newCar.Make = await context.Makes.FirstOrDefaultAsync(e => e.Name == model.Make);
            newCar.Model = model.Model;
            newCar.Year = model.Year;
            newCar.Customer = context.Customers.Where(e=>(e.FirstName + " " + e.LastName + " " + e.Code)==model.Customer).FirstOrDefault();
            newCar.ImageUrl = StoreImage.SaveImage(model, imagesFolder);

            // Cal parent class method for creating new Car
            await CreateNewAsync(newCar);
        }

        // Delete existing Car
        public async Task DeleteCarAsync(int id,string imagesFolder)
        {
            // Find Car by id
            Car car = await GetSingleAsync(id);

            // Call parent class method for deleting existing Car
            await DeleteAsync(id);

            // If it has image, delete it
            if(car.ImageUrl != null && car.ImageUrl != String.Empty)
            {
                if (System.IO.File.Exists(Path.Combine(imagesFolder, car.ImageUrl)))
                {
                    System.IO.File.Delete(Path.Combine(imagesFolder, car.ImageUrl));
                }
            }
        }

        // Return all Car records
        public async Task<Pagination<CarViewModel>> GetAllCarsAsync()
        {
            // Create and populate HashSet with all Car records
            HashSet<Car> cars = (await context.Cars
                .Include(e => e.Make)
                .Include(e => e.Customer)
                .AsNoTracking()
                .ToListAsync())
                .ToHashSet();

            // Create new HashSet with CarViewModel objects
            HashSet<CarViewModel> viewModelSet = new();

            // Iterate through all Car records by calling parent class's generic method for returning all records
            // and populate HashSet by calling extension method for converting Car to CarViewModel
            foreach (var car in cars)
            {
                viewModelSet.Add(car.ConvertCarToCarViewModel());
            }

            // Call static method for returning Pagination class with populated HashSet
            return PaginationUtility<CarViewModel>.GetPagination(in viewModelSet);
        }

        // Return cars filtered by search therm, when page number is changed or when number of shown items per page is changed
        public async Task<Pagination<CarViewModel>> GetFilteredCarsAsync(string searchText, string make, string customer, int pageIndex, int pageSize)
        {
            // Create and populate HashSet with all Car records
            HashSet<Car> allCars = (await context.Cars
                .Include(e=>e.Make)
                .Include(e=>e.Customer)
                .AsNoTracking()
                .ToListAsync())
                .ToHashSet();

            // Check if the Search operation is performed
            // If it is, then filter allCars by searchText
            if (searchText != null && searchText != String.Empty)
            {
                allCars=allCars.Where(e=>e.Code.ToLower().Contains(searchText.ToLower())
                    || e.Model.ToLower().Contains(searchText.ToLower()))
                    .ToHashSet();
            }

            // If search is performed by make, then filter allCars by make
            if(make != null &&make != "Select Make")
            {
                allCars = allCars.Where(e => e.Make == context.Makes.FirstOrDefault(e => e.Name == make))
                    .ToHashSet();
            }

            // If search is performed by customer, then filter allcars by customer
            if(customer != null && customer != "Select Customer")
            {
                allCars = allCars.Where(e => e.Customer == context.Customers.FirstOrDefault(e => (e.FirstName + " " + e.LastName + " " + e.Code) == customer))
                    .ToHashSet();
            }

            // Create new HashSet with CarViewModel objects
            HashSet<CarViewModel> viewModelSet = new();

            // Iterate through all Car records by calling parent class's generic method for returning all records
            // and populate HashSet by calling extension method for converting Car to CarViewModel
            foreach (var car in allCars)
            {
                viewModelSet.Add(car.ConvertCarToCarViewModel());
            }

            // Call static method for returning Pagination class with populated HashSet
            return PaginationUtility<CarViewModel>.GetPagination(in viewModelSet, pageIndex, pageSize);
        }

        // Return single Car record
        public async Task<CarViewModel> GetSingleCarAsync(int id)
        {
            // Call parent class method for returning single Car record
            Car car = await context.Cars
                .Include(e => e.Make)
                .Include(e => e.Customer)
                .AsNoTracking()
                .FirstOrDefaultAsync(e => e.Id == id);

            // Return CarViewModel by calling extension method for converting Car to CarViewModel
            return car.ConvertCarToCarViewModel();
        }

        // Update existing Car
        public async Task UpdateCarAsync(CarViewModel model, string imagesFolder)
        {
            // Find Car record by Id
            Car car = await context.Cars
                .Include(e => e.Make)
                .Include(e => e.Customer)
                .FirstOrDefaultAsync(e => e.Id == model.Id);

            // Set car properties to the ones contained in model
            car.Code = model.Code;
            car.Make = await context.Makes.FirstOrDefaultAsync(e => e.Name == model.Make);
            car.Model = model.Model;
            car.Year = model.Year;
            car.Customer=await context.Customers.FirstOrDefaultAsync(e => (e.FirstName + " " + e.LastName + " " + e.Code) == model.Customer);
            if (model.Image != null)
            {
                if (System.IO.File.Exists(Path.Combine(imagesFolder, car.ImageUrl)))
                {
                    System.IO.File.Delete(Path.Combine(imagesFolder, car.ImageUrl));
                }
                car.ImageUrl = StoreImage.SaveImage(model, imagesFolder);
            }

            // Call parent class method for updating selected Car record
            UpdateRecord(car);
        }

        // Custom model validation
        public async Task<Dictionary<string, string>> ValidateCarAsync(CarViewModel model)
        {
            // Create new Dictionary<string, string> which will hold possible model errors
            Dictionary<string, string> errors = new();

            // Create and populate HashSet with all Car records
            HashSet<Car> allCars = context.Cars
                .Include(e => e.Make)
                .Include(e => e.Customer)
                .AsNoTracking()
                .ToHashSet();

            // If model contains Id (i.e Id is larger than 0), it means that Car is used in Update operation
            if (model.Id > 0)
            {
                // Find single Car record by Id
                Car carRecord=await GetSingleAsync(model.Id);

                // Check if the Car's Code value is changed
                if (carRecord.Code != model.Code)
                {
                    // If it is changed, then check for Code uniqueness within all Car records in database
                    if (allCars.Select(e => e.Code.ToLower()).Contains(model.Code.ToLower()))
                    {
                        // If supplied Code is already used by another Car, then add error to errors Dictionary
                        errors.Add("Code", "There is already Car with this Code in database. Please provide different one!");
                    }
                }
            }
            // Otherwise, it is used in Create operation
            else
            {
                if (allCars.Select(e => e.Code.ToLower()).Contains(model.Code.ToLower()))
                {
                    // If supplied Code is already used by another Car, then add error to errors Dictionary
                    errors.Add("Code", "There is already Car with this Code in database. Please provide different one!");
                }
            }

            // Return errors Dictionary
            return errors;
        }
    }
}
