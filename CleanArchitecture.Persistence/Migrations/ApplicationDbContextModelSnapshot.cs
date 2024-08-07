﻿// <auto-generated />
using System;
using CleanArchitecture.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace CleanArchitecture.Persistence.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.7")
                .HasAnnotation("Proxies:ChangeTracking", false)
                .HasAnnotation("Proxies:CheckEquality", false)
                .HasAnnotation("Proxies:LazyLoading", true)
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("CleanArchitecture.Domain.Entities.Brand", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("AddedOnDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime?>("DeletedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Picture")
                        .HasColumnType("text");

                    b.Property<DateTime>("UpdatedDate")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.ToTable("brands");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            AddedOnDate = new DateTime(2024, 7, 31, 9, 38, 30, 651, DateTimeKind.Utc).AddTicks(2071),
                            DeletedDate = new DateTime(2024, 7, 31, 9, 38, 30, 651, DateTimeKind.Utc).AddTicks(1812),
                            IsDeleted = false,
                            Name = "Computers, Movies & Games",
                            UpdatedDate = new DateTime(2024, 7, 31, 9, 38, 30, 651, DateTimeKind.Utc).AddTicks(1812)
                        },
                        new
                        {
                            Id = 2,
                            AddedOnDate = new DateTime(2024, 7, 31, 9, 38, 30, 651, DateTimeKind.Utc).AddTicks(2095),
                            DeletedDate = new DateTime(2024, 7, 31, 9, 38, 30, 651, DateTimeKind.Utc).AddTicks(2074),
                            IsDeleted = true,
                            Name = "Industrial, Baby & Sports",
                            UpdatedDate = new DateTime(2024, 7, 31, 9, 38, 30, 651, DateTimeKind.Utc).AddTicks(2074)
                        },
                        new
                        {
                            Id = 3,
                            AddedOnDate = new DateTime(2024, 7, 31, 9, 38, 30, 651, DateTimeKind.Utc).AddTicks(2101),
                            DeletedDate = new DateTime(2024, 7, 31, 9, 38, 30, 651, DateTimeKind.Utc).AddTicks(2097),
                            IsDeleted = false,
                            Name = "Baby",
                            UpdatedDate = new DateTime(2024, 7, 31, 9, 38, 30, 651, DateTimeKind.Utc).AddTicks(2096)
                        });
                });

            modelBuilder.Entity("CleanArchitecture.Domain.Entities.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("AddedOnDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime?>("DeletedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("ParentId")
                        .HasColumnType("integer");

                    b.Property<int>("Priorty")
                        .HasColumnType("integer");

                    b.Property<DateTime>("UpdatedDate")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.ToTable("categories");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            AddedOnDate = new DateTime(2024, 7, 31, 9, 38, 30, 653, DateTimeKind.Utc).AddTicks(654),
                            DeletedDate = new DateTime(2024, 7, 31, 9, 38, 30, 653, DateTimeKind.Utc).AddTicks(651),
                            IsDeleted = false,
                            Name = "Electric",
                            ParentId = 0,
                            Priorty = 1,
                            UpdatedDate = new DateTime(2024, 7, 31, 9, 38, 30, 653, DateTimeKind.Utc).AddTicks(650)
                        },
                        new
                        {
                            Id = 2,
                            AddedOnDate = new DateTime(2024, 7, 31, 9, 38, 30, 653, DateTimeKind.Utc).AddTicks(657),
                            DeletedDate = new DateTime(2024, 7, 31, 9, 38, 30, 653, DateTimeKind.Utc).AddTicks(656),
                            IsDeleted = false,
                            Name = "ElModa",
                            ParentId = 0,
                            Priorty = 2,
                            UpdatedDate = new DateTime(2024, 7, 31, 9, 38, 30, 653, DateTimeKind.Utc).AddTicks(656)
                        },
                        new
                        {
                            Id = 3,
                            AddedOnDate = new DateTime(2024, 7, 31, 9, 38, 30, 653, DateTimeKind.Utc).AddTicks(659),
                            DeletedDate = new DateTime(2024, 7, 31, 9, 38, 30, 653, DateTimeKind.Utc).AddTicks(658),
                            IsDeleted = false,
                            Name = "Computer",
                            ParentId = 1,
                            Priorty = 1,
                            UpdatedDate = new DateTime(2024, 7, 31, 9, 38, 30, 653, DateTimeKind.Utc).AddTicks(658)
                        },
                        new
                        {
                            Id = 4,
                            AddedOnDate = new DateTime(2024, 7, 31, 9, 38, 30, 653, DateTimeKind.Utc).AddTicks(661),
                            DeletedDate = new DateTime(2024, 7, 31, 9, 38, 30, 653, DateTimeKind.Utc).AddTicks(660),
                            IsDeleted = false,
                            Name = "Women",
                            ParentId = 2,
                            Priorty = 1,
                            UpdatedDate = new DateTime(2024, 7, 31, 9, 38, 30, 653, DateTimeKind.Utc).AddTicks(660)
                        });
                });

            modelBuilder.Entity("CleanArchitecture.Domain.Entities.Details", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("AddedOnDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("CategoryId")
                        .HasColumnType("integer");

                    b.Property<DateTime?>("DeletedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("UpdatedDate")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.ToTable("details");
                });

            modelBuilder.Entity("CleanArchitecture.Domain.Entities.Image", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("AddedOnDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime?>("DeletedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("FileName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<string>("Path")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("ProductId")
                        .HasColumnType("integer");

                    b.Property<DateTime>("UpdatedDate")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.HasIndex("ProductId");

                    b.ToTable("images");
                });

            modelBuilder.Entity("CleanArchitecture.Domain.Entities.Order", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("AddedOnDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime?>("DeletedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<byte>("OrderType")
                        .HasColumnType("smallint");

                    b.Property<decimal>("TotalAmount")
                        .HasColumnType("numeric");

                    b.Property<DateTime>("UpdatedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("orders");
                });

            modelBuilder.Entity("CleanArchitecture.Domain.Entities.Product", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("AddedOnDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("BrandId")
                        .HasColumnType("integer");

                    b.Property<DateTime?>("DeletedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<decimal>("Discount")
                        .HasColumnType("numeric");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<decimal>("Price")
                        .HasColumnType("numeric");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("UpdatedDate")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.HasIndex("BrandId");

                    b.ToTable("products");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            AddedOnDate = new DateTime(2024, 7, 31, 9, 38, 30, 654, DateTimeKind.Utc).AddTicks(8822),
                            BrandId = 1,
                            DeletedDate = new DateTime(2024, 7, 31, 9, 38, 30, 654, DateTimeKind.Utc).AddTicks(8691),
                            Description = "Andy shoes are designed to keeping in mind durability as well as trends, the most stylish range of shoes & sandals",
                            Discount = 12.847399571065920m,
                            IsDeleted = false,
                            Price = 1706.64m,
                            Title = "Awesome Wooden Shoes",
                            UpdatedDate = new DateTime(2024, 7, 31, 9, 38, 30, 654, DateTimeKind.Utc).AddTicks(8691)
                        },
                        new
                        {
                            Id = 2,
                            AddedOnDate = new DateTime(2024, 7, 31, 9, 38, 30, 654, DateTimeKind.Utc).AddTicks(8899),
                            BrandId = 1,
                            DeletedDate = new DateTime(2024, 7, 31, 9, 38, 30, 654, DateTimeKind.Utc).AddTicks(8825),
                            Description = "New ABC 13 9370, 13.3, 5th Gen CoreA5-8250U, 8GB RAM, 256GB SSD, power UHD Graphics, OS 10 Home, OS Office A & J 2016",
                            Discount = 17.974627559116560m,
                            IsDeleted = false,
                            Price = 1385.06m,
                            Title = "Sleek Frozen Chair",
                            UpdatedDate = new DateTime(2024, 7, 31, 9, 38, 30, 654, DateTimeKind.Utc).AddTicks(8824)
                        },
                        new
                        {
                            Id = 3,
                            AddedOnDate = new DateTime(2024, 7, 31, 9, 38, 30, 654, DateTimeKind.Utc).AddTicks(8926),
                            BrandId = 3,
                            DeletedDate = new DateTime(2024, 7, 31, 9, 38, 30, 654, DateTimeKind.Utc).AddTicks(8901),
                            Description = "New range of formal shirts are designed keeping you in mind. With fits and styling that will make you stand apart",
                            Discount = 19.635343844184880m,
                            IsDeleted = true,
                            Price = 1549.83m,
                            Title = "Refined Concrete Chips",
                            UpdatedDate = new DateTime(2024, 7, 31, 9, 38, 30, 654, DateTimeKind.Utc).AddTicks(8900)
                        });
                });

            modelBuilder.Entity("CleanArchitecture.Domain.Entities.ProductsCategories", b =>
                {
                    b.Property<int>("ProductId")
                        .HasColumnType("integer");

                    b.Property<int>("CategoryId")
                        .HasColumnType("integer");

                    b.HasKey("ProductId", "CategoryId");

                    b.HasIndex("CategoryId");

                    b.ToTable("productsCategories");
                });

            modelBuilder.Entity("CleanArchitecture.Domain.Entities.ProductsOrders", b =>
                {
                    b.Property<int>("OrderId")
                        .HasColumnType("integer");

                    b.Property<int>("ProductId")
                        .HasColumnType("integer");

                    b.HasKey("OrderId", "ProductId");

                    b.HasIndex("ProductId");

                    b.ToTable("orderProducts");
                });

            modelBuilder.Entity("CleanArchitecture.Domain.Entities.Rating", b =>
                {
                    b.Property<Guid>("RatingId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Comment")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("DislikeCount")
                        .HasColumnType("integer");

                    b.Property<int>("LikeCount")
                        .HasColumnType("integer");

                    b.Property<int>("ProductId")
                        .HasColumnType("integer");

                    b.Property<int>("Rate")
                        .HasColumnType("integer");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.HasKey("RatingId");

                    b.HasIndex("ProductId");

                    b.HasIndex("UserId");

                    b.ToTable("ratings");
                });

            modelBuilder.Entity("CleanArchitecture.Domain.Entities.Role", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex");

                    b.ToTable("AspNetRoles", (string)null);

                    b.HasData(
                        new
                        {
                            Id = new Guid("f60340eb-2b78-43a4-a1fe-b6c248c57f6c"),
                            ConcurrencyStamp = "042c4b5c-62fc-420f-ae6e-9c6b39e832c4",
                            Name = "Admin",
                            NormalizedName = "ADMIN"
                        },
                        new
                        {
                            Id = new Guid("4c084bf1-92e5-491b-8606-2b743eb1f0a1"),
                            ConcurrencyStamp = "fb671218-ecf0-488d-90a3-3678833dd80a",
                            Name = "User",
                            NormalizedName = "USER"
                        },
                        new
                        {
                            Id = new Guid("d5bbdcc2-68f6-43f2-8bef-a1d3d75dd900"),
                            ConcurrencyStamp = "7df9053a-1dd1-4bca-9366-c29cd3ede4e4",
                            Name = "Vendor",
                            NormalizedName = "VENDOR"
                        });
                });

            modelBuilder.Entity("CleanArchitecture.Domain.Entities.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("integer");

                    b.Property<string>("CodeForResetPassword")
                        .HasColumnType("text");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("text");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasMaxLength(8)
                        .HasColumnType("character varying(8)");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("boolean");

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<bool?>("IsCodeOfResetPasswordTrue")
                        .HasColumnType("boolean");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("boolean");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("text");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("text");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("boolean");

                    b.Property<string>("Picture")
                        .HasColumnType("text");

                    b.Property<string>("RefreshToken")
                        .HasColumnType("text");

                    b.Property<DateTime?>("RefreshTokenExpiryTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("text");

                    b.Property<DateTime?>("TimeOfCodeExpiration")
                        .HasColumnType("timestamp with time zone");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("boolean");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex");

                    b.ToTable("AspNetUsers", (string)null);

                    b.HasDiscriminator().HasValue("User");

                    b.UseTphMappingStrategy();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<System.Guid>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("text");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("text");

                    b.Property<Guid>("RoleId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<System.Guid>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("text");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("text");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<System.Guid>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("text");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("text");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("text");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<System.Guid>", b =>
                {
                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("RoleId")
                        .HasColumnType("uuid");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<System.Guid>", b =>
                {
                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<string>("Value")
                        .HasColumnType("text");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("CleanArchitecture.Domain.Entities.Vendor", b =>
                {
                    b.HasBaseType("CleanArchitecture.Domain.Entities.User");

                    b.HasDiscriminator().HasValue("Vendor");
                });

            modelBuilder.Entity("CleanArchitecture.Domain.Entities.Details", b =>
                {
                    b.HasOne("CleanArchitecture.Domain.Entities.Category", "Category")
                        .WithMany("Details")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");
                });

            modelBuilder.Entity("CleanArchitecture.Domain.Entities.Image", b =>
                {
                    b.HasOne("CleanArchitecture.Domain.Entities.Product", "Product")
                        .WithMany("images")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Product");
                });

            modelBuilder.Entity("CleanArchitecture.Domain.Entities.Order", b =>
                {
                    b.HasOne("CleanArchitecture.Domain.Entities.User", "user")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("user");
                });

            modelBuilder.Entity("CleanArchitecture.Domain.Entities.Product", b =>
                {
                    b.HasOne("CleanArchitecture.Domain.Entities.Brand", "Brand")
                        .WithMany("products")
                        .HasForeignKey("BrandId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Brand");
                });

            modelBuilder.Entity("CleanArchitecture.Domain.Entities.ProductsCategories", b =>
                {
                    b.HasOne("CleanArchitecture.Domain.Entities.Category", "Category")
                        .WithMany("ProductsCategory")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CleanArchitecture.Domain.Entities.Product", "Product")
                        .WithMany("ProductsCategory")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("CleanArchitecture.Domain.Entities.ProductsOrders", b =>
                {
                    b.HasOne("CleanArchitecture.Domain.Entities.Order", "Order")
                        .WithMany("ProductsOrders")
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CleanArchitecture.Domain.Entities.Product", "product")
                        .WithMany("ProductsOrders")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Order");

                    b.Navigation("product");
                });

            modelBuilder.Entity("CleanArchitecture.Domain.Entities.Rating", b =>
                {
                    b.HasOne("CleanArchitecture.Domain.Entities.Product", "Product")
                        .WithMany("Ratings")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CleanArchitecture.Domain.Entities.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Product");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<System.Guid>", b =>
                {
                    b.HasOne("CleanArchitecture.Domain.Entities.Role", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<System.Guid>", b =>
                {
                    b.HasOne("CleanArchitecture.Domain.Entities.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<System.Guid>", b =>
                {
                    b.HasOne("CleanArchitecture.Domain.Entities.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<System.Guid>", b =>
                {
                    b.HasOne("CleanArchitecture.Domain.Entities.Role", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CleanArchitecture.Domain.Entities.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<System.Guid>", b =>
                {
                    b.HasOne("CleanArchitecture.Domain.Entities.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("CleanArchitecture.Domain.Entities.Brand", b =>
                {
                    b.Navigation("products");
                });

            modelBuilder.Entity("CleanArchitecture.Domain.Entities.Category", b =>
                {
                    b.Navigation("Details");

                    b.Navigation("ProductsCategory");
                });

            modelBuilder.Entity("CleanArchitecture.Domain.Entities.Order", b =>
                {
                    b.Navigation("ProductsOrders");
                });

            modelBuilder.Entity("CleanArchitecture.Domain.Entities.Product", b =>
                {
                    b.Navigation("ProductsCategory");

                    b.Navigation("ProductsOrders");

                    b.Navigation("Ratings");

                    b.Navigation("images");
                });
#pragma warning restore 612, 618
        }
    }
}
