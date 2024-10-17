global using Domain.Contracts;
using Domain.Contracts.ISeeding;
using presentence.Data.DbContexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using Domain.Contracts.IUnitOfWork;
using Persistence.Identity_DbContext;
using Microsoft.AspNetCore.Identity;
using Domain.Idntity_Entities;
namespace Persistence.Seeding.Class_Seeding
{
    public class Dbinitializer : IDbinitializer
    {
        private readonly StoreContext _storeContext;
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly StoreIdentityDbContext _storeIdentityDbContext;

        public Dbinitializer(StoreContext storeContext, 
            StoreIdentityDbContext storeIdentityDbContext, 
            RoleManager<IdentityRole> roleManager,
            UserManager<User> userManager)
        {
            _storeContext = storeContext;
            _storeIdentityDbContext = storeIdentityDbContext;
            _roleManager = roleManager;
            _userManager = userManager;
        }

        public async Task InitializAsync()
        {

            try
            {


            // create database if it doesn`t Exist And apply any pending migration ( updata database يعني هيتعمل )

                if (_storeContext.Database.GetPendingMigrations().Any())
                {
                    await _storeContext.Database.MigrateAsync();
                }
                if (!_storeContext.productTypes.Any()) // _storeContex.productTypes.Count() > 0
                {
                    // 1. read date from file ( this data is string )
                    // 2. convert string data to productTypes ( transform into c# object )
                    // 3. add to db and save changes 

                    //             1. read date from file ( this data is string )

                    var typesData = await File.ReadAllTextAsync(@"..\infrastructure\presentence\Seeding\Data Seeding\types.json");


                    //             2. convert string data to productTypes ( transform into c# object )

                    var ListOfProductTypes = JsonSerializer.Deserialize<List<ProductType>>(typesData);


                    //             3. add to db and save changes 

                    if (ListOfProductTypes is not null && ListOfProductTypes.Any())
                    {
                        await _storeContext.productTypes.AddRangeAsync(ListOfProductTypes);
                        await _storeContext.SaveChangesAsync();
                    }

                }

                if (!_storeContext.productBrands.Any())
                {
                    var BrandsString = await File.ReadAllTextAsync(@"..\infrastructure\presentence\Seeding\Data Seeding\brands.json");

                    var ListOfBrands = JsonSerializer.Deserialize<List<ProductBrand>>(BrandsString);

                    if (ListOfBrands is not null && ListOfBrands.Any())
                    {
                        await _storeContext.productBrands.AddRangeAsync(ListOfBrands);
                        await _storeContext.SaveChangesAsync();
                    }
                }

                if (!_storeContext.products.Any())
                {
                    var ProductsString = await File.ReadAllTextAsync(@"..\infrastructure\presentence\Seeding\Data Seeding\products.json");
                    var ListOfProducts = JsonSerializer.Deserialize<List<Product>>(ProductsString);
                    if (ListOfProducts is not null && ListOfProducts.Any())
                    {
                        await _storeContext.products.AddRangeAsync(ListOfProducts);
                        await _storeContext.SaveChangesAsync();
                    }


                }
            }
            catch (Exception)
            {
                throw;
            }
        }



        public async Task InitializIdentityAsync()
        {

            try
            {



                if (_storeIdentityDbContext.Database.GetPendingMigrations().Any())
                {
                    await _storeIdentityDbContext.Database.MigrateAsync();
                }

                // seed defualt role 
                if (!_roleManager.Roles.Any()) // _storeContex.productTypes.Count() > 0
                {

                    await _roleManager.CreateAsync(new IdentityRole("Admin"));
                    await _roleManager.CreateAsync(new IdentityRole("SuperAdmin"));
                }

                // seed defualt User 
                if (!_userManager.Users.Any()) // _storeContex.productTypes.Count() > 0
                {
                    var SuperAdminUser = new User()
                    {
                        DisplayName = "Super Admin User",
                        Email = "superAdminUser@gmail.com",
                        UserName = "superAdminUser",
                        PhoneNumber = "1234567890"

                    };

                    var AdminUser = new User()
                    {
                        DisplayName = "Admin User",
                        Email = "adminUser@gmail.com",
                        UserName = "adminUser",
                        PhoneNumber = "12359979997"

                    };

                    // add to DB
                    
                    await _userManager.CreateAsync(SuperAdminUser, "Hany1234##");
                    await _userManager.CreateAsync(AdminUser, "Ahmed1234#");

                    await _userManager.AddToRoleAsync(AdminUser, "Admin");
                    await _userManager.AddToRoleAsync(AdminUser, "Admin");



                }

            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}












