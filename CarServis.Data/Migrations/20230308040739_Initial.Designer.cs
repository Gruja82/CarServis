﻿// <auto-generated />
using System;
using CarServis.Data.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace CarServis.Data.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20230308040739_Initial")]
    partial class Initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("CarServis.Data.Entities.Car", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("CustomerId")
                        .HasColumnType("int");

                    b.Property<string>("ImageUrl")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("MakeId")
                        .HasColumnType("int");

                    b.Property<string>("Model")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Year")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("CustomerId");

                    b.HasIndex("MakeId");

                    b.ToTable("Cars");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Code = "3625114",
                            CustomerId = 1,
                            ImageUrl = "2021-Mercedes-Benz-E-Class-design2.jpg",
                            MakeId = 1,
                            Model = "E-Class",
                            Year = "2021"
                        },
                        new
                        {
                            Id = 2,
                            Code = "5847781",
                            CustomerId = 2,
                            ImageUrl = "gettyimages-1313844769-612x612.jpg",
                            MakeId = 5,
                            Model = "Golf",
                            Year = "2022"
                        },
                        new
                        {
                            Id = 3,
                            Code = "9315674",
                            CustomerId = 3,
                            ImageUrl = "gettyimages-1373633710-612x612.jpg",
                            MakeId = 3,
                            Model = "Clio",
                            Year = "2022"
                        },
                        new
                        {
                            Id = 4,
                            Code = "3362514",
                            CustomerId = 4,
                            ImageUrl = "gettyimages-595755830-612x612.jpg",
                            MakeId = 4,
                            Model = "Hilux",
                            Year = "2022"
                        },
                        new
                        {
                            Id = 5,
                            Code = "852147",
                            CustomerId = 5,
                            ImageUrl = "gettyimages-623682918-612x612.jpg",
                            MakeId = 2,
                            Model = "3008",
                            Year = "2022"
                        });
                });

            modelBuilder.Entity("CarServis.Data.Entities.Customer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("City")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Postal")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Customers");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Address = "Obere Str. 57",
                            City = "Berlin",
                            Code = "65561555",
                            Email = "maria@gmail.com",
                            FirstName = "Maria",
                            LastName = "Anders",
                            Phone = "030-0074321",
                            Postal = "12209"
                        },
                        new
                        {
                            Id = 2,
                            Address = "Forsterstr. 57",
                            City = "Mannheim",
                            Code = "96145",
                            Email = "hanna@gmail.com",
                            FirstName = "Hanna",
                            LastName = "Moss",
                            Phone = "0621-08460",
                            Postal = "68306"
                        },
                        new
                        {
                            Id = 3,
                            Address = "Walserweg 21",
                            City = "Aachen",
                            Code = "9798633",
                            Email = "sven@gmail.com",
                            FirstName = "Sven",
                            LastName = "Ottlieb",
                            Phone = "0241-039123",
                            Postal = "52066"
                        },
                        new
                        {
                            Id = 4,
                            Address = "Berliner Platz 43",
                            City = "München",
                            Code = "2554178",
                            Email = "peter@gmail.com",
                            FirstName = "Peter",
                            LastName = "Franken",
                            Phone = "089-0877310",
                            Postal = "80805"
                        },
                        new
                        {
                            Id = 5,
                            Address = "Maubelstr. 90",
                            City = "Brandenburg",
                            Code = "124578",
                            Email = "philip@gmail.com",
                            FirstName = "Philip",
                            LastName = "Cramer",
                            Phone = "0555-09876",
                            Postal = "14776"
                        });
                });

            modelBuilder.Entity("CarServis.Data.Entities.Make", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Country")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ImageUrl")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Web")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Makes");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Code = "2151545",
                            Country = "Germany",
                            ImageUrl = "0294b0b9-6_860d2666caf66420dffeeb98a1662f74.jpg",
                            Name = "Mercedes",
                            Web = "https://www.mercedes-benz.com/en/"
                        },
                        new
                        {
                            Id = 2,
                            Code = "56146",
                            Country = "France",
                            ImageUrl = "f7c003cb-8_1aba00ae2494614dad2083e6d1ee1fa4dd0953b7.png",
                            Name = "Peugeot",
                            Web = "https://www.peugeot.com/en/"
                        },
                        new
                        {
                            Id = 3,
                            Code = "9254558",
                            Country = "France",
                            ImageUrl = "07f2926d-f_Renault-logo-2015-640x550.jpg",
                            Name = "Renault",
                            Web = "https://www.renaultgroup.com/en/"
                        },
                        new
                        {
                            Id = 4,
                            Code = "995984651",
                            Country = "Japan",
                            ImageUrl = "toyota-logo-3A02221675-seeklogo.com.png",
                            Name = "Toyota",
                            Web = "https://www.toyota.com/"
                        },
                        new
                        {
                            Id = 5,
                            Code = "7881651",
                            Country = "Germany",
                            ImageUrl = "Volkswagen-logo-2019-640x500.jpg",
                            Name = "Volkswagen",
                            Web = "https://www.vw.com/en.html"
                        });
                });

            modelBuilder.Entity("CarServis.Data.Entities.Part", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ImageUrl")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("Price")
                        .HasColumnType("float");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Parts");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Code = "24651656",
                            ImageUrl = "b38d14cc-edb4-4549-8e3d-eb6a2b85a49a.b863c0d5135d2d6c0102708bcae96c31.jpeg",
                            Name = "Disc Brake Rotors",
                            Price = 48.359999999999999,
                            Quantity = 0
                        },
                        new
                        {
                            Id = 2,
                            Code = "54221478",
                            ImageUrl = "5eea95ca-1edd-49ff-b2dc-b88bf273ecca.aabf538db4149e3e599ae70bba085457.jpeg",
                            Name = "Motorcraft BT-97 Belt Tensioner",
                            Price = 95.609999999999999,
                            Quantity = 0
                        },
                        new
                        {
                            Id = 3,
                            Code = "6332145",
                            ImageUrl = "7952d7e8-5643-41fe-b6e6-01aa10d32062.e6928c111749af9bd310e8f87353f5f8.jpeg",
                            Name = "Dorman 621-410 Engine Cooling Fan",
                            Price = 111.95,
                            Quantity = 0
                        },
                        new
                        {
                            Id = 4,
                            Code = "7884559",
                            ImageUrl = "dba9c009-572a-406d-a545-f61941c08920_1.29e820cfbb478f0571845f57b8ff52db.jpeg",
                            Name = "1PZ UMT-2A1 Carburetor for Tecumseh",
                            Price = 15.99,
                            Quantity = 0
                        },
                        new
                        {
                            Id = 5,
                            Code = "985478",
                            ImageUrl = "36cf9ea1-c511-47a2-807a-ede13efd53e4.95dc224e07478fbe5e53cf7c510af198.jpeg",
                            Name = "Flo-Tek 102505 Assembled S/B Chevy Aluminum Head",
                            Price = 459.99000000000001,
                            Quantity = 0
                        });
                });

            modelBuilder.Entity("CarServis.Data.Entities.Repair", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("CarId")
                        .HasColumnType("int");

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("CustomerId")
                        .HasColumnType("int");

                    b.Property<int>("PartId")
                        .HasColumnType("int");

                    b.Property<int>("Qty")
                        .HasColumnType("int");

                    b.Property<DateTime>("RepairDate")
                        .HasColumnType("datetime2");

                    b.Property<double>("WorkCost")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.HasIndex("CarId");

                    b.HasIndex("CustomerId");

                    b.HasIndex("PartId");

                    b.ToTable("Repairs");
                });

            modelBuilder.Entity("CarServis.Data.Entities.Car", b =>
                {
                    b.HasOne("CarServis.Data.Entities.Customer", "Customer")
                        .WithMany("Cars")
                        .HasForeignKey("CustomerId")
                        .IsRequired();

                    b.HasOne("CarServis.Data.Entities.Make", "Make")
                        .WithMany("Cars")
                        .HasForeignKey("MakeId")
                        .IsRequired();

                    b.Navigation("Customer");

                    b.Navigation("Make");
                });

            modelBuilder.Entity("CarServis.Data.Entities.Repair", b =>
                {
                    b.HasOne("CarServis.Data.Entities.Car", "Car")
                        .WithMany()
                        .HasForeignKey("CarId")
                        .IsRequired();

                    b.HasOne("CarServis.Data.Entities.Customer", "Customer")
                        .WithMany("Repairs")
                        .HasForeignKey("CustomerId")
                        .IsRequired();

                    b.HasOne("CarServis.Data.Entities.Part", "Part")
                        .WithMany("Repairs")
                        .HasForeignKey("PartId")
                        .IsRequired();

                    b.Navigation("Car");

                    b.Navigation("Customer");

                    b.Navigation("Part");
                });

            modelBuilder.Entity("CarServis.Data.Entities.Customer", b =>
                {
                    b.Navigation("Cars");

                    b.Navigation("Repairs");
                });

            modelBuilder.Entity("CarServis.Data.Entities.Make", b =>
                {
                    b.Navigation("Cars");
                });

            modelBuilder.Entity("CarServis.Data.Entities.Part", b =>
                {
                    b.Navigation("Repairs");
                });
#pragma warning restore 612, 618
        }
    }
}
