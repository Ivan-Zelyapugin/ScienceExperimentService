using MediatR;
using ScienceExperimentService.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScienceExperimentService.Application.Files.Commands.Update.UpdateValue
{
    public class UpdateValueCommandHandler : IRequestHandler<UpdateValueCommand, Unit>
    {
        private readonly IExperimentsDbContext _dbContext;

        public UpdateValueCommandHandler(IExperimentsDbContext dbContext) =>
            _dbContext = dbContext;

        public async Task<Unit> Handle(UpdateValueCommand request, CancellationToken cancellationToken)
        {
            var value = await _dbContext.Values.FindAsync(new object[] { request.Id }, cancellationToken);

            if (value == null)
            {
                throw new KeyNotFoundException("Value not found");
            }

            value.DateTime = request.DateTime;
            value.Time = request.Time;
            value.Indicator = request.Indicator;

            _dbContext.Values.Update(value);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
