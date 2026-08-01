using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using Talabate.Core.Entity.Order_Agregate;

namespace Talabate.Infrastructer.Genaric_Repoistory.Data.Configration
{
    internal class OrderItemConfigrations : IEntityTypeConfiguration<OrderItem>
    {
        public void Configure(EntityTypeBuilder<OrderItem> builder)
        {
            builder.OwnsOne(OI => OI.Product, product => product.WithOwner());
            builder.Property(OI => OI.Price).HasColumnType("decimal(18,2)");

        }
    }
}
