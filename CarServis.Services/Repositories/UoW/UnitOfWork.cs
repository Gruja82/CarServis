using CarServis.Data.Database;
using CarServis.Services.Repositories.Cars;
using CarServis.Services.Repositories.Customers;
using CarServis.Services.Repositories.Makes;
using CarServis.Services.Repositories.Parts;
using CarServis.Services.Repositories.Repairs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarServis.Services.Repositories.UoW
{
    // This is implmentation class for IUnitOfWork interface
    // It provides acces to individual repositories and implements
    // method for saving changes to database
    // and method for disposing database context
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext context;

        public ICarRepository Cars { get; }
        public IPartRepository Parts { get; }
        public ICustomerRepository Customers { get; }
        public IRepairRepository Repairs { get; }
        public IMakeRepository Makes { get; }
        // Through constructor inject AppDbContext, and initialize
        // all repositories
        public UnitOfWork(AppDbContext context,ICarRepository cars,IPartRepository parts,ICustomerRepository customers,
            IRepairRepository repairs,IMakeRepository makes)
        {
            this.context = context;
            Cars = cars;
            Parts = parts;
            Customers = customers;
            Repairs = repairs;
            Makes = makes;
        }

        // Method for saving changes to database
        public async Task ConfirmChangesAsync()
        {
            await context.SaveChangesAsync();
        }

        // Dispose context object to instantly release context
        // object and suppress GC
        public void Dispose()
        {
            context.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
