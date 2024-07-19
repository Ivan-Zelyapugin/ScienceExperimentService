using MediatR;
using ScienceExperimentService.Application.Interfaces;
using ScienceExperimentService.Domain.Entitys;


namespace ScienceExperimentService.Application.Files.Commands.Create.CreateValue
{
    public class CreateValueCommandHandler : IRequestHandler<CreateValueCommand, Guid>
    {
        private readonly IExperimentsDbContext _dbContext;
        public CreateValueCommandHandler(IExperimentsDbContext dbContext) =>
            _dbContext = dbContext;

        public async Task<Guid> Handle(CreateValueCommand request, CancellationToken cancellationToken)
        {
            var value = new Values
            {
                Id = Guid.NewGuid(),
                DateTime = request.DateTime,
                Time = request.Time,
                Indicator = request.Indicator,
                FileId = request.FileId,
                File = request.File
            };

            _dbContext.Values.Add(value);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return value.Id;
        }
    }
}
