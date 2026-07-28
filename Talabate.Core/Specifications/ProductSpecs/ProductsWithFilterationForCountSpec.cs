using System;
using System.Collections.Generic;
using System.Text;
using Talabate.Core.Entity.Product;

namespace Talabate.Core.Specifications.ProductSpecs
{
    public class ProductsWithFilterationForCountSpec : BaseSpecifications<Product>
    {
        public ProductsWithFilterationForCountSpec(ProductSpecPrams specPrams) : base(
            p =>
            (string.IsNullOrEmpty(specPrams.Search) || p.Name.ToLower().Contains(specPrams.Search)) &&
            (!specPrams.BrandId.HasValue || p.BrandId == specPrams.BrandId.Value) &&
            (!specPrams.CategoryId.HasValue || p.CategoryId == specPrams.CategoryId.Value)

            )
        {
        }
    }
}
