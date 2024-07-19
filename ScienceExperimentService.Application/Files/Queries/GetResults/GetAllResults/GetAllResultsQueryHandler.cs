using MediatR;
using Microsoft.EntityFrameworkCore;
using ScienceExperimentService.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScienceExperimentService.Application.Files.Queries.GetResults.GetAllResults
{
    public class GetAllResultsQueryHandler : IRequestHandler<GetAllResultsQuery, IEnumerable<ResultDto>>
    {
        private readonly IExperimentsDbContext _dbContext;

        public GetAllResultsQueryHandler(IExperimentsDbContext dbContext) =>
            _dbContext = dbContext;

        public async Task<IEnumerable<ResultDto>> Handle(GetAllResultsQuery request, CancellationToken cancellationToken)
        {
            var results = await _dbContext.Results
                .Include(r => r.File)
                .Select(r => new ResultDto
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
