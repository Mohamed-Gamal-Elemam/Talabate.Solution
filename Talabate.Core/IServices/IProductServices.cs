using System;
using System.Collections.Generic;
using System.Text;
using Talabate.Core.Entity.Product;
using Talabate.Core.Specifications.ProductSpecs;

namespace Talabate.Core.IServices
{
    public interface IProductServices
    {
        Task<IReadOnlyList<Product>> GetProductsAsync(ProductSpecPrams specPrams);
        Task<Product?> GetProductByIdAsync(int id);
        Task<IReadOnlyList<ProductBrand>> GetProductBrandsAsync();
        Task<IReadOnlyList<ProductCategory>> GetProductCategorysAsync();
        Task<int> GetCountAsync(ProductSpecPrams specPrams);
    }
}
