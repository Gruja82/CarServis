using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarServis.Services.Repositories.Generic
{
    // This generic interface contains declarations of all CRUD
    // operations that can be performed on each entity
    public interface IGenericRepository<T> where T : class
    {
        // Return all records
        Task<IEnumerable<T>> GetAllAsync();
        // Return single record
        Task<T> GetSingleAsync(int id);
        // Create new record
        Task CreateNewAsync(T entity);
        // Update existing record
        void UpdateRecord(T entity);
        // Delete existing record
        Task DeleteAsync(int id);
    }
}
