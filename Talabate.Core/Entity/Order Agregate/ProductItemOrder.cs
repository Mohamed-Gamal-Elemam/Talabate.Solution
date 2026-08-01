using System;
using System.Collections.Generic;
using System.Text;

namespace Talabate.Core.Entity.Order_Agregate
{
    public class ProductItemOrder
    {
        public ProductItemOrder()
        {
            
        }

        public ProductItemOrder(int productID, string productName, string pictureUrl)
        {
            ProductID = productID;
            ProductName = productName;
            PictureUrl = pictureUrl;
        }

        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public string PictureUrl { get; set; }

    }
}
