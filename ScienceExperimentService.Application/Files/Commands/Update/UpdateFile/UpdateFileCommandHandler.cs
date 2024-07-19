using MediatR;
using ScienceExperimentService.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScienceExperimentService.Application.Files.Commands.Update.UpdateFile
{
    public class UpdateFileCommandHandler : IRequestHandler<UpdateFileCommand, Unit>
    {
        private readonly IExperimentsDbContext _dbContext;

        public UpdateFileCommandHandler(IExperimentsDbContext dbContext) =>
            _dbContext = dbContext;

        public async Task<Unit> Handle(UpdateFileCommand request, CancellationToken cancellationToken)
        {
            var file = await _dbContext.Files.FindAsync(new object[] { request.Id }, cancellationToken);

            if (file == null)
            {
                throw new KeyNotFoundException("File not found");
            }

            file.FileName = request.FileName;
            file.FileType = request.FileType;
            file.FileSize = request.FileSize;
            file.CreatedDate = request.CreatedDate;
            file.AuthorName = request.AuthorName;

            _dbContext.Files.Update(file);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
