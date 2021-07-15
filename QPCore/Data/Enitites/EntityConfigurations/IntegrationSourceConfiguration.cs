using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace QPCore.Data.Enitites.EntityConfigurations
{
    public class IntegrationSourceConfiguration : BaseEntityTypeConfiguration<IntegrationSource>
    {
        public override void CustomConfigure(EntityTypeBuilder<IntegrationSource> entity)
        {
            entity.ToTable("IntegrationSources");

            entity.HasKey(p => p.Id)
                .HasName("pk_integrationsource_id");
            
            entity.Property(e => e.Id)
                .HasColumnName("id")
                .HasIdentityOptions(startValue: 100);

            entity.Property(e => e.Name)
                .HasColumnName("name")
                .HasMaxLength(50)
                .IsRequired();

            entity.Property(e => e.Logo)
                .HasMaxLength(100)
                .HasColumnName("logo")
                .IsRequired();

            entity.Property(e => e.Readme)
                .HasMaxLength(5000)
                .HasColumnName("readme");
                
        }
    }
}