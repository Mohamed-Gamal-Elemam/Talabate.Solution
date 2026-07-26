using System;
using System.Collections.Generic;
using System.Text;

namespace Talabate.Core.Specifications.ProductSpecs
{
    public class ProductSpecPrams
    {
        private int pageSize=5;

        public string? Sort { get; set; }
        public int? BrandId { get; set; }   
        public int? CategoryId { get; set; }
        public int PageSize { get => pageSize; set => pageSize =value > 10 ? pageSize =10 : value; }
        public int PageIndex { get; set; } = 1;
        private string search;

        public string Search
        {
            get { return search; }
            set { search = value.ToLower() ; }
        }


    }
}
