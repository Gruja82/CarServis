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

namespace CarServis.Services.Repositories.Parts
{
    // Implementation class for parts
    // It inherits GenericRepository<> class which contains all basic CRUD methods,
    // and implements IPartRepository interface for specific implementation 
    // concerning specific Entity
    public class PartRepository:GenericRepository<Part>,IPartRepository
    {
        // Through class constructor, pass AppDbContext instance to parent class constructor
        public PartRepository(AppDbContext context):base(context)
        {

        }

        // Create new Part
        public async Task CreateNewPartAsync(PartViewModel model, string imagesFolder)
        {
            // Create new Part object
            Part newPart = new();

            newPart.Code = model.Code;
            newPart.Name=model.Name;
            newPart.Price = model.Price;
            newPart.Quantity = model.Quantity;
            newPart.ImageUrl = StoreImage.SaveImage(model, imagesFolder);

            // Call parent class method for creating new Part
            await CreateNewAsync(newPart);
        }

        // Delete existing Part
        public async Task DeletePartAsync(int id, string imagesFolder)
        {
            // Find Part by Id
            Part part=await GetSingleAsync(id);

            // Call parent class method for deleting existing Part
            await DeleteAsync(id);

            // If it has image, delete it
            if (part.ImageUrl != null && part.ImageUrl != string.Empty)
            {
                if (System.IO.File.Exists(Path.Combine(imagesFolder, part.ImageUrl)))
                {
                    System.IO.File.Delete(Path.Combine(imagesFolder , part.ImageUrl));
                }
            }
        }

        // Return all Part records
        public async Task<Pagination<PartViewModel>> GetAllPartsAsync()
        {
            // Create new HashSet with PartViewModel objects
            HashSet<PartViewModel> viewModelSet = new();

            // Iterate through all Part records by calling parent class's generic method for returning all records
            // and populate HashSet by calling extension method for converting Part to PartViewModel
            foreach (var part in await GetAllAsync())
            {
                viewModelSet.Add(part.ConvertPartToPartViewModel());
            }

            // Call static method for returning Pagination class with populated HashSet
            return PaginationUtility<PartViewModel>.GetPagination(in viewModelSet);
        }

        // Return parts filtered by serach therm, when page number is changed or when number of shown items per page is changed
        public async Task<Pagination<PartViewModel>> GetFilteredPartsAsync(string searchText, int pageIndex, int pageSize)
        {
            // Create and populate HashSet with all Part records
            HashSet<Part> allParts = (await GetAllAsync()).ToHashSet();

            // Check if the Search operation is performed
            // If it is, then filter allParts by searchText
            if (searchText != null && searchText != string.Empty)
            {
                allParts=allParts.Where(e=>e.Code.ToLower().Contains(searchText.ToLower())
                    || e.Name.ToLower().Contains(searchText.ToLower()))
                    .ToHashSet();
            }

            // Create new HashSet with PartViewModel objects
            HashSet<PartViewModel> viewModelSet = new();

            // Iterate through all Part records by calling parent class's generic method for returning all records
            // and populate HashSet by calling extension method for converting Part to PartViewModel
            foreach (var part in allParts)
            {
                viewModelSet.Add(part.ConvertPartToPartViewModel());
            }

            // Call static method for returning Pagination class with populated HashSet
            return PaginationUtility<PartViewModel>.GetPagination(in viewModelSet, pageIndex, pageSize);
        }

        // Return single Part record
        public async Task<PartViewModel> GetSinglePartAsync(int id)
        {
            // Find Part record by Id
            Part part = await GetSingleAsync(id);

            // Return PartViewModel by calling extension method for converting Part to PartViewModel
            return part.ConvertPartToPartViewModel();
        }

        // Update existing Part
        public async Task UpdatePartAsync(PartViewModel model, string imagesFolder)
        {
            // Find Part record by Id
            Part part=await GetSingleAsync(model.Id);

            // Set part properties to the ones contained in model
            part.Code = model.Code;
            part.Name = model.Name;
            part.Price = model.Price;
            part.Quantity = model.Quantity;
            if (model.Image != null)
            {
                if (System.IO.File.Exists(Path.Combine(imagesFolder, part.ImageUrl)))
                {
                    System.IO.File.Delete(Path.Combine(imagesFolder, part.ImageUrl));
                }
                part.ImageUrl = StoreImage.SaveImage(model, imagesFolder);
            }

            // Call parent class method for updating selected Part record
            UpdateRecord(part);
        }

        // Custom model validation
        public async Task<Dictionary<string, string>> ValidatePartAsync(PartViewModel model)
        {
            // Create new Dictionary<string, string> which will hold possible model errors
            Dictionary<string, string> errors = new();

            // Create and populate HashSet with all Part records
            HashSet<Part> allParts = (await GetAllAsync()).ToHashSet();

            // If model contains Id (i.e Id is larger than 0), it means that Part is used in Update operation
            if (model.Id > 0)
            {
                // Find single Part record by Id
                Part partRecord = await GetSingleAsync(model.Id);

                // Check if the Part's Code value is changed
                if (partRecord.Code != model.Code)
                {
                    // If it is changed, then check for Code uniqueness within all Part records in database
                    if (allParts.Select(e => e.Code.ToLower()).Contains(model.Code.ToLower()))
                    {
                        // If supplied Code is already used by another Part, then add error to errors Dictionary
                        errors.Add("Code", "There is already Part with this Code in database. Please provide different one!");
                    }
                }

                // Check if the Part's Name value is changed
                if (partRecord.Name != model.Name)
                {
                    // If it is changed, then check for Name uniqueness within all Part records in database
                    if (allParts.Select(e => e.Name.ToLower()).Contains(model.Name.ToLower()))
                    {
                        // If supplied Name is already used by another Part, then add error to errors Dictionary
                        errors.Add("Name", "There is already Part with this Name in database. Please provide different one!");
                    }
                }
            }
            // Otherwise, it is used in Create operation
            else
            {
                if (allParts.Select(e => e.Code.ToLower()).Contains(model.Code.ToLower()))
                {
                    // If supplied Code is already used by another Part, then add error to errors Dictionary
                    errors.Add("Code", "There is already Part with this Code in database. Please provide different one!");
                }

                if (allParts.Select(e => e.Name.ToLower()).Contains(model.Name.ToLower()))
                {
                    // If supplied Name is already used by another Part, then add error to errors Dictionary
                    errors.Add("Name", "There is already Part with this Name in database. Please provide different one!");
                }
            }

            // Return errors Dictionary
            return errors;
        }
    }
}
