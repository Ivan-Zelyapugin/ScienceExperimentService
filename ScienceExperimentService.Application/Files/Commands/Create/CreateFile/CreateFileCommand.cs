using MediatR;
using DomainFile = ScienceExperimentService.Domain.Entitys.Files;
namespace ScienceExperimentService.Application.Files.Commands.Create.CreateFile
{
    public class CreateFileCommand : IRequest<DomainFile>
    {
        public string FileName { get; set; }
        public string FileType { get; set; }
        public long FileSize { get; set; }
        public DateTime CreatedDate { get; set; }
        public string AuthorName { get; set; }
    }
}
