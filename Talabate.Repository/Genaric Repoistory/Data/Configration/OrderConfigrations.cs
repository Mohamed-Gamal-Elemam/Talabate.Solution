using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using Talabate.Core.Entity.Order_Agregate;

namespace Talabate.Infrastructer.Genaric_Repoistory.Data.Configration
{
    internal class OrderConfigrations : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.OwnsOne(o => o.ShippingAddress, ShippingAddress => ShippingAddress.WithOwner());
            builder.Property(o => o.Status).HasConversion(OStutes => OStutes.ToString(), OStutes => (OrderStatus)Enum.Parse(typeof( OrderStatus),OStutes));
            builder.Property(o => o.SubTotal).HasColumnType("decimal(18,2)");
            builder.HasOne(o => o.deliveryMethod)
                .WithMany()
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
