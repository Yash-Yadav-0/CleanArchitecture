using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CleanArchitecture.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class third_Migration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("beb7ea36-7d70-41ba-91e7-6c57c7242738"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("d9113db7-fdc0-4cab-b107-53b19ecc6e4d"));

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

            migrationBuilder.UpdateData(
                table: "details",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "AddedOnDate", "DeletedDate", "Description", "Title", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 7, 17, 4, 1, 7, 188, DateTimeKind.Utc).AddTicks(2324), new DateTime(2024, 7, 17, 4, 1, 7, 188, DateTimeKind.Utc).AddTicks(2204), "Assumenda rerum doloremque quis incidunt.", "Dolorem.", new DateTime(2024, 7, 17, 4, 1, 7, 188, DateTimeKind.Utc).AddTicks(2204) });

            migrationBuilder.UpdateData(
                table: "details",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "AddedOnDate", "DeletedDate", "Description", "Title", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 7, 17, 4, 1, 7, 188, DateTimeKind.Utc).AddTicks(2356), new DateTime(2024, 7, 17, 4, 1, 7, 188, DateTimeKind.Utc).AddTicks(2326), "Quia excepturi est laborum non.", "Exercitationem quod.", new DateTime(2024, 7, 17, 4, 1, 7, 188, DateTimeKind.Utc).AddTicks(2326) });

            migrationBuilder.UpdateData(
                table: "details",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "AddedOnDate", "DeletedDate", "Description", "Title", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 7, 17, 4, 1, 7, 188, DateTimeKind.Utc).AddTicks(2381), new DateTime(2024, 7, 17, 4, 1, 7, 188, DateTimeKind.Utc).AddTicks(2358), "Sint magnam omnis architecto maiores.", "Architecto.", new DateTime(2024, 7, 17, 4, 1, 7, 188, DateTimeKind.Utc).AddTicks(2357) });

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("beb7ea36-7d70-41ba-91e7-6c57c7242738"), "a9826de7-18c6-460b-a295-85813962beb3", "Admin", "ADMIN" },
                    { new Guid("d9113db7-fdc0-4cab-b107-53b19ecc6e4d"), "09684b7d-e9f8-407b-ae15-fbe790949400", "User", "USER" }
                });

            migrationBuilder.UpdateData(
                table: "brands",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "AddedOnDate", "DeletedDate", "Name", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 7, 17, 3, 53, 33, 111, DateTimeKind.Utc).AddTicks(2740), new DateTime(2024, 7, 17, 3, 53, 33, 111, DateTimeKind.Utc).AddTicks(2583), "Industrial & Automotive", new DateTime(2024, 7, 17, 3, 53, 33, 111, DateTimeKind.Utc).AddTicks(2583) });

            migrationBuilder.UpdateData(
                table: "brands",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "AddedOnDate", "DeletedDate", "Name", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 7, 17, 3, 53, 33, 111, DateTimeKind.Utc).AddTicks(2760), new DateTime(2024, 7, 17, 3, 53, 33, 111, DateTimeKind.Utc).AddTicks(2742), "Garden, Books & Electronics", new DateTime(2024, 7, 17, 3, 53, 33, 111, DateTimeKind.Utc).AddTicks(2742) });

            migrationBuilder.UpdateData(
                table: "brands",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "AddedOnDate", "DeletedDate", "Name", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 7, 17, 3, 53, 33, 111, DateTimeKind.Utc).AddTicks(2773), new DateTime(2024, 7, 17, 3, 53, 33, 111, DateTimeKind.Utc).AddTicks(2761), "Health, Home & Shoes", new DateTime(2024, 7, 17, 3, 53, 33, 111, DateTimeKind.Utc).AddTicks(2761) });

            migrationBuilder.UpdateData(
                table: "categories",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "AddedOnDate", "DeletedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 7, 17, 3, 53, 33, 113, DateTimeKind.Utc).AddTicks(4633), new DateTime(2024, 7, 17, 3, 53, 33, 113, DateTimeKind.Utc).AddTicks(4626), new DateTime(2024, 7, 17, 3, 53, 33, 113, DateTimeKind.Utc).AddTicks(4626) });

            migrationBuilder.UpdateData(
                table: "categories",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "AddedOnDate", "DeletedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 7, 17, 3, 53, 33, 113, DateTimeKind.Utc).AddTicks(4636), new DateTime(2024, 7, 17, 3, 53, 33, 113, DateTimeKind.Utc).AddTicks(4635), new DateTime(2024, 7, 17, 3, 53, 33, 113, DateTimeKind.Utc).AddTicks(4635) });

            migrationBuilder.UpdateData(
                table: "categories",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "AddedOnDate", "DeletedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 7, 17, 3, 53, 33, 113, DateTimeKind.Utc).AddTicks(4638), new DateTime(2024, 7, 17, 3, 53, 33, 113, DateTimeKind.Utc).AddTicks(4637), new DateTime(2024, 7, 17, 3, 53, 33, 113, DateTimeKind.Utc).AddTicks(4637) });

            migrationBuilder.UpdateData(
                table: "categories",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "AddedOnDate", "DeletedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 7, 17, 3, 53, 33, 113, DateTimeKind.Utc).AddTicks(4640), new DateTime(2024, 7, 17, 3, 53, 33, 113, DateTimeKind.Utc).AddTicks(4639), new DateTime(2024, 7, 17, 3, 53, 33, 113, DateTimeKind.Utc).AddTicks(4639) });

            migrationBuilder.UpdateData(
                table: "details",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "AddedOnDate", "DeletedDate", "Description", "Title", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 7, 17, 3, 53, 33, 115, DateTimeKind.Utc).AddTicks(8279), new DateTime(2024, 7, 17, 3, 53, 33, 115, DateTimeKind.Utc).AddTicks(8159), "Eum repellat nisi laboriosam et.", "Eius.", new DateTime(2024, 7, 17, 3, 53, 33, 115, DateTimeKind.Utc).AddTicks(8159) });

            migrationBuilder.UpdateData(
                table: "details",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "AddedOnDate", "DeletedDate", "Description", "Title", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 7, 17, 3, 53, 33, 115, DateTimeKind.Utc).AddTicks(8315), new DateTime(2024, 7, 17, 3, 53, 33, 115, DateTimeKind.Utc).AddTicks(8281), "Sunt quia veritatis explicabo et.", "Qui magni.", new DateTime(2024, 7, 17, 3, 53, 33, 115, DateTimeKind.Utc).AddTicks(8281) });

            migrationBuilder.UpdateData(
                table: "details",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "AddedOnDate", "DeletedDate", "Description", "Title", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 7, 17, 3, 53, 33, 115, DateTimeKind.Utc).AddTicks(8397), new DateTime(2024, 7, 17, 3, 53, 33, 115, DateTimeKind.Utc).AddTicks(8316), "Non incidunt dolorem error tenetur.", "Illum.", new DateTime(2024, 7, 17, 3, 53, 33, 115, DateTimeKind.Utc).AddTicks(8316) });

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "AddedOnDate", "DeletedDate", "Description", "Discount", "Price", "Title", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 7, 17, 3, 53, 33, 117, DateTimeKind.Utc).AddTicks(6331), new DateTime(2024, 7, 17, 3, 53, 33, 117, DateTimeKind.Utc).AddTicks(6222), "The Football Is Good For Training And Recreational Purposes", 16.837235113216280m, 568.19m, "Incredible Fresh Gloves", new DateTime(2024, 7, 17, 3, 53, 33, 117, DateTimeKind.Utc).AddTicks(6222) });

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "AddedOnDate", "DeletedDate", "Description", "Discount", "Price", "Title", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 7, 17, 3, 53, 33, 117, DateTimeKind.Utc).AddTicks(6355), new DateTime(2024, 7, 17, 3, 53, 33, 117, DateTimeKind.Utc).AddTicks(6333), "The Nagasaki Lander is the trademarked name of several series of Nagasaki sport bikes, that started with the 1984 ABC800J", 42.078652759450680m, 1237.39m, "Small Cotton Computer", new DateTime(2024, 7, 17, 3, 53, 33, 117, DateTimeKind.Utc).AddTicks(6333) });

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "AddedOnDate", "DeletedDate", "Description", "Discount", "Price", "Title", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 7, 17, 3, 53, 33, 117, DateTimeKind.Utc).AddTicks(6376), new DateTime(2024, 7, 17, 3, 53, 33, 117, DateTimeKind.Utc).AddTicks(6357), "Ergonomic executive chair upholstered in bonded black leather and PVC padded seat and back for all-day comfort and support", 44.473588906204720m, 309.77m, "Rustic Frozen Computer", new DateTime(2024, 7, 17, 3, 53, 33, 117, DateTimeKind.Utc).AddTicks(6356) });
        }
    }
}
