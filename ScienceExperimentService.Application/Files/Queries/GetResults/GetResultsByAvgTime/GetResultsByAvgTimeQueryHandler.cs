using MediatR;
using Microsoft.EntityFrameworkCore;
using ScienceExperimentService.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScienceExperimentService.Application.Files.Queries.GetResults.GetResultsByAvgTime
{
    public class GetResultsByAvgTimeQueryHandler : IRequestHandler<GetResultsByAvgTimeQuery, IEnumerable<ResultDto>>
    {
        private readonly IExperimentsDbContext _dbContext;

        public GetResultsByAvgTimeQueryHandler(IExperimentsDbContext dbContext) =>
            _dbContext = dbContext;

        public async Task<IEnumerable<ResultDto>> Handle(GetResultsByAvgTimeQuery request, CancellationToken cancellationToken)
        {
            var query = _dbContext.Results
                .Include(r => r.File)
                .AsQueryable();

            if (request.MinAvgTime.HasValue)
            {
                query = query.Where(r => r.AvgExperimentTime >= request.MinAvgTime.Value);
            }

            if (request.MaxAvgTime.HasValue)
            {
                query = query.Where(r => r.AvgExperimentTime <= request.MaxAvgTime.Value);
            }

            var results = await query.Select(r => new ResultDto
            {
                FileName = r.File.FileName,
                FirstExperimentStart = r.FirstExperimentStart,
                LastExperimentStart = r.LastExperimentStart,
                MaxExperimentTime = r.MaxExperimentTime,
                MinExperimentTime = r.MinExperimentTime,
                AvgExperimentTime = r.AvgExperimentTime,
                AvgIndicator = r.AvgIndicator,
                MedianIndicator = r.MedianIndicator,
                MaxIndicator = r.MaxIndicator,
                MinIndicator = r.MinIndicator,
                ExperimentCount = r.ExperimentCount
            })
            .ToListAsync(cancellationToken);

            return results;
        }
    }
}
