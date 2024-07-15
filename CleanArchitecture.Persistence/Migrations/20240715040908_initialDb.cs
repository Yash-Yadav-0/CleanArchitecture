using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CleanArchitecture.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class initialDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
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
                name: "deviceInfo",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    DeviceType = table.Column<string>(type: "text", nullable: false),
                    IpAddress = table.Column<string>(type: "text", nullable: false),
                    Browser = table.Column<string>(type: "text", nullable: false),
                    LoggedInAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_deviceInfo", x => x.Id);
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
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("ea4d7a20-bd4f-43cb-8377-9127308e8bac"), "d5238fdd-42e5-4ee6-9c28-7d5895b7e529", "Admin", "ADMIN" },
                    { new Guid("fe7df93a-d50c-4920-a4eb-21d1d2b3f437"), "0726768a-dbba-4606-98a7-398028b11953", "User", "USER" }
                });

            migrationBuilder.InsertData(
                table: "brands",
                columns: new[] { "Id", "AddedOnDate", "DeletedDate", "IsDeleted", "Name", "Picture", "UpdatedDate" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 7, 15, 4, 9, 8, 197, DateTimeKind.Utc).AddTicks(6632), new DateTime(2024, 7, 15, 4, 9, 8, 197, DateTimeKind.Utc).AddTicks(6586), false, "Books", null, new DateTime(2024, 7, 15, 4, 9, 8, 197, DateTimeKind.Utc).AddTicks(6585) },
                    { 2, new DateTime(2024, 7, 15, 4, 9, 8, 197, DateTimeKind.Utc).AddTicks(6684), new DateTime(2024, 7, 15, 4, 9, 8, 197, DateTimeKind.Utc).AddTicks(6634), true, "Movies, Jewelery & Industrial", null, new DateTime(2024, 7, 15, 4, 9, 8, 197, DateTimeKind.Utc).AddTicks(6634) },
                    { 3, new DateTime(2024, 7, 15, 4, 9, 8, 197, DateTimeKind.Utc).AddTicks(6696), new DateTime(2024, 7, 15, 4, 9, 8, 197, DateTimeKind.Utc).AddTicks(6685), false, "Garden & Jewelery", null, new DateTime(2024, 7, 15, 4, 9, 8, 197, DateTimeKind.Utc).AddTicks(6685) }
                });

            migrationBuilder.InsertData(
                table: "categories",
                columns: new[] { "Id", "AddedOnDate", "DeletedDate", "IsDeleted", "Name", "ParentId", "Priorty", "UpdatedDate" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 7, 15, 4, 9, 8, 199, DateTimeKind.Utc).AddTicks(6260), new DateTime(2024, 7, 15, 4, 9, 8, 199, DateTimeKind.Utc).AddTicks(6256), false, "Electric", 0, 1, new DateTime(2024, 7, 15, 4, 9, 8, 199, DateTimeKind.Utc).AddTicks(6256) },
                    { 2, new DateTime(2024, 7, 15, 4, 9, 8, 199, DateTimeKind.Utc).AddTicks(6262), new DateTime(2024, 7, 15, 4, 9, 8, 199, DateTimeKind.Utc).AddTicks(6261), false, "ElModa", 0, 2, new DateTime(2024, 7, 15, 4, 9, 8, 199, DateTimeKind.Utc).AddTicks(6261) },
                    { 3, new DateTime(2024, 7, 15, 4, 9, 8, 199, DateTimeKind.Utc).AddTicks(6264), new DateTime(2024, 7, 15, 4, 9, 8, 199, DateTimeKind.Utc).AddTicks(6263), false, "Computer", 1, 1, new DateTime(2024, 7, 15, 4, 9, 8, 199, DateTimeKind.Utc).AddTicks(6263) },
                    { 4, new DateTime(2024, 7, 15, 4, 9, 8, 199, DateTimeKind.Utc).AddTicks(6266), new DateTime(2024, 7, 15, 4, 9, 8, 199, DateTimeKind.Utc).AddTicks(6265), false, "Women", 2, 1, new DateTime(2024, 7, 15, 4, 9, 8, 199, DateTimeKind.Utc).AddTicks(6265) }
                });

            migrationBuilder.InsertData(
                table: "details",
                columns: new[] { "Id", "AddedOnDate", "CategoryId", "DeletedDate", "Description", "IsDeleted", "Title", "UpdatedDate" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 7, 15, 4, 9, 8, 201, DateTimeKind.Utc).AddTicks(5790), 1, new DateTime(2024, 7, 15, 4, 9, 8, 201, DateTimeKind.Utc).AddTicks(5655), "Enim sed et debitis voluptatibus.", false, "Dolore.", new DateTime(2024, 7, 15, 4, 9, 8, 201, DateTimeKind.Utc).AddTicks(5654) },
                    { 2, new DateTime(2024, 7, 15, 4, 9, 8, 201, DateTimeKind.Utc).AddTicks(5868), 3, new DateTime(2024, 7, 15, 4, 9, 8, 201, DateTimeKind.Utc).AddTicks(5792), "Quis veritatis et iusto perferendis.", true, "Illo earum.", new DateTime(2024, 7, 15, 4, 9, 8, 201, DateTimeKind.Utc).AddTicks(5792) },
                    { 3, new DateTime(2024, 7, 15, 4, 9, 8, 201, DateTimeKind.Utc).AddTicks(5896), 4, new DateTime(2024, 7, 15, 4, 9, 8, 201, DateTimeKind.Utc).AddTicks(5869), "Eius mollitia labore architecto incidunt.", false, "Aut.", new DateTime(2024, 7, 15, 4, 9, 8, 201, DateTimeKind.Utc).AddTicks(5869) }
                });

            migrationBuilder.InsertData(
                table: "products",
                columns: new[] { "Id", "AddedOnDate", "BrandId", "DeletedDate", "Description", "Discount", "IsDeleted", "Price", "Title", "UpdatedDate" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 7, 15, 4, 9, 8, 204, DateTimeKind.Utc).AddTicks(1905), 1, new DateTime(2024, 7, 15, 4, 9, 8, 204, DateTimeKind.Utc).AddTicks(1765), "The automobile layout consists of a front-engine design, with transaxle-type transmissions mounted at the rear of the engine and four wheel drive", 40.421912349796320m, false, 1982.15m, "Ergonomic Granite Keyboard", new DateTime(2024, 7, 15, 4, 9, 8, 204, DateTimeKind.Utc).AddTicks(1765) },
                    { 2, new DateTime(2024, 7, 15, 4, 9, 8, 204, DateTimeKind.Utc).AddTicks(1932), 1, new DateTime(2024, 7, 15, 4, 9, 8, 204, DateTimeKind.Utc).AddTicks(1908), "The Football Is Good For Training And Recreational Purposes", 40.879096004844680m, false, 1389.95m, "Gorgeous Metal Chicken", new DateTime(2024, 7, 15, 4, 9, 8, 204, DateTimeKind.Utc).AddTicks(1907) },
                    { 3, new DateTime(2024, 7, 15, 4, 9, 8, 204, DateTimeKind.Utc).AddTicks(1950), 3, new DateTime(2024, 7, 15, 4, 9, 8, 204, DateTimeKind.Utc).AddTicks(1933), "The Football Is Good For Training And Recreational Purposes", 12.3628725804131680m, true, 1993.91m, "Incredible Steel Cheese", new DateTime(2024, 7, 15, 4, 9, 8, 204, DateTimeKind.Utc).AddTicks(1933) }
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
                name: "deviceInfo");

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
