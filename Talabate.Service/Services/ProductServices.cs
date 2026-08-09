using System;
using System.Collections.Generic;
using System.Text;
using Talabate.Core.Entity.Product;
using Talabate.Core.IServices;
using Talabate.Core.IUnitOfWork;
using Talabate.Core.Specifications.ProductSpecs;

namespace Talabate.Application.Services
{
    public class ProductServices : IProductServices
    {
        private readonly IUnitOfWork _unitOfWork;

        public ProductServices(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IReadOnlyList<Product>> GetProductsAsync(ProductSpecPrams specPrams)
        {
            var spec =new ProductwithBrandCategorySpacification(specPrams);
            var products = await _unitOfWork.Reposatory<Product>().GetAllWithSpecAsync(spec);
            return products;
        }

        public async Task<int> GetCountAsync(ProductSpecPrams specPrams)
        {
            var spec =new ProductsWithFilterationForCountSpec(specPrams);
            var count = await _unitOfWork.Reposatory<Product>().CountAsync(spec);
            return count;
        }
     
        public async Task<Product?> GetProductByIdAsync(int id)
        {
            var spec = new ProductwithBrandCategorySpacification(id);
            var Product=await _unitOfWork.Reposatory<Product>().GetWithSpecsync(spec);
            return Product;
        }
        public async Task<IReadOnlyList<ProductBrand>> GetProductBrandsAsync()
        =>await _unitOfWork.Reposatory<ProductBrand>().GetAllAsync();


        public async Task<IReadOnlyList<ProductCategory>> GetProductCategorysAsync()
       =>await _unitOfWork.Reposatory<ProductCategory>().GetAllAsync();

    }
}
