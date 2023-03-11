using CarServis.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarServis.Data.Extensions
{
    public static class ModelBuilderExtension
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>().HasData(
                new Customer
                {
                    Id = 1,
                    Code = "65561555",
                    FirstName = "Maria",
                    LastName = "Anders",
                    Address = "Obere Str. 57",
                    City = "Berlin",
                    Postal = "12209",
                    Phone = "030-0074321",
                    Email = "maria@gmail.com"
                },
                new Customer
                {
                    Id = 2,
                    Code = "96145",
                    FirstName = "Hanna",
                    LastName = "Moss",
                    Address = "Forsterstr. 57",
                    City = "Mannheim",
                    Postal = "68306",
                    Phone = "0621-08460",
                    Email = "hanna@gmail.com"
                },
                new Customer
                {
                    Id = 3,
                    Code = "9798633",
                    FirstName = "Sven",
                    LastName = "Ottlieb",
                    Address = "Walserweg 21",
                    City = "Aachen",
                    Postal = "52066",
                    Phone = "0241-039123",
                    Email = "sven@gmail.com"
                },
                new Customer
                {
                    Id = 4,
                    Code = "2554178",
                    FirstName = "Peter",
                    LastName = "Franken",
                    Address = "Berliner Platz 43",
                    City = "München",
                    Postal = "80805",
                    Phone = "089-0877310",
                    Email = "peter@gmail.com"
                },
                new Customer
                {
                    Id = 5,
                    Code = "124578",
                    FirstName = "Philip",
                    LastName = "Cramer",
                    Address = "Maubelstr. 90",
                    City = "Brandenburg",
                    Postal = "14776",
                    Phone = "0555-09876",
                    Email = "philip@gmail.com"
                });

            modelBuilder.Entity<Make>().HasData(
                new Make
                {
                    Id = 1,
                    Code = "2151545",
                    Name = "Mercedes",
                    Country = "Germany",
                    Web = "https://www.mercedes-benz.com/en/",
                    ImageUrl = "0294b0b9-6_860d2666caf66420dffeeb98a1662f74.jpg"
                },
                new Make
                {
                    Id = 2,
                    Code = "56146",
                    Name = "Peugeot",
                    Country = "France",
                    Web = "https://www.peugeot.com/en/",
                    ImageUrl = "f7c003cb-8_1aba00ae2494614dad2083e6d1ee1fa4dd0953b7.png"
                },
                new Make
                {
                    Id = 3,
                    Code = "9254558",
                    Name = "Renault",
                    Country = "France",
                    Web = "https://www.renaultgroup.com/en/",
                    ImageUrl = "07f2926d-f_Renault-logo-2015-640x550.jpg"
                },
                new Make
                {
                    Id = 4,
                    Code = "995984651",
                    Name = "Toyota",
                    Country = "Japan",
                    Web = "https://www.toyota.com/",
                    ImageUrl = "toyota-logo-3A02221675-seeklogo.com.png"
                },
                new Make
                {
                    Id = 5,
                    Code = "7881651",
                    Name = "Volkswagen",
                    Country = "Germany",
                    Web = "https://www.vw.com/en.html",
                    ImageUrl = "Volkswagen-logo-2019-640x500.jpg"
                });

            modelBuilder.Entity<Part>().HasData(
                new Part
                {
                    Id = 1,
                    Code = "24651656",
                    Name = "Disc Brake Rotors",
                    Price = 48.36,
                    ImageUrl = "b38d14cc-edb4-4549-8e3d-eb6a2b85a49a.b863c0d5135d2d6c0102708bcae96c31.jpeg"
                },
                new Part
                {
                    Id = 2,
                    Code = "54221478",
                    Name = "Motorcraft BT-97 Belt Tensioner",
                    Price = 95.61,
                    ImageUrl = "5eea95ca-1edd-49ff-b2dc-b88bf273ecca.aabf538db4149e3e599ae70bba085457.jpeg"
                },
                new Part
                {
                    Id = 3,
                    Code = "6332145",
                    Name = "Dorman 621-410 Engine Cooling Fan",
                    Price = 111.95,
                    ImageUrl = "7952d7e8-5643-41fe-b6e6-01aa10d32062.e6928c111749af9bd310e8f87353f5f8.jpeg"
                },
                new Part
                {
                    Id = 4,
                    Code = "7884559",
                    Name = "1PZ UMT-2A1 Carburetor for Tecumseh",
                    Price = 15.99,
                    ImageUrl = "dba9c009-572a-406d-a545-f61941c08920_1.29e820cfbb478f0571845f57b8ff52db.jpeg"
                },
                new Part
                {
                    Id = 5,
                    Code = "985478",
                    Name = "Flo-Tek 102505 Assembled S/B Chevy Aluminum Head",
                    Price = 459.99,
                    ImageUrl = "36cf9ea1-c511-47a2-807a-ede13efd53e4.95dc224e07478fbe5e53cf7c510af198.jpeg"
                });

            modelBuilder.Entity<Car>().HasData(
                new Car
                {
                    Id = 1,
                    Code = "3625114",
                    MakeId = 1,
                    Model = "E-Class",
                    Year = "2021",
                    CustomerId = 1,
                    ImageUrl = "2021-Mercedes-Benz-E-Class-design2.jpg"
                },
                new Car
                {
                    Id = 2,
                    Code = "5847781",
                    MakeId = 5,
                    Model = "Golf",
                    Year = "2022",
                    CustomerId = 2,
                    ImageUrl = "gettyimages-1313844769-612x612.jpg"
                },
                new Car
                {
                    Id = 3,
                    Code = "9315674",
                    MakeId = 3,
                    Model = "Clio",
                    Year = "2022",
                    CustomerId = 3,
                    ImageUrl = "gettyimages-1373633710-612x612.jpg"
                },
                new Car
                {
                    Id = 4,
                    Code = "3362514",
                    MakeId = 4,
                    Model = "Hilux",
                    Year = "2022",
                    CustomerId = 4,
                    ImageUrl = "gettyimages-595755830-612x612.jpg"
                },
                new Car
                {
                    Id = 5,
                    Code = "852147",
                    MakeId = 2,
                    Model = "3008",
                    Year = "2022",
                    CustomerId = 5,
                    ImageUrl = "gettyimages-623682918-612x612.jpg"
                });
        }
    }
}
