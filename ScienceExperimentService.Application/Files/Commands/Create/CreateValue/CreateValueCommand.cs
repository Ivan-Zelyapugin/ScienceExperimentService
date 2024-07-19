using MediatR;
using DomainFiles = ScienceExperimentService.Domain.Entitys.Files;

namespace ScienceExperimentService.Application.Files.Commands.Create.CreateValue
{
    public class CreateValueCommand : IRequest<Guid>
    {
        public DateTime DateTime { get; set; }
        public DomainFiles File { get; set; }
        public int Time { get; set; }
        public float Indicator { get; set; }
        public Guid FileId { get; set; }
    }
}
