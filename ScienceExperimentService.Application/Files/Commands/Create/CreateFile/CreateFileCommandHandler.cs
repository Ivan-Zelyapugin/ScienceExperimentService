using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using ScienceExperimentService.Application.Interfaces;
using ScienceExperimentService.Domain.Entitys;
using System.Globalization;
using DomainFile = ScienceExperimentService.Domain.Entitys.Files;

namespace ScienceExperimentService.Application.Files.Commands.Create.CreateFile
{
    public class CreateFileCommandHandler : IRequestHandler<CreateFileCommand, DomainFile>
    {
        private readonly IExperimentsDbContext _dbContext;

        public CreateFileCommandHandler(IExperimentsDbContext dbContext) =>
            _dbContext = dbContext;

        public async Task<DomainFile> Handle(CreateFileCommand request, CancellationToken cancellationToken)
        {
            var file = new DomainFile
            {
                Id = Guid.NewGuid(),
                FileName = request.FileName,
                FileType = request.FileType,
                FileSize = request.FileSize,
                CreatedDate = request.CreatedDate,
                AuthorName = request.AuthorName
            };

            _dbContext.Files.Add(file);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return file;
        }

    }
}
