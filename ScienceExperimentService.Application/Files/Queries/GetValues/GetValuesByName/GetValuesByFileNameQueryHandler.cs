using MediatR;
using Microsoft.EntityFrameworkCore;
using ScienceExperimentService.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScienceExperimentService.Application.Files.Queries.GetValues.GetValuesByName
{
    public class GetValuesByFileNameQueryHandler : IRequestHandler<GetValuesByFileNameQuery, IEnumerable<ValueDto>>
    {
        private readonly IExperimentsDbContext _dbContext;

        public GetValuesByFileNameQueryHandler(IExperimentsDbContext dbContext) =>
            _dbContext = dbContext;

        public async Task<IEnumerable<ValueDto>> Handle(GetValuesByFileNameQuery request, CancellationToken cancellationToken)
        {
            var values = await _dbContext.Values
                .Include(v => v.File)
                .Where(v => v.File.FileName == request.FileName)
                .Select(v => new ValueDto
                {
                    DateTime = v.DateTime,
                    Time = v.Time,
                    Indicator = v.Indicator,
                })
                .ToListAsync(cancellationToken);

            return values;
        }
    }
}
