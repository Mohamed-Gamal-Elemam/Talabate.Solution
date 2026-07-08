
using Talabate.Core.Entity;

namespace Talabate.Core.Specifications
{
    public class ProductwithBrandCategorySpacification :BaseSpecifications<Product>
    {
        public ProductwithBrandCategorySpacification() :base()
        {
            AddIncludes();

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
