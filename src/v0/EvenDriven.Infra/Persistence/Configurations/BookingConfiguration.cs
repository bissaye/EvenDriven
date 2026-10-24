using EvenDriven.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace EvenDriven.Infra.Persistence.Configurations
{
    public sealed class BookingConfiguration : BaseEntityConfiguration<BookingEntity>
    {
        protected override void ConfigureEntity(EntityTypeBuilder<BookingEntity> builder)
        {
            builder.ToTable("Bookings");

            builder.OwnsOne(b => b.DateRange, dateRangeBuilder =>
            {
                dateRangeBuilder.Property(dr => dr.StartDate).IsRequired();
                dateRangeBuilder.Property(dr => dr.EndDate).IsRequired();
            });
        }
    }
}
