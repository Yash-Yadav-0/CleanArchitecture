using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CleanArchitecture.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class firstMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "deviceInfo");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("2048c126-5303-46a3-9092-b6a3522dc89f"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("8ce510d6-6ae5-4571-b6a8-983c73016004"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("e8787a4e-1e10-42ba-b34b-c3ee297a03b4"));

            migrationBuilder.DeleteData(
                table: "details",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "details",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "details",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("4c084bf1-92e5-491b-8606-2b743eb1f0a1"), "fb671218-ecf0-488d-90a3-3678833dd80a", "User", "USER" },
                    { new Guid("d5bbdcc2-68f6-43f2-8bef-a1d3d75dd900"), "7df9053a-1dd1-4bca-9366-c29cd3ede4e4", "Vendor", "VENDOR" },
                    { new Guid("f60340eb-2b78-43a4-a1fe-b6c248c57f6c"), "042c4b5c-62fc-420f-ae6e-9c6b39e832c4", "Admin", "ADMIN" }
                });

            migrationBuilder.UpdateData(
                table: "brands",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "AddedOnDate", "DeletedDate", "Name", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 7, 31, 9, 38, 30, 651, DateTimeKind.Utc).AddTicks(2071), new DateTime(2024, 7, 31, 9, 38, 30, 651, DateTimeKind.Utc).AddTicks(1812), "Computers, Movies & Games", new DateTime(2024, 7, 31, 9, 38, 30, 651, DateTimeKind.Utc).AddTicks(1812) });

            migrationBuilder.UpdateData(
                table: "brands",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "AddedOnDate", "DeletedDate", "Name", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 7, 31, 9, 38, 30, 651, DateTimeKind.Utc).AddTicks(2095), new DateTime(2024, 7, 31, 9, 38, 30, 651, DateTimeKind.Utc).AddTicks(2074), "Industrial, Baby & Sports", new DateTime(2024, 7, 31, 9, 38, 30, 651, DateTimeKind.Utc).AddTicks(2074) });

            migrationBuilder.UpdateData(
                table: "brands",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "AddedOnDate", "DeletedDate", "Name", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 7, 31, 9, 38, 30, 651, DateTimeKind.Utc).AddTicks(2101), new DateTime(2024, 7, 31, 9, 38, 30, 651, DateTimeKind.Utc).AddTicks(2097), "Baby", new DateTime(2024, 7, 31, 9, 38, 30, 651, DateTimeKind.Utc).AddTicks(2096) });

            migrationBuilder.UpdateData(
                table: "categories",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "AddedOnDate", "DeletedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 7, 31, 9, 38, 30, 653, DateTimeKind.Utc).AddTicks(654), new DateTime(2024, 7, 31, 9, 38, 30, 653, DateTimeKind.Utc).AddTicks(651), new DateTime(2024, 7, 31, 9, 38, 30, 653, DateTimeKind.Utc).AddTicks(650) });

            migrationBuilder.UpdateData(
                table: "categories",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "AddedOnDate", "DeletedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 7, 31, 9, 38, 30, 653, DateTimeKind.Utc).AddTicks(657), new DateTime(2024, 7, 31, 9, 38, 30, 653, DateTimeKind.Utc).AddTicks(656), new DateTime(2024, 7, 31, 9, 38, 30, 653, DateTimeKind.Utc).AddTicks(656) });

            migrationBuilder.UpdateData(
                table: "categories",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "AddedOnDate", "DeletedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 7, 31, 9, 38, 30, 653, DateTimeKind.Utc).AddTicks(659), new DateTime(2024, 7, 31, 9, 38, 30, 653, DateTimeKind.Utc).AddTicks(658), new DateTime(2024, 7, 31, 9, 38, 30, 653, DateTimeKind.Utc).AddTicks(658) });

            migrationBuilder.UpdateData(
                table: "categories",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "AddedOnDate", "DeletedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 7, 31, 9, 38, 30, 653, DateTimeKind.Utc).AddTicks(661), new DateTime(2024, 7, 31, 9, 38, 30, 653, DateTimeKind.Utc).AddTicks(660), new DateTime(2024, 7, 31, 9, 38, 30, 653, DateTimeKind.Utc).AddTicks(660) });

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "AddedOnDate", "DeletedDate", "Description", "Discount", "Price", "Title", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 7, 31, 9, 38, 30, 654, DateTimeKind.Utc).AddTicks(8822), new DateTime(2024, 7, 31, 9, 38, 30, 654, DateTimeKind.Utc).AddTicks(8691), "Andy shoes are designed to keeping in mind durability as well as trends, the most stylish range of shoes & sandals", 12.847399571065920m, 1706.64m, "Awesome Wooden Shoes", new DateTime(2024, 7, 31, 9, 38, 30, 654, DateTimeKind.Utc).AddTicks(8691) });

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "AddedOnDate", "DeletedDate", "Description", "Discount", "Price", "Title", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 7, 31, 9, 38, 30, 654, DateTimeKind.Utc).AddTicks(8899), new DateTime(2024, 7, 31, 9, 38, 30, 654, DateTimeKind.Utc).AddTicks(8825), "New ABC 13 9370, 13.3, 5th Gen CoreA5-8250U, 8GB RAM, 256GB SSD, power UHD Graphics, OS 10 Home, OS Office A & J 2016", 17.974627559116560m, 1385.06m, "Sleek Frozen Chair", new DateTime(2024, 7, 31, 9, 38, 30, 654, DateTimeKind.Utc).AddTicks(8824) });

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "AddedOnDate", "DeletedDate", "Description", "Discount", "Price", "Title", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 7, 31, 9, 38, 30, 654, DateTimeKind.Utc).AddTicks(8926), new DateTime(2024, 7, 31, 9, 38, 30, 654, DateTimeKind.Utc).AddTicks(8901), "New range of formal shirts are designed keeping you in mind. With fits and styling that will make you stand apart", 19.635343844184880m, 1549.83m, "Refined Concrete Chips", new DateTime(2024, 7, 31, 9, 38, 30, 654, DateTimeKind.Utc).AddTicks(8900) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("4c084bf1-92e5-491b-8606-2b743eb1f0a1"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("d5bbdcc2-68f6-43f2-8bef-a1d3d75dd900"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("f60340eb-2b78-43a4-a1fe-b6c248c57f6c"));

            migrationBuilder.CreateTable(
                name: "deviceInfo",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Browser = table.Column<string>(type: "text", nullable: false),
                    DeviceType = table.Column<string>(type: "text", nullable: false),
                    IpAddress = table.Column<string>(type: "text", nullable: false),
                    LoggedInAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_deviceInfo", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("2048c126-5303-46a3-9092-b6a3522dc89f"), "325b5ce1-7e0c-40e6-b1be-638264d496c7", "Vendor", "VENDOR" },
                    { new Guid("8ce510d6-6ae5-4571-b6a8-983c73016004"), "6b2e6023-1201-497f-96a4-0db6000e8955", "Admin", "ADMIN" },
                    { new Guid("e8787a4e-1e10-42ba-b34b-c3ee297a03b4"), "075f38ae-c5fa-4ef6-ab85-d9bff844e015", "User", "USER" }
                });

            migrationBuilder.UpdateData(
                table: "brands",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "AddedOnDate", "DeletedDate", "Name", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 7, 17, 4, 1, 7, 184, DateTimeKind.Utc).AddTicks(4004), new DateTime(2024, 7, 17, 4, 1, 7, 184, DateTimeKind.Utc).AddTicks(3921), "Home & Automotive", new DateTime(2024, 7, 17, 4, 1, 7, 184, DateTimeKind.Utc).AddTicks(3921) });

            migrationBuilder.UpdateData(
                table: "brands",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "AddedOnDate", "DeletedDate", "Name", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 7, 17, 4, 1, 7, 184, DateTimeKind.Utc).AddTicks(4011), new DateTime(2024, 7, 17, 4, 1, 7, 184, DateTimeKind.Utc).AddTicks(4006), "Baby", new DateTime(2024, 7, 17, 4, 1, 7, 184, DateTimeKind.Utc).AddTicks(4006) });

            migrationBuilder.UpdateData(
                table: "brands",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "AddedOnDate", "DeletedDate", "Name", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 7, 17, 4, 1, 7, 184, DateTimeKind.Utc).AddTicks(4028), new DateTime(2024, 7, 17, 4, 1, 7, 184, DateTimeKind.Utc).AddTicks(4012), "Health, Beauty & Sports", new DateTime(2024, 7, 17, 4, 1, 7, 184, DateTimeKind.Utc).AddTicks(4012) });

            migrationBuilder.UpdateData(
                table: "categories",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "AddedOnDate", "DeletedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 7, 17, 4, 1, 7, 186, DateTimeKind.Utc).AddTicks(3856), new DateTime(2024, 7, 17, 4, 1, 7, 186, DateTimeKind.Utc).AddTicks(3849), new DateTime(2024, 7, 17, 4, 1, 7, 186, DateTimeKind.Utc).AddTicks(3849) });

            migrationBuilder.UpdateData(
                table: "categories",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "AddedOnDate", "DeletedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 7, 17, 4, 1, 7, 186, DateTimeKind.Utc).AddTicks(3858), new DateTime(2024, 7, 17, 4, 1, 7, 186, DateTimeKind.Utc).AddTicks(3857), new DateTime(2024, 7, 17, 4, 1, 7, 186, DateTimeKind.Utc).AddTicks(3857) });

            migrationBuilder.UpdateData(
                table: "categories",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "AddedOnDate", "DeletedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 7, 17, 4, 1, 7, 186, DateTimeKind.Utc).AddTicks(3860), new DateTime(2024, 7, 17, 4, 1, 7, 186, DateTimeKind.Utc).AddTicks(3859), new DateTime(2024, 7, 17, 4, 1, 7, 186, DateTimeKind.Utc).AddTicks(3859) });

            migrationBuilder.UpdateData(
                table: "categories",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "AddedOnDate", "DeletedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 7, 17, 4, 1, 7, 186, DateTimeKind.Utc).AddTicks(3862), new DateTime(2024, 7, 17, 4, 1, 7, 186, DateTimeKind.Utc).AddTicks(3861), new DateTime(2024, 7, 17, 4, 1, 7, 186, DateTimeKind.Utc).AddTicks(3861) });

            migrationBuilder.InsertData(
                table: "details",
                columns: new[] { "Id", "AddedOnDate", "CategoryId", "DeletedDate", "Description", "IsDeleted", "Title", "UpdatedDate" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 7, 17, 4, 1, 7, 188, DateTimeKind.Utc).AddTicks(2324), 1, new DateTime(2024, 7, 17, 4, 1, 7, 188, DateTimeKind.Utc).AddTicks(2204), "Assumenda rerum doloremque quis incidunt.", false, "Dolorem.", new DateTime(2024, 7, 17, 4, 1, 7, 188, DateTimeKind.Utc).AddTicks(2204) },
                    { 2, new DateTime(2024, 7, 17, 4, 1, 7, 188, DateTimeKind.Utc).AddTicks(2356), 3, new DateTime(2024, 7, 17, 4, 1, 7, 188, DateTimeKind.Utc).AddTicks(2326), "Quia excepturi est laborum non.", true, "Exercitationem quod.", new DateTime(2024, 7, 17, 4, 1, 7, 188, DateTimeKind.Utc).AddTicks(2326) },
                    { 3, new DateTime(2024, 7, 17, 4, 1, 7, 188, DateTimeKind.Utc).AddTicks(2381), 4, new DateTime(2024, 7, 17, 4, 1, 7, 188, DateTimeKind.Utc).AddTicks(2358), "Sint magnam omnis architecto maiores.", false, "Architecto.", new DateTime(2024, 7, 17, 4, 1, 7, 188, DateTimeKind.Utc).AddTicks(2357) }
                });

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "AddedOnDate", "DeletedDate", "Description", "Discount", "Price", "Title", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 7, 17, 4, 1, 7, 190, DateTimeKind.Utc).AddTicks(1754), new DateTime(2024, 7, 17, 4, 1, 7, 190, DateTimeKind.Utc).AddTicks(1553), "The Apollotech B340 is an affordable wireless mouse with reliable connectivity, 12 months battery life and modern design", 18.083372607392840m, 97.99m, "Fantastic Frozen Pizza", new DateTime(2024, 7, 17, 4, 1, 7, 190, DateTimeKind.Utc).AddTicks(1553) });

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "AddedOnDate", "DeletedDate", "Description", "Discount", "Price", "Title", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 7, 17, 4, 1, 7, 190, DateTimeKind.Utc).AddTicks(1782), new DateTime(2024, 7, 17, 4, 1, 7, 190, DateTimeKind.Utc).AddTicks(1756), "The slim & simple Maple Gaming Keyboard from Dev Byte comes with a sleek body and 7- Color RGB LED Back-lighting for smart functionality", 36.173927615778600m, 372.78m, "Practical Rubber Towels", new DateTime(2024, 7, 17, 4, 1, 7, 190, DateTimeKind.Utc).AddTicks(1756) });

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "AddedOnDate", "DeletedDate", "Description", "Discount", "Price", "Title", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 7, 17, 4, 1, 7, 190, DateTimeKind.Utc).AddTicks(1803), new DateTime(2024, 7, 17, 4, 1, 7, 190, DateTimeKind.Utc).AddTicks(1783), "The automobile layout consists of a front-engine design, with transaxle-type transmissions mounted at the rear of the engine and four wheel drive", 19.899471559961680m, 216.63m, "Licensed Steel Gloves", new DateTime(2024, 7, 17, 4, 1, 7, 190, DateTimeKind.Utc).AddTicks(1783) });
        }
    }
}
