using CarServis.Data.Entities;
using CarServis.Services.Repositories.Generic;
using CarServis.Services.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarServis.Services.Repositories.Repairs
{
    // Interface which contains declarations of all necessary methods for working with Repair
    public interface IRepairRepository:IGenericRepository<Repair>
    {
        // Custom model validation
        Task<Dictionary<string, string>> ValidateRepairAsync(RepairViewModel model);
        // Return all Repair records
        Task<Pagination<RepairViewModel>> GetAllRepairsAsync();
        // Return filtered Repair records, and used when page number is chnaged or items shown per page is changed
        Task<Pagination<RepairViewModel>> GetFilteredRepairsAsync(string searchText, string customer, int pageIndex, int pageSize);
        // Return car make and model
        Task<HashSet<string>> ReturnCarMakeAndModel();
        // Return single Repair
        Task<RepairViewModel> GetSingleRepairAsync(int id);
        // Create new Repair
        Task CreateNewRepairAsync(RepairViewModel model);
        // Update existing Repair
        Task UpdateRepairAsync(RepairViewModel model);
        // Delete existing Repair
        Task DeleteRepairAsync(int id);
    }
}
