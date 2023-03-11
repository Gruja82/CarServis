using CarServis.Data.Entities;
using CarServis.Services.Repositories.Generic;
using CarServis.Services.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarServis.Services.Repositories.Cars
{
    // Interface which contains declarations of all necessary methods for working with Car
    public interface ICarRepository:IGenericRepository<Car>
    {
        // Custom model validation
        Task<Dictionary<string, string>> ValidateCarAsync(CarViewModel model);
        // Return all Car records
        Task<Pagination<CarViewModel>> GetAllCarsAsync();
        // Return filtered Car records, and used when page number is chnaged or items shown per page is changed
        Task<Pagination<CarViewModel>> GetFilteredCarsAsync(string searchText, string make, string customer, int pageIndex, int pageSize);
        // Return single Car
        Task<CarViewModel> GetSingleCarAsync(int id);
        // Create new Car
        Task CreateNewCarAsync(CarViewModel model,string imagesFolder);
        // Update existing Car
        Task UpdateCarAsync(CarViewModel model, string imagesFolder);
        // Delete existing Car
        Task DeleteCarAsync(int id,string imagesFolder);

    }
}
