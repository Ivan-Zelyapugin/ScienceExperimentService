using Microsoft.EntityFrameworkCore;
using DomailFiles =  ScienceExperimentService.Domain.Entitys.Files;
using ScienceExperimentService.Domain.Entitys;

namespace ScienceExperimentService.Application.Interfaces
{
    public interface IExperimentsDbContext
    {
        DbSet<DomailFiles> Files { get; set; }
        DbSet<Values> Values { get; set; }
        DbSet<Results> Results { get; set; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
