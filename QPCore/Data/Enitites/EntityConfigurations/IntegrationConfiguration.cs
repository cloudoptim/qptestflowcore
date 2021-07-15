using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace QPCore.Data.Enitites.EntityConfigurations
{
    public class IntegrationConfiguration : BaseEntityTypeConfiguration<Integration>
    {
        public override void CustomConfigure(EntityTypeBuilder<Integration> entity)
        {
            entity.ToTable("Integrations");

            entity.HasKey(p => p.Id)
                .HasName("pk_intergration_id");

            entity.Property(e => e.Id)
                .HasColumnName("id")
                .HasIdentityOptions(startValue: 100);

            entity.Property(e => e.SourceId)
                .HasColumnName("source_id");

            entity.Property(e => e.UserId)
                .HasColumnName("user_id");

            entity.Property(e => e.IsActive)
                .HasColumnName("is_active")
                .IsRequired();

            entity.Ignore(e => e.Name);

            entity.Property(e => e.SourceId)
                .HasColumnName("source_id");

            entity.Property(e => e.Pat)
                .HasColumnName("pat")
                .HasMaxLength(5000)
                .IsRequired();

            entity.Property(e => e.Organization)
                .HasColumnName("organization")
                .HasMaxLength(128);

            entity.Property(e => e.Project)
                .HasColumnName("project")
                .HasMaxLength(128);

            entity.Property(e => e.Url)
                .HasColumnName("url")
                .HasMaxLength(300);

            entity.HasOne(e => e.Source)
                .WithMany(e => e.Integrations)
                .HasForeignKey(e => e.SourceId)
                .HasConstraintName("fk_integrationsource_integration_source_id")
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(e => e.User)
                .WithMany(e => e.Integrations)
                .HasForeignKey(e => e.UserId)
                .HasConstraintName("fk_orguser_integration_user_id")
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}