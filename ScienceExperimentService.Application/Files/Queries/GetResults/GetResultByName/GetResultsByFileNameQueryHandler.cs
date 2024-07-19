using MediatR;
using Microsoft.EntityFrameworkCore;
using ScienceExperimentService.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScienceExperimentService.Application.Files.Queries.GetResults.GetResultByName
{
    public class GetResultsByFileNameQueryHandler : IRequestHandler<GetResultsByFileNameQuery, IEnumerable<ResultDto>>
    {
        private readonly IExperimentsDbContext _dbContext;

        public GetResultsByFileNameQueryHandler(IExperimentsDbContext dbContext) =>
            _dbContext = dbContext;

        public async Task<IEnumerable<ResultDto>> Handle(GetResultsByFileNameQuery request, CancellationToken cancellationToken)
        {
            var results = await _dbContext.Results
                .Include(r => r.File)
                .Where(r => r.File.FileName == request.FileName)
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
