using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ScienceExperimentService.Domain.Entitys;

namespace ScienceExperimentService.Persistence.EntityTypeConfigurations
{
    public class ResultsConfiguration : IEntityTypeConfiguration<Results>
    {
        public void Configure(EntityTypeBuilder<Results> builder)
        {
            builder.HasKey(result => result.Id);
            builder.HasIndex(result => result.Id).IsUnique();
            builder.Property(result => result.FirstExperimentStart).IsRequired();
            builder.Property(result => result.LastExperimentStart).IsRequired();
            builder.Property(result => result.MaxExperimentTime).IsRequired();
            builder.Property(result => result.MinExperimentTime).IsRequired();
            builder.Property(result => result.AvgExperimentTime).IsRequired();
            builder.Property(result => result.AvgIndicator).IsRequired();
            builder.Property(result => result.MedianIndicator).IsRequired();
            builder.Property(result => result.MaxIndicator).IsRequired();
            builder.Property(result => result.MinIndicator).IsRequired();
            builder.Property(result => result.ExperimentCount).IsRequired();
            builder.HasOne(result => result.File)
                   .WithMany()
                   .HasForeignKey(result => result.FileId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
