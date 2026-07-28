using Talabate.Core.Entity.Product;

namespace Talabate.Core.Specifications.ProductSpecs
{
    public class ProductwithBrandCategorySpacification :BaseSpecifications<Product>
    {
        public ProductwithBrandCategorySpacification(ProductSpecPrams specPrams) :base(
            p=>
            (string.IsNullOrEmpty(specPrams.Search)||p.Name.ToLower().Contains(specPrams.Search))&&
            (!specPrams.BrandId.HasValue ||p.BrandId==specPrams.BrandId.Value)&&
            (!specPrams.CategoryId.HasValue ||p.CategoryId==specPrams.CategoryId.Value)
            
            )
        {
            AddIncludes();


            if (!string.IsNullOrEmpty(specPrams.Sort))
            {
                switch (specPrams.Sort)
                {
                    case "PriceAsc":
                        AddOrderBy(p => p.Price);
                            break;
                    case "PriceDes":
                        AddOrderByDesc(p => p.Price);
                        break;
                    default: AddOrderBy(p => p.Name);
                        break;
                }
            }
            else 
                AddOrderBy(p=>p.Name);



            ApplyPagination(specPrams.PageSize * (specPrams.PageIndex - 1), specPrams.PageSize);



        }
        public ProductwithBrandCategorySpacification(int id)
            :base(p => p.Id == id)
        {
            AddIncludes();

        }


       

        private void AddIncludes()
        {
            Includes.Add(p => p.Brand);
            Includes.Add(p => p.Category);
        }

    }
}
