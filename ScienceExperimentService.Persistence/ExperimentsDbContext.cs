using Microsoft.EntityFrameworkCore;
using ScienceExperimentService.Application.Interfaces;
using ScienceExperimentService.Domain.Entitys;
using ScienceExperimentService.Persistence.EntityTypeConfigurations;


namespace ScienceExperimentService.Persistence
{
    public class ExperimentsDbContext : DbContext, IExperimentsDbContext
    {
        public DbSet<Files> Files {  get; set; }
        public DbSet<Values> Values { get; set; }
        public DbSet<Results> Results { get; set; }
        public ExperimentsDbContext(DbContextOptions<ExperimentsDbContext> options) 
            : base(options) { }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new FilesConfiguration());
            builder.ApplyConfiguration(new ValuesConfiguration());
            builder.ApplyConfiguration(new ResultsConfiguration());
            base.OnModelCreating(builder);
        }
    }
}
