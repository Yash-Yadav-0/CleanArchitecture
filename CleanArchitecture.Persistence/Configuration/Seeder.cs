using CleanArchitecture.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CleanArchitecture.Persistence.Configuration
{
    public static class Seeder
    {
        public static void SeedRoles(this ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<Role>()
                .HasData(new List<Role>()
                {
                    new ()
                    {
                        Id = Guid.Parse("d24e2067-471f-4a8d-8d13-72f4a57b8f32"),
                        Name = "SuperAdmin",
                        NormalizedName = "SUPERADMIN",
                        ConcurrencyStamp = Guid.NewGuid().ToString(),
                        Permissions = Permissions.All,
                    },
                    new ()
                    {
                        Id = Guid.Parse("6b03a8e9-d90e-404d-b1a7-51ed6702f4be"),
                        Name = "Admin",
                        NormalizedName = "ADMIN",
                        ConcurrencyStamp = Guid.NewGuid().ToString(),
                        Permissions = Permissions.ManageUsers | Permissions.ManageProducts | Permissions.ViewOrder,
                    },
                    new ()
                    {
                        Id = Guid.Parse("3f72b4a3-bc5c-4464-9b22-cb8197745345"),
                        Name = "Vendor",
                        NormalizedName = "VENDOR",
                        ConcurrencyStamp = Guid.NewGuid().ToString(),
                        Permissions = Permissions.ManageProducts | Permissions.ViewOrder,
                    },
                    new ()
                    {
                        Id = Guid.Parse("2e4d5f86-82ab-41c5-bbc3-d21e8f0b8a2c"),
                        Name = "Customer",
                        NormalizedName = "CUSTOMER",
                        ConcurrencyStamp = Guid.NewGuid().ToString(),
                        Permissions = Permissions.ViewOrder | Permissions.ManageOrder,
                    }
                });
        }

        public static void SeedUsers(this ModelBuilder modelBuilder)
        {
            var hasher = new PasswordHasher<User>(); // Ensure you use the same hasher

            modelBuilder
                .Entity<User>()
                .HasData(new List<User>
                {
                    new()
                    {
                        Id = Guid.Parse("5f87441a-8951-4535-a9c6-f4e3073ab1d7"),
                        FullName = "SuperAdmin User",
                        UserName = "SuperAdminUser",
                        NormalizedUserName = "SUPERADMIN@GMAIL.COM",
                        Email = "superadmin@gmail.com",
                        NormalizedEmail = "SUPERADMIN@GMAIL.COM",
                        EmailConfirmed = true,
                        SecurityStamp = "7ZZZLCDPUZ2CY6CYAEPQLQ6O6DDDFRDE",
                        
                        PasswordHash = hasher.HashPassword(null, "SuperAdmin@123"), // Use the same hashing method
                        Picture = null,
                    },
                    new()
                    {
                        Id = Guid.Parse("3f42b4a3-bc5c-4464-9b22-cb819774539f"),
                        FullName = "Admin User",
                        UserName = "AdminUser",
                        NormalizedUserName = "ADMIN@GMAIL.COM",
                        Email = "admin@gmail.com",
                        NormalizedEmail = "ADMIN@GMAIL.COM",
                        EmailConfirmed = true,
                        SecurityStamp = "7ZZZLCDPUZ2CY6CYAEPQLQ6O6WEROFTD",
                        PasswordHash = hasher.HashPassword(null, "Admin@123"),
                        Picture = null,
                    },
                    new()
                    {
                        Id = Guid.Parse("8c528156-1623-41f9-bf02-d5e47a4a66d4"),
                        FullName = "Vendor User",
                        UserName = "VendorUser",
                        NormalizedUserName = "VENDOR@GMAIL.COM",
                        Email = "vendor@gmail.com",
                        NormalizedEmail = "VENDOR@GMAIL.COM",
                        EmailConfirmed = true,
                        SecurityStamp = "7ZZZLCDPUZ2CY6CYAEPQLQ6O6IPXFJRU",
                        PasswordHash = hasher.HashPassword(null, "Vendor@123"),
                        Picture = null,
                    },
                    new()
                    {
                        Id = Guid.Parse("9f83774d-1822-47a3-9e6e-0a6f89bcb7c7"),
                        FullName = "Customer User",
                        UserName = "CustomerUser",
                        NormalizedUserName = "CUSTOMER@GMAIL.COM",
                        Email = "customer@gmail.com",
                        NormalizedEmail = "CUSTOMER@GMAIL.COM",
                        EmailConfirmed = true,
                        SecurityStamp = "7ZZZLCDPUZ2CY6CYAEPQLQ6O6IPWOSKT",
                        PasswordHash = hasher.HashPassword(null, "Customer@123"),
                        Picture = null,
                    }
                });
        }

        public static void SeedUserRoles(this ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<IdentityUserRole<Guid>>()
                .HasData(new List<IdentityUserRole<Guid>>()
                {
                    new()
                    {
                        UserId = Guid.Parse("5f87441a-8951-4535-a9c6-f4e3073ab1d7"), // SuperAdminUser
                        RoleId = Guid.Parse("d24e2067-471f-4a8d-8d13-72f4a57b8f32") // SuperAdmin
                    },
                    new()
                    {
                        UserId = Guid.Parse("3f42b4a3-bc5c-4464-9b22-cb819774539f"), // AdminUser
                        RoleId = Guid.Parse("6b03a8e9-d90e-404d-b1a7-51ed6702f4be") // Admin
                    },
                    new()
                    {
                        UserId = Guid.Parse("8c528156-1623-41f9-bf02-d5e47a4a66d4"), // VendorUser
                        RoleId = Guid.Parse("3f72b4a3-bc5c-4464-9b22-cb8197745345") // Vendor
                    },
                    new()
                    {
                        UserId = Guid.Parse("9f83774d-1822-47a3-9e6e-0a6f89bcb7c7"), // CustomerUser
                        RoleId = Guid.Parse("2e4d5f86-82ab-41c5-bbc3-d21e8f0b8a2c") // Customer
                    }
                });
        }
    }
}
