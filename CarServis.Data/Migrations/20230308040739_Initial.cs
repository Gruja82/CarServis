using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CarServis.Data.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Postal = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Makes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Country = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Web = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Makes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Parts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<double>(type: "float", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Parts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Cars",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MakeId = table.Column<int>(type: "int", nullable: false),
                    Model = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Year = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CustomerId = table.Column<int>(type: "int", nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cars", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Cars_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Cars_Makes_MakeId",
                        column: x => x.MakeId,
                        principalTable: "Makes",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Repairs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CarId = table.Column<int>(type: "int", nullable: false),
                    PartId = table.Column<int>(type: "int", nullable: false),
                    RepairDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Qty = table.Column<int>(type: "int", nullable: false),
                    CustomerId = table.Column<int>(type: "int", nullable: false),
                    WorkCost = table.Column<double>(type: "float", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Repairs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Repairs_Cars_CarId",
                        column: x => x.CarId,
                        principalTable: "Cars",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Repairs_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Repairs_Parts_PartId",
                        column: x => x.PartId,
                        principalTable: "Parts",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "Customers",
                columns: new[] { "Id", "Address", "City", "Code", "Email", "FirstName", "LastName", "Phone", "Postal" },
                values: new object[,]
                {
                    { 1, "Obere Str. 57", "Berlin", "65561555", "maria@gmail.com", "Maria", "Anders", "030-0074321", "12209" },
                    { 2, "Forsterstr. 57", "Mannheim", "96145", "hanna@gmail.com", "Hanna", "Moss", "0621-08460", "68306" },
                    { 3, "Walserweg 21", "Aachen", "9798633", "sven@gmail.com", "Sven", "Ottlieb", "0241-039123", "52066" },
                    { 4, "Berliner Platz 43", "München", "2554178", "peter@gmail.com", "Peter", "Franken", "089-0877310", "80805" },
                    { 5, "Maubelstr. 90", "Brandenburg", "124578", "philip@gmail.com", "Philip", "Cramer", "0555-09876", "14776" }
                });

            migrationBuilder.InsertData(
                table: "Makes",
                columns: new[] { "Id", "Code", "Country", "ImageUrl", "Name", "Web" },
                values: new object[,]
                {
                    { 1, "2151545", "Germany", "0294b0b9-6_860d2666caf66420dffeeb98a1662f74.jpg", "Mercedes", "https://www.mercedes-benz.com/en/" },
                    { 2, "56146", "France", "f7c003cb-8_1aba00ae2494614dad2083e6d1ee1fa4dd0953b7.png", "Peugeot", "https://www.peugeot.com/en/" },
                    { 3, "9254558", "France", "07f2926d-f_Renault-logo-2015-640x550.jpg", "Renault", "https://www.renaultgroup.com/en/" },
                    { 4, "995984651", "Japan", "toyota-logo-3A02221675-seeklogo.com.png", "Toyota", "https://www.toyota.com/" },
                    { 5, "7881651", "Germany", "Volkswagen-logo-2019-640x500.jpg", "Volkswagen", "https://www.vw.com/en.html" }
                });

            migrationBuilder.InsertData(
                table: "Parts",
                columns: new[] { "Id", "Code", "ImageUrl", "Name", "Price", "Quantity" },
                values: new object[,]
                {
                    { 1, "24651656", "b38d14cc-edb4-4549-8e3d-eb6a2b85a49a.b863c0d5135d2d6c0102708bcae96c31.jpeg", "Disc Brake Rotors", 48.359999999999999, 0 },
                    { 2, "54221478", "5eea95ca-1edd-49ff-b2dc-b88bf273ecca.aabf538db4149e3e599ae70bba085457.jpeg", "Motorcraft BT-97 Belt Tensioner", 95.609999999999999, 0 },
                    { 3, "6332145", "7952d7e8-5643-41fe-b6e6-01aa10d32062.e6928c111749af9bd310e8f87353f5f8.jpeg", "Dorman 621-410 Engine Cooling Fan", 111.95, 0 },
                    { 4, "7884559", "dba9c009-572a-406d-a545-f61941c08920_1.29e820cfbb478f0571845f57b8ff52db.jpeg", "1PZ UMT-2A1 Carburetor for Tecumseh", 15.99, 0 },
                    { 5, "985478", "36cf9ea1-c511-47a2-807a-ede13efd53e4.95dc224e07478fbe5e53cf7c510af198.jpeg", "Flo-Tek 102505 Assembled S/B Chevy Aluminum Head", 459.99000000000001, 0 }
                });

            migrationBuilder.InsertData(
                table: "Cars",
                columns: new[] { "Id", "Code", "CustomerId", "ImageUrl", "MakeId", "Model", "Year" },
                values: new object[,]
                {
                    { 1, "3625114", 1, "2021-Mercedes-Benz-E-Class-design2.jpg", 1, "E-Class", "2021" },
                    { 2, "5847781", 2, "gettyimages-1313844769-612x612.jpg", 5, "Golf", "2022" },
                    { 3, "9315674", 3, "gettyimages-1373633710-612x612.jpg", 3, "Clio", "2022" },
                    { 4, "3362514", 4, "gettyimages-595755830-612x612.jpg", 4, "Hilux", "2022" },
                    { 5, "852147", 5, "gettyimages-623682918-612x612.jpg", 2, "3008", "2022" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Cars_CustomerId",
                table: "Cars",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Cars_MakeId",
                table: "Cars",
                column: "MakeId");

            migrationBuilder.CreateIndex(
                name: "IX_Repairs_CarId",
                table: "Repairs",
                column: "CarId");

            migrationBuilder.CreateIndex(
                name: "IX_Repairs_CustomerId",
                table: "Repairs",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Repairs_PartId",
                table: "Repairs",
                column: "PartId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Repairs");

            migrationBuilder.DropTable(
                name: "Cars");

            migrationBuilder.DropTable(
                name: "Parts");

            migrationBuilder.DropTable(
                name: "Customers");

            migrationBuilder.DropTable(
                name: "Makes");
        }
    }
}
