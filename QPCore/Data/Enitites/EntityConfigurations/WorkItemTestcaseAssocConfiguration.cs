using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace QPCore.Data.Enitites.EntityConfigurations
{
    public class WorkItemTestcaseAssocConfiguration : BaseEntityTypeConfiguration<WorkItemTestcaseAssoc>
    {
        public override void CustomConfigure(EntityTypeBuilder<WorkItemTestcaseAssoc> builder)
        {
            builder.ToTable("WorkItemTestcaseAssocs");

            builder.HasKey(k => k.Id)
                .HasName("pk_workitem_testcase_assoc_id");
            
            builder.Property(e => e.Id)
                .HasColumnName("id")
                .HasIdentityOptions(startValue: 1);

            builder.Ignore(e => e.Name);

            builder.Property(e => e.WorkItemId)
                .HasColumnName("workitem_id")
                .IsRequired();

            builder.Property(e => e.TestcaseId)
                .HasColumnName("testflow_id")
                .IsRequired();

            builder.HasOne(e => e.WorkItem)
                .WithMany(e => e.WorkItemTestcaseAssocs)
                .HasForeignKey(e => e.WorkItemId)
                .HasConstraintName("fk_workitem_workitemtestcaseassoc_workitem_id")
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(e => e.Testcase)
                .WithMany(e => e.WorkItemTestcaseAssocs)
                .HasForeignKey(e => e.TestcaseId)
                .HasConstraintName("fk_testflow_workitemtestcaseassoc_testflow_id")
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}