
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace QPCore.Data.Enitites.EntityConfigurations
{
    public class WorkItemConfiguration : BaseEntityTypeConfiguration<WorkItem>
    {
        public override void CustomConfigure(EntityTypeBuilder<WorkItem> builder)
        {
            builder.ToTable("WorkItems");

            builder.HasKey(e => e.Id)
                .HasName("pk_workitem_id");

            builder.Property(e => e.Id)
                .HasIdentityOptions(startValue: 1)
                .HasColumnName("id");

            builder.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(500)
                .HasColumnName("azure_workitem_name");

            builder.Property(e => e.WorkItemTypeId)
                .HasColumnName("work_item_type_id")
                .IsRequired();
            
            builder.Property(e => e.AzureWorkItemId)
                .HasColumnName("azure_workitem_id")
                .IsRequired();

            builder.Property(e => e.AzureFeatureId)
                .HasColumnName("azure_feature_id");

            builder.Property(e => e.AzureFeatureName)
                .HasColumnName("azure_feature_name");

            builder.HasOne(e => e.WorkItemType)
                .WithMany(e => e.WorkItems)
                .HasConstraintName("fk_workitem_workitemtype_workitemtypeid")
                .HasForeignKey(e => e.WorkItemTypeId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}