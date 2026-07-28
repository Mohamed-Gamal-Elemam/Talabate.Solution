using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using Talabate.Core.Entity.Product;

namespace Talabate.Repository.Genaric_Repoistory.Data
{
    public static class StoreContextSeed
    {

        public static async Task SeedAsync(StoreContext _dbcontext)
        {
            if (_dbcontext.productBrands.Count()==0)
            {

              var BrandsData = File.ReadAllText("../Talabate.Repository/Data/SeedData/brands.json");
              var brands = JsonSerializer.Deserialize<List<ProductBrand>>(BrandsData);
                if(brands is not null && brands.Count > 0)
                {
                    foreach (var brand in brands)
                    {
                        _dbcontext.productBrands.Add(brand);
                    }
                    await _dbcontext.SaveChangesAsync();
                }
            }

            if (_dbcontext.productCategories.Count() == 0)
            {

                var CategoriesData = File.ReadAllText("../Talabate.Repository/Data/SeedData/categories.json");
                var categories = JsonSerializer.Deserialize<List<ProductCategory>>(CategoriesData);
                if (categories is not null && categories.Count > 0)
                {
                    foreach (var category in categories)
                    {
                        _dbcontext.productCategories.Add(category);
                    }
                    await _dbcontext.SaveChangesAsync();
                }
            }

            if (_dbcontext.products.Count() == 0)
            {

                var ProductsData = File.ReadAllText("../Talabate.Repository/Data/SeedData/products.json");
                var products = JsonSerializer.Deserialize<List<Product>>(ProductsData);
                if (products is not null && products.Count > 0)
                {
                    foreach (var product in products)
                    {
                        _dbcontext.products.Add(product);
                    }
                    await _dbcontext.SaveChangesAsync();
                }
            }


        }

       
    }
}
