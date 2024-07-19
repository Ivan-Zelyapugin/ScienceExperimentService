using MediatR;

namespace ScienceExperimentService.Application.Files.Commands.Update.UpdateFile
{
    public class UpdateFileCommand : IRequest<Unit>
    {
        public Guid Id { get; set; }
        public string FileName { get; set; }
        public string FileType { get; set; }
        public long FileSize { get; set; }
        public DateTime CreatedDate { get; set; }
        public string AuthorName { get; set; }
    }
}
