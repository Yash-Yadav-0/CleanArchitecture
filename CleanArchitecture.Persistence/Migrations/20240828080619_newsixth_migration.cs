using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CleanArchitecture.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class newsixth_migration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Permissions = table.Column<int>(type: "integer", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    CreatedBy = table.Column<string>(type: "text", nullable: true),
                    LastModifiedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "text", nullable: true),
                    Name = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    FullName = table.Column<string>(type: "text", nullable: false),
                    Picture = table.Column<string>(type: "text", nullable: true),
                    RefreshToken = table.Column<string>(type: "text", nullable: true),
                    RefreshTokenExpiryTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    CodeForResetPassword = table.Column<string>(type: "text", nullable: true),
                    TimeOfCodeExpiration = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    IsCodeOfResetPasswordTrue = table.Column<bool>(type: "boolean", nullable: true),
                    Discriminator = table.Column<string>(type: "character varying(8)", maxLength: 8, nullable: false),
                    UserName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "boolean", nullable: false),
                    PasswordHash = table.Column<string>(type: "text", nullable: true),
                    SecurityStamp = table.Column<string>(type: "text", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "text", nullable: true),
                    PhoneNumber = table.Column<string>(type: "text", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "boolean", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "boolean", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "boolean", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "brands",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Picture = table.Column<string>(type: "text", nullable: true),
                    AddedOnDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DeletedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_brands", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "categories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ParentId = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Priorty = table.Column<int>(type: "integer", nullable: false),
                    AddedOnDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DeletedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    RoleId = table.Column<Guid>(type: "uuid", nullable: false),
                    ClaimType = table.Column<string>(type: "text", nullable: true),
                    ClaimValue = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    ClaimType = table.Column<string>(type: "text", nullable: true),
                    ClaimValue = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "text", nullable: false),
                    ProviderKey = table.Column<string>(type: "text", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "text", nullable: true),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    RoleId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    LoginProvider = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Value = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "orders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    TotalAmount = table.Column<decimal>(type: "numeric", nullable: false),
                    OrderType = table.Column<byte>(type: "smallint", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    AddedOnDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DeletedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_orders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_orders_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "products",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Title = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    Price = table.Column<decimal>(type: "numeric", nullable: false),
                    Discount = table.Column<decimal>(type: "numeric", nullable: false),
                    BrandId = table.Column<int>(type: "integer", nullable: false),
                    AddedOnDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DeletedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_products", x => x.Id);
                    table.ForeignKey(
                        name: "FK_products_brands_BrandId",
                        column: x => x.BrandId,
                        principalTable: "brands",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "details",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Title = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    CategoryId = table.Column<int>(type: "integer", nullable: false),
                    AddedOnDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DeletedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_details", x => x.Id);
                    table.ForeignKey(
                        name: "FK_details_categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "images",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    FileName = table.Column<string>(type: "text", nullable: false),
                    Path = table.Column<string>(type: "text", nullable: false),
                    ProductId = table.Column<int>(type: "integer", nullable: false),
                    AddedOnDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DeletedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_images", x => x.Id);
                    table.ForeignKey(
                        name: "FK_images_products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "orderProducts",
                columns: table => new
                {
                    ProductId = table.Column<int>(type: "integer", nullable: false),
                    OrderId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_orderProducts", x => new { x.OrderId, x.ProductId });
                    table.ForeignKey(
                        name: "FK_orderProducts_orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_orderProducts_products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "productsCategories",
                columns: table => new
                {
                    ProductId = table.Column<int>(type: "integer", nullable: false),
                    CategoryId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_productsCategories", x => new { x.ProductId, x.CategoryId });
                    table.ForeignKey(
                        name: "FK_productsCategories_categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_productsCategories_products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ratings",
                columns: table => new
                {
                    RatingId = table.Column<Guid>(type: "uuid", nullable: false),
                    Rate = table.Column<int>(type: "integer", nullable: false),
                    LikeCount = table.Column<int>(type: "integer", nullable: false),
                    DislikeCount = table.Column<int>(type: "integer", nullable: false),
                    Comment = table.Column<string>(type: "text", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    ProductId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ratings", x => x.RatingId);
                    table.ForeignKey(
                        name: "FK_ratings_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ratings_products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "CreatedAt", "CreatedBy", "LastModifiedAt", "LastModifiedBy", "Name", "NormalizedName", "Permissions" },
                values: new object[,]
                {
                    { new Guid("2e4d5f86-82ab-41c5-bbc3-d21e8f0b8a2c"), "b56cfcdc-2d16-42fa-b28b-30c1f932c98a", null, null, null, null, "Customer", "CUSTOMER", 96 },
                    { new Guid("3f72b4a3-bc5c-4464-9b22-cb8197745345"), "6fca332c-5d9b-465d-ae83-39546e74f214", null, null, null, null, "Vendor", "VENDOR", 48 },
                    { new Guid("6b03a8e9-d90e-404d-b1a7-51ed6702f4be"), "112d11bc-9fe4-4e41-a215-e01eb6d9ad72", null, null, null, null, "Admin", "ADMIN", 56 },
                    { new Guid("d24e2067-471f-4a8d-8d13-72f4a57b8f32"), "edd5ca22-b0cc-4a2b-b817-8dc46de707d8", null, null, null, null, "SuperAdmin", "SUPERADMIN", -1 }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "CodeForResetPassword", "ConcurrencyStamp", "Discriminator", "Email", "EmailConfirmed", "FullName", "IsCodeOfResetPasswordTrue", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "Picture", "RefreshToken", "RefreshTokenExpiryTime", "SecurityStamp", "TimeOfCodeExpiration", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { new Guid("3f42b4a3-bc5c-4464-9b22-cb819774539f"), 0, null, "dc830f49-cb83-4c76-8267-ba596b51cb10", "User", "admin@gmail.com", true, "Admin User", null, false, null, "ADMIN@GMAIL.COM", "ADMIN@GMAIL.COM", "AQAAAAIAAYagAAAAEIxDDO2nnhjTwNhf5zOa6ZkUPkllbQW7Qm+ka5EkoXGoZDYd+JlgRq3BSVNBqihgzA==", null, false, null, null, null, "7ZZZLCDPUZ2CY6CYAEPQLQ6O6WEROFTD", null, false, "AdminUser" },
                    { new Guid("5f87441a-8951-4535-a9c6-f4e3073ab1d7"), 0, null, "e82b1632-39ae-4df8-be36-005ab707f670", "User", "superadmin@gmail.com", true, "SuperAdmin User", null, false, null, "SUPERADMIN@GMAIL.COM", "SUPERADMIN@GMAIL.COM", "AQAAAAIAAYagAAAAEAV2jpyyf/CumcvhCxivo9uMSs/wKcwSeC556GGAv3MADr93vDECp3G9dgs8eOyt/g==", null, false, null, null, null, "7ZZZLCDPUZ2CY6CYAEPQLQ6O6DDDFRDE", null, false, "SuperAdminUser" },
                    { new Guid("8c528156-1623-41f9-bf02-d5e47a4a66d4"), 0, null, "4d94c7e6-ff3b-4a9f-8347-088a624cfcd2", "User", "vendor@gmail.com", true, "Vendor User", null, false, null, "VENDOR@GMAIL.COM", "VENDOR@GMAIL.COM", "AQAAAAIAAYagAAAAEA6YoDM0V6hrMt//qkjTdrnVbr0ig7Xn2W1tVZ/1pIl4mHUzsKUemOtpz/VBWOlGSw==", null, false, null, null, null, "7ZZZLCDPUZ2CY6CYAEPQLQ6O6IPXFJRU", null, false, "VendorUser" },
                    { new Guid("9f83774d-1822-47a3-9e6e-0a6f89bcb7c7"), 0, null, "94402dac-8b9f-4bac-9a43-510d6e86d433", "User", "customer@gmail.com", true, "Customer User", null, false, null, "CUSTOMER@GMAIL.COM", "CUSTOMER@GMAIL.COM", "AQAAAAIAAYagAAAAEMQ3jcg7SUo+3be4AMSMBOl/Rb8Yb+MaqLg4bPGv0gRe0jtdHxBjM+p4P/o02kquMg==", null, false, null, null, null, "7ZZZLCDPUZ2CY6CYAEPQLQ6O6IPWOSKT", null, false, "CustomerUser" }
                });

            migrationBuilder.InsertData(
                table: "brands",
                columns: new[] { "Id", "AddedOnDate", "DeletedDate", "IsDeleted", "Name", "Picture", "UpdatedDate" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 8, 28, 8, 6, 18, 387, DateTimeKind.Utc).AddTicks(2003), new DateTime(2024, 8, 28, 8, 6, 18, 387, DateTimeKind.Utc).AddTicks(1931), false, "Industrial", null, new DateTime(2024, 8, 28, 8, 6, 18, 387, DateTimeKind.Utc).AddTicks(1931) },
                    { 2, new DateTime(2024, 8, 28, 8, 6, 18, 387, DateTimeKind.Utc).AddTicks(2052), new DateTime(2024, 8, 28, 8, 6, 18, 387, DateTimeKind.Utc).AddTicks(2005), true, "Sports & Beauty", null, new DateTime(2024, 8, 28, 8, 6, 18, 387, DateTimeKind.Utc).AddTicks(2004) },
                    { 3, new DateTime(2024, 8, 28, 8, 6, 18, 387, DateTimeKind.Utc).AddTicks(2062), new DateTime(2024, 8, 28, 8, 6, 18, 387, DateTimeKind.Utc).AddTicks(2054), false, "Books", null, new DateTime(2024, 8, 28, 8, 6, 18, 387, DateTimeKind.Utc).AddTicks(2053) }
                });

            migrationBuilder.InsertData(
                table: "categories",
                columns: new[] { "Id", "AddedOnDate", "DeletedDate", "IsDeleted", "Name", "ParentId", "Priorty", "UpdatedDate" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 8, 28, 8, 6, 18, 389, DateTimeKind.Utc).AddTicks(4539), new DateTime(2024, 8, 28, 8, 6, 18, 389, DateTimeKind.Utc).AddTicks(4535), false, "Electric", 0, 1, new DateTime(2024, 8, 28, 8, 6, 18, 389, DateTimeKind.Utc).AddTicks(4535) },
                    { 2, new DateTime(2024, 8, 28, 8, 6, 18, 389, DateTimeKind.Utc).AddTicks(4542), new DateTime(2024, 8, 28, 8, 6, 18, 389, DateTimeKind.Utc).AddTicks(4541), false, "ElModa", 0, 2, new DateTime(2024, 8, 28, 8, 6, 18, 389, DateTimeKind.Utc).AddTicks(4541) },
                    { 3, new DateTime(2024, 8, 28, 8, 6, 18, 389, DateTimeKind.Utc).AddTicks(4544), new DateTime(2024, 8, 28, 8, 6, 18, 389, DateTimeKind.Utc).AddTicks(4543), false, "Computer", 1, 1, new DateTime(2024, 8, 28, 8, 6, 18, 389, DateTimeKind.Utc).AddTicks(4543) },
                    { 4, new DateTime(2024, 8, 28, 8, 6, 18, 389, DateTimeKind.Utc).AddTicks(4550), new DateTime(2024, 8, 28, 8, 6, 18, 389, DateTimeKind.Utc).AddTicks(4549), false, "Women", 2, 1, new DateTime(2024, 8, 28, 8, 6, 18, 389, DateTimeKind.Utc).AddTicks(4545) }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { new Guid("6b03a8e9-d90e-404d-b1a7-51ed6702f4be"), new Guid("3f42b4a3-bc5c-4464-9b22-cb819774539f") },
                    { new Guid("d24e2067-471f-4a8d-8d13-72f4a57b8f32"), new Guid("5f87441a-8951-4535-a9c6-f4e3073ab1d7") },
                    { new Guid("3f72b4a3-bc5c-4464-9b22-cb8197745345"), new Guid("8c528156-1623-41f9-bf02-d5e47a4a66d4") },
                    { new Guid("2e4d5f86-82ab-41c5-bbc3-d21e8f0b8a2c"), new Guid("9f83774d-1822-47a3-9e6e-0a6f89bcb7c7") }
                });

            migrationBuilder.InsertData(
                table: "products",
                columns: new[] { "Id", "AddedOnDate", "BrandId", "DeletedDate", "Description", "Discount", "IsDeleted", "Price", "Title", "UpdatedDate" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 8, 28, 8, 6, 18, 391, DateTimeKind.Utc).AddTicks(5492), 1, new DateTime(2024, 8, 28, 8, 6, 18, 391, DateTimeKind.Utc).AddTicks(5347), "New range of formal shirts are designed keeping you in mind. With fits and styling that will make you stand apart", 13.683889594268600m, false, 1813.51m, "Practical Rubber Keyboard", new DateTime(2024, 8, 28, 8, 6, 18, 391, DateTimeKind.Utc).AddTicks(5347) },
                    { 2, new DateTime(2024, 8, 28, 8, 6, 18, 391, DateTimeKind.Utc).AddTicks(5519), 1, new DateTime(2024, 8, 28, 8, 6, 18, 391, DateTimeKind.Utc).AddTicks(5494), "New range of formal shirts are designed keeping you in mind. With fits and styling that will make you stand apart", 15.038294025563400m, false, 970.75m, "Tasty Fresh Chair", new DateTime(2024, 8, 28, 8, 6, 18, 391, DateTimeKind.Utc).AddTicks(5493) },
                    { 3, new DateTime(2024, 8, 28, 8, 6, 18, 391, DateTimeKind.Utc).AddTicks(5548), 3, new DateTime(2024, 8, 28, 8, 6, 18, 391, DateTimeKind.Utc).AddTicks(5520), "Boston's most advanced compression wear technology increases muscle oxygenation, stabilizes active muscles", 33.178499799026160m, true, 1495.04m, "Gorgeous Metal Hat", new DateTime(2024, 8, 28, 8, 6, 18, 391, DateTimeKind.Utc).AddTicks(5520) }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_details_CategoryId",
                table: "details",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_images_ProductId",
                table: "images",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_orderProducts_ProductId",
                table: "orderProducts",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_orders_UserId",
                table: "orders",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_products_BrandId",
                table: "products",
                column: "BrandId");

            migrationBuilder.CreateIndex(
                name: "IX_productsCategories_CategoryId",
                table: "productsCategories",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_ratings_ProductId",
                table: "ratings",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_ratings_UserId",
                table: "ratings",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "details");

            migrationBuilder.DropTable(
                name: "images");

            migrationBuilder.DropTable(
                name: "orderProducts");

            migrationBuilder.DropTable(
                name: "productsCategories");

            migrationBuilder.DropTable(
                name: "ratings");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "orders");

            migrationBuilder.DropTable(
                name: "categories");

            migrationBuilder.DropTable(
                name: "products");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "brands");
        }
    }
}
