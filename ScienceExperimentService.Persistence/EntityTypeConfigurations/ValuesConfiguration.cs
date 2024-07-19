using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ScienceExperimentService.Domain.Entitys;

namespace ScienceExperimentService.Persistence.EntityTypeConfigurations
{
    public class ValuesConfiguration : IEntityTypeConfiguration<Values>
    {
        public void Configure(EntityTypeBuilder<Values> builder)
        {
            builder.HasKey(value => value.Id);
            builder.HasIndex(value => value.Id).IsUnique();
            builder.Property(value => value.DateTime).IsRequired();
            builder.Property(value => value.Time).IsRequired();
            builder.Property(value => value.Indicator).IsRequired();
            builder.HasOne(value => value.File)
                .WithMany()
                .HasForeignKey(value => value.FileId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
