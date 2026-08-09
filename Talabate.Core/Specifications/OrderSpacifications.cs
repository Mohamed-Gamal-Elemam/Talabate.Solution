using System;
using System.Collections.Generic;
using System.Text;
using Talabate.Core.Entity.Order_Agregate;

namespace Talabate.Core.Specifications
{
    public class OrderSpacifications :BaseSpecifications<Order>
    {
        public OrderSpacifications(string BuyerEmail) :base(o=>o.BuyerEmail == BuyerEmail)
        {
            Includes.Add(o => o.deliveryMethod);
            Includes.Add(o => o.Items);


            AddOrderByDesc(O => O.OrderDate);
        }

        public OrderSpacifications(int OrderId, string BuyerEmail):base(o=>o.Id== OrderId &&o.BuyerEmail==BuyerEmail ) 
        {
            Includes.Add(o => o.deliveryMethod);
            Includes.Add(o => o.Items);
        }
    }
}
