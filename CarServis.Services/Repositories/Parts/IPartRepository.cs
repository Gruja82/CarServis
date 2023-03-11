using CarServis.Data.Entities;
using CarServis.Services.Repositories.Generic;
using CarServis.Services.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarServis.Services.Repositories.Parts
{
    // Interface which contains declarations of all necessary methods for working with Part
    public interface IPartRepository:IGenericRepository<Part>
    {
        // Custom model validation
        Task<Dictionary<string, string>> ValidatePartAsync(PartViewModel model);
        // Return all Part records
        Task<Pagination<PartViewModel>> GetAllPartsAsync();
        // Return filtered Part records, and used when page number is chnaged or items shown per page is changed
        Task<Pagination<PartViewModel>> GetFilteredPartsAsync(string searchText, int pageIndex, int pageSize);
        // Return single Part
        Task<PartViewModel> GetSinglePartAsync(int id);
        // Create new Part
        Task CreateNewPartAsync(PartViewModel model, string imagesFolder);
        // Update existing Part
        Task UpdatePartAsync(PartViewModel model,string imagesFolder);
        // Delete existing Part
        Task DeletePartAsync(int id, string imagesFolder);
    }
}
