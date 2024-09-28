global using Domain.Contracts;
using presentence.Data.DbContexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

namespace Persistence.Seeding.Class_Seeding
{
    public class Dbinitializer : IDbinitializer
    {
        private readonly StoreContex _storeContex;

        public Dbinitializer(StoreContex storeContex)
        {
            _storeContex = storeContex;
        }

        public async Task InitializAsync()
        {

            try
            {


            // create database if it doesn`t Exist And apply any pending migration ( updata database يعني هيتعمل )

                if (_storeContex.Database.GetPendingMigrations().Any())
                {
                    await _storeContex.Database.MigrateAsync();
                }
                if (!_storeContex.productTypes.Any()) // _storeContex.productTypes.Count() > 0
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
                        await _storeContex.productTypes.AddRangeAsync(ListOfProductTypes);
                        await _storeContex.SaveChangesAsync();
                    }

                }

                if (!_storeContex.productBrands.Any())
                {
                    var BrandsString = await File.ReadAllTextAsync(@"..\infrastructure\presentence\Seeding\Data Seeding\brands.json");

                    var ListOfBrands = JsonSerializer.Deserialize<List<ProductBrand>>(BrandsString);

                    if (ListOfBrands is not null && ListOfBrands.Any())
                    {
                        await _storeContex.productBrands.AddRangeAsync(ListOfBrands);
                        await _storeContex.SaveChangesAsync();
                    }
                }

                if (!_storeContex.products.Any())
                {
                    var ProductsString = await File.ReadAllTextAsync(@"..\infrastructure\presentence\Seeding\Data Seeding\products.json");
                    var ListOfProducts = JsonSerializer.Deserialize<List<Product>>(ProductsString);
                    if (ListOfProducts is not null && ListOfProducts.Any())
                    {
                        await _storeContex.products.AddRangeAsync(ListOfProducts);
                        await _storeContex.SaveChangesAsync();
                    }


                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}












