using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CleanArchitecture.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Second_Migration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("ea4d7a20-bd4f-43cb-8377-9127308e8bac"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("fe7df93a-d50c-4920-a4eb-21d1d2b3f437"));

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "AspNetUsers",
                type: "character varying(8)",
                maxLength: 8,
                nullable: false,
                defaultValue: "");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("beb7ea36-7d70-41ba-91e7-6c57c7242738"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("d9113db7-fdc0-4cab-b107-53b19ecc6e4d"));

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "AspNetUsers");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("ea4d7a20-bd4f-43cb-8377-9127308e8bac"), "d5238fdd-42e5-4ee6-9c28-7d5895b7e529", "Admin", "ADMIN" },
                    { new Guid("fe7df93a-d50c-4920-a4eb-21d1d2b3f437"), "0726768a-dbba-4606-98a7-398028b11953", "User", "USER" }
                });

            migrationBuilder.UpdateData(
                table: "brands",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "AddedOnDate", "DeletedDate", "Name", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 7, 15, 4, 9, 8, 197, DateTimeKind.Utc).AddTicks(6632), new DateTime(2024, 7, 15, 4, 9, 8, 197, DateTimeKind.Utc).AddTicks(6586), "Books", new DateTime(2024, 7, 15, 4, 9, 8, 197, DateTimeKind.Utc).AddTicks(6585) });

            migrationBuilder.UpdateData(
                table: "brands",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "AddedOnDate", "DeletedDate", "Name", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 7, 15, 4, 9, 8, 197, DateTimeKind.Utc).AddTicks(6684), new DateTime(2024, 7, 15, 4, 9, 8, 197, DateTimeKind.Utc).AddTicks(6634), "Movies, Jewelery & Industrial", new DateTime(2024, 7, 15, 4, 9, 8, 197, DateTimeKind.Utc).AddTicks(6634) });

            migrationBuilder.UpdateData(
                table: "brands",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "AddedOnDate", "DeletedDate", "Name", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 7, 15, 4, 9, 8, 197, DateTimeKind.Utc).AddTicks(6696), new DateTime(2024, 7, 15, 4, 9, 8, 197, DateTimeKind.Utc).AddTicks(6685), "Garden & Jewelery", new DateTime(2024, 7, 15, 4, 9, 8, 197, DateTimeKind.Utc).AddTicks(6685) });

            migrationBuilder.UpdateData(
                table: "categories",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "AddedOnDate", "DeletedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 7, 15, 4, 9, 8, 199, DateTimeKind.Utc).AddTicks(6260), new DateTime(2024, 7, 15, 4, 9, 8, 199, DateTimeKind.Utc).AddTicks(6256), new DateTime(2024, 7, 15, 4, 9, 8, 199, DateTimeKind.Utc).AddTicks(6256) });

            migrationBuilder.UpdateData(
                table: "categories",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "AddedOnDate", "DeletedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 7, 15, 4, 9, 8, 199, DateTimeKind.Utc).AddTicks(6262), new DateTime(2024, 7, 15, 4, 9, 8, 199, DateTimeKind.Utc).AddTicks(6261), new DateTime(2024, 7, 15, 4, 9, 8, 199, DateTimeKind.Utc).AddTicks(6261) });

            migrationBuilder.UpdateData(
                table: "categories",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "AddedOnDate", "DeletedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 7, 15, 4, 9, 8, 199, DateTimeKind.Utc).AddTicks(6264), new DateTime(2024, 7, 15, 4, 9, 8, 199, DateTimeKind.Utc).AddTicks(6263), new DateTime(2024, 7, 15, 4, 9, 8, 199, DateTimeKind.Utc).AddTicks(6263) });

            migrationBuilder.UpdateData(
                table: "categories",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "AddedOnDate", "DeletedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 7, 15, 4, 9, 8, 199, DateTimeKind.Utc).AddTicks(6266), new DateTime(2024, 7, 15, 4, 9, 8, 199, DateTimeKind.Utc).AddTicks(6265), new DateTime(2024, 7, 15, 4, 9, 8, 199, DateTimeKind.Utc).AddTicks(6265) });

            migrationBuilder.UpdateData(
                table: "details",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "AddedOnDate", "DeletedDate", "Description", "Title", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 7, 15, 4, 9, 8, 201, DateTimeKind.Utc).AddTicks(5790), new DateTime(2024, 7, 15, 4, 9, 8, 201, DateTimeKind.Utc).AddTicks(5655), "Enim sed et debitis voluptatibus.", "Dolore.", new DateTime(2024, 7, 15, 4, 9, 8, 201, DateTimeKind.Utc).AddTicks(5654) });

            migrationBuilder.UpdateData(
                table: "details",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "AddedOnDate", "DeletedDate", "Description", "Title", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 7, 15, 4, 9, 8, 201, DateTimeKind.Utc).AddTicks(5868), new DateTime(2024, 7, 15, 4, 9, 8, 201, DateTimeKind.Utc).AddTicks(5792), "Quis veritatis et iusto perferendis.", "Illo earum.", new DateTime(2024, 7, 15, 4, 9, 8, 201, DateTimeKind.Utc).AddTicks(5792) });

            migrationBuilder.UpdateData(
                table: "details",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "AddedOnDate", "DeletedDate", "Description", "Title", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 7, 15, 4, 9, 8, 201, DateTimeKind.Utc).AddTicks(5896), new DateTime(2024, 7, 15, 4, 9, 8, 201, DateTimeKind.Utc).AddTicks(5869), "Eius mollitia labore architecto incidunt.", "Aut.", new DateTime(2024, 7, 15, 4, 9, 8, 201, DateTimeKind.Utc).AddTicks(5869) });

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "AddedOnDate", "DeletedDate", "Description", "Discount", "Price", "Title", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 7, 15, 4, 9, 8, 204, DateTimeKind.Utc).AddTicks(1905), new DateTime(2024, 7, 15, 4, 9, 8, 204, DateTimeKind.Utc).AddTicks(1765), "The automobile layout consists of a front-engine design, with transaxle-type transmissions mounted at the rear of the engine and four wheel drive", 40.421912349796320m, 1982.15m, "Ergonomic Granite Keyboard", new DateTime(2024, 7, 15, 4, 9, 8, 204, DateTimeKind.Utc).AddTicks(1765) });

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "AddedOnDate", "DeletedDate", "Description", "Discount", "Price", "Title", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 7, 15, 4, 9, 8, 204, DateTimeKind.Utc).AddTicks(1932), new DateTime(2024, 7, 15, 4, 9, 8, 204, DateTimeKind.Utc).AddTicks(1908), "The Football Is Good For Training And Recreational Purposes", 40.879096004844680m, 1389.95m, "Gorgeous Metal Chicken", new DateTime(2024, 7, 15, 4, 9, 8, 204, DateTimeKind.Utc).AddTicks(1907) });

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "AddedOnDate", "DeletedDate", "Description", "Discount", "Price", "Title", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 7, 15, 4, 9, 8, 204, DateTimeKind.Utc).AddTicks(1950), new DateTime(2024, 7, 15, 4, 9, 8, 204, DateTimeKind.Utc).AddTicks(1933), "The Football Is Good For Training And Recreational Purposes", 12.3628725804131680m, 1993.91m, "Incredible Steel Cheese", new DateTime(2024, 7, 15, 4, 9, 8, 204, DateTimeKind.Utc).AddTicks(1933) });
        }
    }
}
