using CarServis.Data.Entities;
using CarServis.Services.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarServis.Services.Extensions
{
    // This class contains extension methods for converting entity classes to their's view models
    public static class Converts
    {
        public static CarViewModel ConvertCarToCarViewModel(this Car car)
        {
            CarViewModel viewModel = new();

            viewModel.Id=car.Id;
            viewModel.Code=car.Code;
            viewModel.Make = car.Make.Name;
            viewModel.Model = car.Model;
            viewModel.Year = car.Year;
            viewModel.Customer = car.Customer.FirstName + " " + car.Customer.LastName + " " + car.Customer.Code;
            viewModel.ImageUrl = car.ImageUrl;

            return viewModel;
        }

        public static PartViewModel ConvertPartToPartViewModel(this Part part)
        {
            PartViewModel viewModel = new();

            viewModel.Id=part.Id;
            viewModel.Code = part.Code;
            viewModel.Name=part.Name;
            viewModel.Price= part.Price;
            viewModel.Quantity = part.Quantity;
            viewModel.ImageUrl = part.ImageUrl;

            return viewModel;
        }

        public static CustomerViewModel ConvertCustomerToCustomerViewModel(this Customer customer)
        {
            CustomerViewModel viewModel = new();

            viewModel.Id = customer.Id;
            viewModel.Code = customer.Code;
            viewModel.FirstName = customer.FirstName;
            viewModel.LastName = customer.LastName;
            viewModel.Address = customer.Address;
            viewModel.City = customer.City;
            viewModel.Postal = customer.Postal;
            viewModel.Phone = customer.Phone;
            viewModel.Email = customer.Email;

            return viewModel;
        }

        public static RepairViewModel ConvertRepairToRepairViewModel(this Repair repair)
        {
            RepairViewModel viewModel = new();

            viewModel.Id = repair.Id;
            viewModel.Code = repair.Code;
            viewModel.Car = repair.Car.Make.Name + " " + repair.Car.Model;
            viewModel.Part = repair.Part.Name;
            viewModel.RepairDate = repair.RepairDate;
            viewModel.Qty= repair.Qty;
            viewModel.WorkCost = repair.WorkCost;
            viewModel.Customer = repair.Customer.FirstName + " " + repair.Customer.LastName + " " + repair.Customer.Code;
            viewModel.Charge = repair.Part.Price*repair.Qty + repair.WorkCost;

            return viewModel;
        }

        public static MakeViewModel ConvertMakeToMakeViewModel(this Make make)
        {
            MakeViewModel viewModel = new();

            viewModel.Id = make.Id;
            viewModel.Code = make.Code;
            viewModel.Name = make.Name;
            viewModel.Country = make.Country;
            viewModel.Web=make.Web;
            viewModel.ImageUrl = make.ImageUrl;

            return viewModel;
        }
    }
}
