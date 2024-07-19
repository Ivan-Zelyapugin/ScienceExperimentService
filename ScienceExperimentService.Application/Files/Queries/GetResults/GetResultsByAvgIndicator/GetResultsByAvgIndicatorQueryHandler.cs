using MediatR;
using Microsoft.EntityFrameworkCore;
using ScienceExperimentService.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScienceExperimentService.Application.Files.Queries.GetResults.GetResultsByAvgIndicator
{
    public class GetResultsByAvgIndicatorQueryHandler : IRequestHandler<GetResultsByAvgIndicatorQuery, IEnumerable<ResultDto>>
    {
        private readonly IExperimentsDbContext _dbContext;

        public GetResultsByAvgIndicatorQueryHandler(IExperimentsDbContext dbContext) =>
            _dbContext = dbContext;

        public async Task<IEnumerable<ResultDto>> Handle(GetResultsByAvgIndicatorQuery request, CancellationToken cancellationToken)
        {
            var query = _dbContext.Results.AsQueryable();

            if (request.MinAvgIndicator.HasValue)
            {
                query = query.Where(r => r.AvgIndicator >= request.MinAvgIndicator.Value);
            }

            if (request.MaxAvgIndicator.HasValue)
            {
                query = query.Where(r => r.AvgIndicator <= request.MaxAvgIndicator.Value);
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
