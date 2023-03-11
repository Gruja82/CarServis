using CarServis.Services.Repositories.Cars;
using CarServis.Services.Repositories.Customers;
using CarServis.Services.Repositories.Generic;
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
    // This interface provides access to individual repositories
    // and declares single method for saving changes to database
    public interface IUnitOfWork:IDisposable
    {
        public ICarRepository Cars { get; }
        public IPartRepository Parts { get; }
        public ICustomerRepository Customers { get; }
        public IRepairRepository Repairs { get; }
        public IMakeRepository Makes { get; }
        Task ConfirmChangesAsync();
    }
}
