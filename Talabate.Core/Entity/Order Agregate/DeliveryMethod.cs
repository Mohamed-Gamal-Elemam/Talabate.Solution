using System;
using System.Collections.Generic;
using System.Text;

namespace Talabate.Core.Entity.Order_Agregate
{
    public class DeliveryMethod :BaseEntity
    {
        public DeliveryMethod()
        {
            
        }

        public DeliveryMethod(string shortName, string deliveryTime, string description, decimal cost)
        {
            ShortName = shortName;
            DeliveryTime = deliveryTime;
            Description = description;
            Cost = cost;
        }

        public string ShortName { get; set; }

        public string DeliveryTime { get; set; }
        public string Description { get; set; }
        public decimal  Cost { get; set; }
    }
}
