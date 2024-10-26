global using OrderEntity = Domain.Entities.OrderEntites.Order;
using Domain.Entities.OrderEntites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Data.configuration
{
    public class OrderConfigration : IEntityTypeConfiguration<OrderEntity>
    {
        public void Configure(EntityTypeBuilder<OrderEntity> builder)
        {
            builder.OwnsOne(o => o.ShippingAdress, o => o.WithOwner());

            builder.HasMany(o => o.OrderItems)
                .WithOne();

            builder.Property(o => o.paymentStatus).HasConversion(

                s => s.ToString(), // db وخزنه في ال string الي enum حول ال db وانت رايح الي ال

                s => Enum.Parse<OrderPaymentStatus>(s) // enum اللي جاي الي string حول ال db وانت راجع من ال

            );

            builder.HasOne(o => o.DeliveryMethod)
                .WithMany().
                OnDelete(DeleteBehavior.SetNull);

            builder.Property(o => o.SubTotal).HasColumnType("decimal(18,3)");

        }
    }
}
