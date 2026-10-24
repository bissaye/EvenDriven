using EvenDriven.Domain.Primitives;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace EvenDriven.Infra.Persistence.Configurations
{
    public abstract class BaseEntityConfiguration<T> : IEntityTypeConfiguration<T> where T : BaseEntity
    {
        public void Configure(EntityTypeBuilder<T> builder)
        {
            builder.HasKey(e => e.Id);

            builder.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .IsRequired();

            builder.HasIndex(e => e.Id)
                .IsUnique(); 

            builder.Property(e => e.CreatedAt)
                .IsRequired();

            builder.Property(e => e.CreatedAt)
                .IsRequired();

            ConfigureEntity(builder);

        }

        protected abstract void ConfigureEntity(EntityTypeBuilder<T> builder);
    }
}
