using CarServis.Data.Database;
using CarServis.Services.Repositories.Cars;
using CarServis.Services.Repositories.Customers;
using CarServis.Services.Repositories.Makes;
using CarServis.Services.Repositories.Parts;
using CarServis.Services.Repositories.Repairs;
using CarServis.Services.Repositories.UoW;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarServis.Services.Utility
{
    // The purpose of this static class is to register
    // database context, all individual repositories and UnitOfWork repository
    public static class RegisterServices
    {
        public static void AddServicesToContainer(IServiceCollection services,string connString)
        {
            services.AddDbContext<AppDbContext>(options => options.UseSqlServer(connString));
            services.AddScoped<ICarRepository, CarRepository>();
            services.AddScoped<IPartRepository, PartRepository>();
            services.AddScoped<ICustomerRepository, CustomerRepository>();
            services.AddScoped<IMakeRepository, MakeRepository>();
            services.AddScoped<IPartRepository, PartRepository>();
            services.AddScoped<IRepairRepository, RepairRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
        }
    }
}
