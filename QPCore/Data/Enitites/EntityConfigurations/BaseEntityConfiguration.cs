
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QPCore.Data.BaseEntites;

namespace QPCore.Data.Enitites.EntityConfigurations
{
    public abstract class BaseEntityTypeConfiguration<TBase> : IEntityTypeConfiguration<TBase> where TBase : BaseEntity
    {
        public void Configure(EntityTypeBuilder<TBase> entity)
        {
            this.CustomConfigure(entity); 

            entity.Property(e => e.CreatedBy)
                .HasColumnName("created_by")
                .IsRequired();

            entity.Property(e => e.CreatedDate)
                .HasColumnName("created_date")
                .IsRequired();

            entity.Property(e => e.UpdatedBy)
                .HasColumnName("updated_by");

            entity.Property(e => e.UpdatedDate)
                .HasColumnName("updated_date");
        }

        public abstract void CustomConfigure(EntityTypeBuilder<TBase> entity);
    }
}