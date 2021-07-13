using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace QPCore.Data.Enitites.EntityConfigurations
{
    public class WorkItemTypeConfiguration : BaseEntityTypeConfiguration<WorkItemType>
    {
        public override void CustomConfigure(EntityTypeBuilder<WorkItemType> entity)
        {
            entity.ToTable("WorkItemTypes");

            entity.HasKey(e => e.Id)
                .HasName("pk_workitemtype_id");

            entity.Property(e => e.Id)
                .HasColumnName("id");
            
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasColumnName("name")
                .IsRequired();

            entity.HasIndex(e => e.Name)
                .HasDatabaseName("uq_workiten_type_name")
                .IsUnique();
        }
    }
}