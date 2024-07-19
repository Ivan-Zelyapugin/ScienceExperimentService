using MediatR;
using ScienceExperimentService.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScienceExperimentService.Application.Files.Commands.Update.UpdateResult
{
    public class UpdateResultCommandHandler : IRequestHandler<UpdateResultCommand, Unit>
    {
        private readonly IExperimentsDbContext _dbContext;

        public UpdateResultCommandHandler(IExperimentsDbContext dbContext) =>
            _dbContext = dbContext;

        public async Task<Unit> Handle(UpdateResultCommand request, CancellationToken cancellationToken)
        {
            var result = await _dbContext.Results.FindAsync(new object[] { request.Id }, cancellationToken);

            if (result == null)
            {
                throw new KeyNotFoundException("Result not found");
            }

            result.FirstExperimentStart = request.FirstExperimentStart;
            result.LastExperimentStart = request.LastExperimentStart;
            result.MaxExperimentTime = request.MaxExperimentTime;
            result.MinExperimentTime = request.MinExperimentTime;
            result.AvgExperimentTime = request.AvgExperimentTime;
            result.AvgIndicator = request.AvgIndicator;
            result.MedianIndicator = request.MedianIndicator;
            result.MaxIndicator = request.MaxIndicator;
            result.MinIndicator = request.MinIndicator;
            result.ExperimentCount = request.ExperimentCount;

            _dbContext.Results.Update(result);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
