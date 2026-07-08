using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Talabate.Core.Entity
{
    public class Product : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string PictureUrl { get; set; }
        [ForeignKey (nameof(Brand))]
        public int BrandId { get; set; }
        [ForeignKey(nameof(Category))]
        public int CategoryId { get; set; }
        public virtual ProductCategory Category { get; set; }
       
        public virtual ProductBrand Brand { get; set; }
    }
}
;