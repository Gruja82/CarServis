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

namespace CarServis.Services.Repositories.Makes
{
    // Implementation class for makes
    // It inherits GenericRepository<> class which contains all basic CRUD methods,
    // and implements IMakeRepository interface for specific implementation 
    // concerning specific Entity
    public class MakeRepository:GenericRepository<Make>,IMakeRepository
    {
        // Through class constructor, pass AppDbContext instance to parent class constructor
        public MakeRepository(AppDbContext context):base(context)
        {

        }

        // Create new Make
        public async Task CreateNewMakeAsync(MakeViewModel model, string imagesFolder)
        {
            // Create new Make
            Make newMake = new();

            // Set it's properties to the ones in model
            newMake.Code = model.Code;
            newMake.Name = model.Name;
            newMake.Country = model.Country;
            newMake.Web = model.Web;
            newMake.ImageUrl = StoreImage.SaveImage(model, imagesFolder);

            // Cal parent class method for creating new Make
            await CreateNewAsync(newMake);
        }

        // Delete existing Make
        public async Task DeleteMakeAsync(int id, string imagesFolder)
        {
            // Find Make by id
            Make make = await GetSingleAsync(id);

            // Call parent class method for deleting existing Make
            await DeleteAsync(id);

            // If it has image, delete it
            if (make.ImageUrl != null && make.ImageUrl != String.Empty)
            {
                if (System.IO.File.Exists(Path.Combine(imagesFolder, make.ImageUrl)))
                {
                    System.IO.File.Delete(Path.Combine(imagesFolder, make.ImageUrl));
                }
            }
        }

        // Return all Car records
        public async Task<Pagination<MakeViewModel>> GetAllMakesAsync()
        {
            // Create new HashSet with MakeViewModel objects
            HashSet<MakeViewModel> viewModelSet = new();

            // Iterate through all Make records by calling parent class's generic method for returning all records
            // and populate HashSet by calling extension method for converting Make to MakeViewModel
            foreach (var make in await GetAllAsync())
            {
                viewModelSet.Add(make.ConvertMakeToMakeViewModel());
            }

            // Call static method for returning Pagination class with populated HashSet
            return PaginationUtility<MakeViewModel>.GetPagination(in viewModelSet);
        }

        // Return makes filtered by search therm, when page number is changed or when number of shown items per page is changed
        public async Task<Pagination<MakeViewModel>> GetFilteredMakesAsync(string searchText, int pageIndex, int pageSize)
        {
            // Create and populate HashSet with all Make records
            HashSet<Make> allMakes=(await GetAllAsync()).ToHashSet();

            // Check if the Search operation is performed
            // If it is, then filter allMakes by searchText
            if (searchText != null && searchText != string.Empty)
            {
                allMakes = allMakes.Where(e => e.Code.ToLower().Contains(searchText.ToLower())
                    || e.Name.ToLower().Contains(searchText.ToLower())
                    || e.Country.ToLower() == searchText.ToLower())
                    .ToHashSet();
            }

            // Create new HashSet with MakeViewModel objects
            HashSet<MakeViewModel> viewModelSet = new();

            // Iterate through all Make records by calling parent class's generic method for returning all records
            // and populate HashSet by calling extension method for converting Make to MakeViewModel
            foreach (var make in allMakes)
            {
                viewModelSet.Add(make.ConvertMakeToMakeViewModel());
            }

            // Call static method for returning Pagination class with populated HashSet
            return PaginationUtility<MakeViewModel>.GetPagination(in viewModelSet, pageIndex, pageSize);
        }

        // Return single Make record
        public async Task<MakeViewModel> GetSingleMakeAsync(int id)
        {
            // Call parent class method for returning single Make record
            Make make = await GetSingleAsync(id);

            // Return MakeViewModel by calling extension method for converting Make to MakeViewModel
            return make.ConvertMakeToMakeViewModel();
        }

        // Update existing Make
        public async Task UpdateMakeAsync(MakeViewModel model, string imagesFolder)
        {
            // Find Car record by Id
            Make make=await GetSingleAsync(model.Id);

            // Set it's properties to the ones contained in model
            make.Code = model.Code;
            make.Name = model.Name;
            make.Country = model.Country;
            make.Web = model.Web;
            if (model.Image != null)
            {
                if (System.IO.File.Exists(Path.Combine(imagesFolder, make.ImageUrl)))
                {
                    System.IO.File.Delete(Path.Combine(imagesFolder, make.ImageUrl));
                }
                make.ImageUrl = StoreImage.SaveImage(model, imagesFolder);
            }

            // Call parent class method for updating selected Make record
            UpdateRecord(make);
        }

        // Custom model validation
        public async Task<Dictionary<string, string>> ValidateMakeAsync(MakeViewModel model)
        {
            // Create new Dictionary<string, string> which will hold possible model errors
            Dictionary<string, string> errors = new();

            // Create and populate HashSet with all Make records
            HashSet<Make> allMakes = (await GetAllAsync()).ToHashSet();

            // If model contains Id (i.e Id is larger than 0), it means that Make is used in Update operation
            if (model.Id > 0)
            {
                // Find single Make record by Id
                Make makeRecord = await GetSingleAsync(model.Id);

                // Check if the Make's Code value is changed
                if (makeRecord.Code != model.Code)
                {
                    // If it is changed, then check for Code uniqueness within all Make records in database
                    if (allMakes.Select(e => e.Code.ToLower()).Contains(model.Code.ToLower()))
                    {
                        // If supplied Code is already used by another Make, then add error to errors Dictionary
                        errors.Add("Code", "There is already Make with this Code in database. Please provide different one!");
                    }
                }

                // Check if the Make's Name value is changed
                if (makeRecord.Name != model.Name)
                {
                    // If it is changed, then check for Name uniqueness within all Make records in database
                    if (allMakes.Select(e => e.Name.ToLower()).Contains(model.Name.ToLower()))
                    {
                        // If supplied Name is already used by another Make, then add error to errors Dictionary
                        errors.Add("Name", "There is already Make with this Name in database. Please provide different one!");
                    }
                }
            }
            // Otherwise, it is used in Create operation
            else
            {
                if (allMakes.Select(e => e.Code.ToLower()).Contains(model.Code.ToLower()))
                {
                    // If supplied Code is already used by another Make, then add error to errors Dictionary
                    errors.Add("Code", "There is already Make with this Code in database. Please provide different one!");
                }

                if (allMakes.Select(e => e.Name.ToLower()).Contains(model.Name.ToLower()))
                {
                    // If supplied Name is already used by another Make, then add error to errors Dictionary
                    errors.Add("Name", "There is already Make with this Name in database. Please provide different one!");
                }
            }

            // Return errors Dictionary
            return errors;
        }
    }
}
