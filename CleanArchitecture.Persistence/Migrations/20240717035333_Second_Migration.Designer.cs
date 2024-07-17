﻿// <auto-generated />
using System;
using CleanArchitecture.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace CleanArchitecture.Persistence.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20240717035333_Second_Migration")]
    partial class Second_Migration
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
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
                            AddedOnDate = new DateTime(2024, 7, 17, 3, 53, 33, 111, DateTimeKind.Utc).AddTicks(2740),
                            DeletedDate = new DateTime(2024, 7, 17, 3, 53, 33, 111, DateTimeKind.Utc).AddTicks(2583),
                            IsDeleted = false,
                            Name = "Industrial & Automotive",
                            UpdatedDate = new DateTime(2024, 7, 17, 3, 53, 33, 111, DateTimeKind.Utc).AddTicks(2583)
                        },
                        new
                        {
                            Id = 2,
                            AddedOnDate = new DateTime(2024, 7, 17, 3, 53, 33, 111, DateTimeKind.Utc).AddTicks(2760),
                            DeletedDate = new DateTime(2024, 7, 17, 3, 53, 33, 111, DateTimeKind.Utc).AddTicks(2742),
                            IsDeleted = true,
                            Name = "Garden, Books & Electronics",
                            UpdatedDate = new DateTime(2024, 7, 17, 3, 53, 33, 111, DateTimeKind.Utc).AddTicks(2742)
                        },
                        new
                        {
                            Id = 3,
                            AddedOnDate = new DateTime(2024, 7, 17, 3, 53, 33, 111, DateTimeKind.Utc).AddTicks(2773),
                            DeletedDate = new DateTime(2024, 7, 17, 3, 53, 33, 111, DateTimeKind.Utc).AddTicks(2761),
                            IsDeleted = false,
                            Name = "Health, Home & Shoes",
                            UpdatedDate = new DateTime(2024, 7, 17, 3, 53, 33, 111, DateTimeKind.Utc).AddTicks(2761)
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
                            AddedOnDate = new DateTime(2024, 7, 17, 3, 53, 33, 113, DateTimeKind.Utc).AddTicks(4633),
                            DeletedDate = new DateTime(2024, 7, 17, 3, 53, 33, 113, DateTimeKind.Utc).AddTicks(4626),
                            IsDeleted = false,
                            Name = "Electric",
                            ParentId = 0,
                            Priorty = 1,
                            UpdatedDate = new DateTime(2024, 7, 17, 3, 53, 33, 113, DateTimeKind.Utc).AddTicks(4626)
                        },
                        new
                        {
                            Id = 2,
                            AddedOnDate = new DateTime(2024, 7, 17, 3, 53, 33, 113, DateTimeKind.Utc).AddTicks(4636),
                            DeletedDate = new DateTime(2024, 7, 17, 3, 53, 33, 113, DateTimeKind.Utc).AddTicks(4635),
                            IsDeleted = false,
                            Name = "ElModa",
                            ParentId = 0,
                            Priorty = 2,
                            UpdatedDate = new DateTime(2024, 7, 17, 3, 53, 33, 113, DateTimeKind.Utc).AddTicks(4635)
                        },
                        new
                        {
                            Id = 3,
                            AddedOnDate = new DateTime(2024, 7, 17, 3, 53, 33, 113, DateTimeKind.Utc).AddTicks(4638),
                            DeletedDate = new DateTime(2024, 7, 17, 3, 53, 33, 113, DateTimeKind.Utc).AddTicks(4637),
                            IsDeleted = false,
                            Name = "Computer",
                            ParentId = 1,
                            Priorty = 1,
                            UpdatedDate = new DateTime(2024, 7, 17, 3, 53, 33, 113, DateTimeKind.Utc).AddTicks(4637)
                        },
                        new
                        {
                            Id = 4,
                            AddedOnDate = new DateTime(2024, 7, 17, 3, 53, 33, 113, DateTimeKind.Utc).AddTicks(4640),
                            DeletedDate = new DateTime(2024, 7, 17, 3, 53, 33, 113, DateTimeKind.Utc).AddTicks(4639),
                            IsDeleted = false,
                            Name = "Women",
                            ParentId = 2,
                            Priorty = 1,
                            UpdatedDate = new DateTime(2024, 7, 17, 3, 53, 33, 113, DateTimeKind.Utc).AddTicks(4639)
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

                    b.HasData(
                        new
                        {
                            Id = 1,
                            AddedOnDate = new DateTime(2024, 7, 17, 3, 53, 33, 115, DateTimeKind.Utc).AddTicks(8279),
                            CategoryId = 1,
                            DeletedDate = new DateTime(2024, 7, 17, 3, 53, 33, 115, DateTimeKind.Utc).AddTicks(8159),
                            Description = "Eum repellat nisi laboriosam et.",
                            IsDeleted = false,
                            Title = "Eius.",
                            UpdatedDate = new DateTime(2024, 7, 17, 3, 53, 33, 115, DateTimeKind.Utc).AddTicks(8159)
                        },
                        new
                        {
                            Id = 2,
                            AddedOnDate = new DateTime(2024, 7, 17, 3, 53, 33, 115, DateTimeKind.Utc).AddTicks(8315),
                            CategoryId = 3,
                            DeletedDate = new DateTime(2024, 7, 17, 3, 53, 33, 115, DateTimeKind.Utc).AddTicks(8281),
                            Description = "Sunt quia veritatis explicabo et.",
                            IsDeleted = true,
                            Title = "Qui magni.",
                            UpdatedDate = new DateTime(2024, 7, 17, 3, 53, 33, 115, DateTimeKind.Utc).AddTicks(8281)
                        },
                        new
                        {
                            Id = 3,
                            AddedOnDate = new DateTime(2024, 7, 17, 3, 53, 33, 115, DateTimeKind.Utc).AddTicks(8397),
                            CategoryId = 4,
                            DeletedDate = new DateTime(2024, 7, 17, 3, 53, 33, 115, DateTimeKind.Utc).AddTicks(8316),
                            Description = "Non incidunt dolorem error tenetur.",
                            IsDeleted = false,
                            Title = "Illum.",
                            UpdatedDate = new DateTime(2024, 7, 17, 3, 53, 33, 115, DateTimeKind.Utc).AddTicks(8316)
                        });
                });

            modelBuilder.Entity("CleanArchitecture.Domain.Entities.DeviceInfo", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Browser")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("DeviceType")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("IpAddress")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("LoggedInAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.ToTable("deviceInfo");
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
                            AddedOnDate = new DateTime(2024, 7, 17, 3, 53, 33, 117, DateTimeKind.Utc).AddTicks(6331),
                            BrandId = 1,
                            DeletedDate = new DateTime(2024, 7, 17, 3, 53, 33, 117, DateTimeKind.Utc).AddTicks(6222),
                            Description = "The Football Is Good For Training And Recreational Purposes",
                            Discount = 16.837235113216280m,
                            IsDeleted = false,
                            Price = 568.19m,
                            Title = "Incredible Fresh Gloves",
                            UpdatedDate = new DateTime(2024, 7, 17, 3, 53, 33, 117, DateTimeKind.Utc).AddTicks(6222)
                        },
                        new
                        {
                            Id = 2,
                            AddedOnDate = new DateTime(2024, 7, 17, 3, 53, 33, 117, DateTimeKind.Utc).AddTicks(6355),
                            BrandId = 1,
                            DeletedDate = new DateTime(2024, 7, 17, 3, 53, 33, 117, DateTimeKind.Utc).AddTicks(6333),
                            Description = "The Nagasaki Lander is the trademarked name of several series of Nagasaki sport bikes, that started with the 1984 ABC800J",
                            Discount = 42.078652759450680m,
                            IsDeleted = false,
                            Price = 1237.39m,
                            Title = "Small Cotton Computer",
                            UpdatedDate = new DateTime(2024, 7, 17, 3, 53, 33, 117, DateTimeKind.Utc).AddTicks(6333)
                        },
                        new
                        {
                            Id = 3,
                            AddedOnDate = new DateTime(2024, 7, 17, 3, 53, 33, 117, DateTimeKind.Utc).AddTicks(6376),
                            BrandId = 3,
                            DeletedDate = new DateTime(2024, 7, 17, 3, 53, 33, 117, DateTimeKind.Utc).AddTicks(6357),
                            Description = "Ergonomic executive chair upholstered in bonded black leather and PVC padded seat and back for all-day comfort and support",
                            Discount = 44.473588906204720m,
                            IsDeleted = true,
                            Price = 309.77m,
                            Title = "Rustic Frozen Computer",
                            UpdatedDate = new DateTime(2024, 7, 17, 3, 53, 33, 117, DateTimeKind.Utc).AddTicks(6356)
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
                            Id = new Guid("beb7ea36-7d70-41ba-91e7-6c57c7242738"),
                            ConcurrencyStamp = "a9826de7-18c6-460b-a295-85813962beb3",
                            Name = "Admin",
                            NormalizedName = "ADMIN"
                        },
                        new
                        {
                            Id = new Guid("d9113db7-fdc0-4cab-b107-53b19ecc6e4d"),
                            ConcurrencyStamp = "09684b7d-e9f8-407b-ae15-fbe790949400",
                            Name = "User",
                            NormalizedName = "USER"
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