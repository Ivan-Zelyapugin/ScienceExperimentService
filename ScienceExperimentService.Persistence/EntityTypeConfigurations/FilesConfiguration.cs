using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ScienceExperimentService.Domain.Entitys;

namespace ScienceExperimentService.Persistence.EntityTypeConfigurations
{
    public class FilesConfiguration : IEntityTypeConfiguration<Files>
    {
        public void Configure (EntityTypeBuilder<Files> builder)
        {
            builder.HasKey(file => file.Id);
            builder.HasIndex(file => file.Id).IsUnique();
            builder.Property(file => file.FileName).IsRequired().HasMaxLength(255);
            builder.Property(file => file.CreatedDate).IsRequired();
            builder.Property(file => file.AuthorName).IsRequired().HasMaxLength(255);
        }
    }
}
