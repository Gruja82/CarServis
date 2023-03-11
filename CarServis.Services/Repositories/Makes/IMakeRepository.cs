using CarServis.Data.Entities;
using CarServis.Services.Repositories.Generic;
using CarServis.Services.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarServis.Services.Repositories.Makes
{
    // Interface which contains declarations of all necessary methods for working with Make
    public interface IMakeRepository:IGenericRepository<Make>
    {
        // Custom model validation
        Task<Dictionary<string, string>> ValidateMakeAsync(MakeViewModel model);
        // Return all Make records
        Task<Pagination<MakeViewModel>> GetAllMakesAsync();
        // Return filtered Make records, and used when page number is chnaged or items shown per page is changed
        Task<Pagination<MakeViewModel>> GetFilteredMakesAsync(string searchText, int pageIndex, int pageSize);
        // Return single Make
        Task<MakeViewModel> GetSingleMakeAsync(int id);
        // Create new Make
        Task CreateNewMakeAsync(MakeViewModel model, string imagesFolder);
        // Update existing Make
        Task UpdateMakeAsync(MakeViewModel model, string imagesFolder);
        // Delete existing Make
        Task DeleteMakeAsync(int id, string imagesFolder);
    }
}
