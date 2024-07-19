using MediatR;
using ScienceExperimentService.Application.Interfaces;
using ScienceExperimentService.Domain.Entitys;


namespace ScienceExperimentService.Application.Files.Commands.Create.CreateResult
{
    public class CreateResultCommandHandler : IRequestHandler<CreateResultCommand, Guid>
    {
        private readonly IExperimentsDbContext _dbContext;

        public CreateResultCommandHandler(IExperimentsDbContext dbContext) =>
            _dbContext = dbContext;

        public async Task<Guid> Handle(CreateResultCommand request, CancellationToken cancellationToken)
        {
            var result = new Results
            {
                Id = Guid.NewGuid(),
                FileId = request.FileId,
                File = request.File,
                FirstExperimentStart = request.FirstExperimentStart,
                LastExperimentStart = request.LastExperimentStart,
                MaxExperimentTime = request.MaxExperimentTime,
                MinExperimentTime = request.MinExperimentTime,
                AvgExperimentTime = request.AvgExperimentTime,
                AvgIndicator = request.AvgIndicator,
                MedianIndicator = request.MedianIndicator,
                MaxIndicator = request.MaxIndicator,
                MinIndicator = request.MinIndicator,
                ExperimentCount = request.ExperimentCount
            };

            _dbContext.Results.Add(result);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return result.Id;
        }
    }
}
