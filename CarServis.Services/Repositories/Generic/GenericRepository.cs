using CarServis.Data.Database;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarServis.Services.Repositories.Generic
{
    // Generic implementation class which implements IGenericRepository interface
    // in which are implemented all CRUD methods
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected readonly AppDbContext context;

        // Through constructor inject AppDbContext
        public GenericRepository(AppDbContext context)
        {
            this.context = context;
        }

        // Create new record
        public async Task CreateNewAsync(T entity)
        {
            await context.Set<T>().AddAsync(entity);
        }

        // Delete existing record
        public async Task DeleteAsync(int id)
        {
            T entity = await context.Set<T>().FindAsync(id);
            context.Set<T>().Remove(entity);
        }

        // Return all records
        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await context.Set<T>().ToListAsync();
        }

        // Return single record
        public async Task<T> GetSingleAsync(int id)
        {
            return await context.Set<T>().FindAsync(id);
        }

        // Update existing record
        public void UpdateRecord(T entity)
        {
            context.Set<T>().Update(entity);
        }
    }
}
