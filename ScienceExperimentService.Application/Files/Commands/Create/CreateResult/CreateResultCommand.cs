using MediatR;
using DomainFile = ScienceExperimentService.Domain.Entitys.Files;

namespace ScienceExperimentService.Application.Files.Commands.Create.CreateResult
{
    public class CreateResultCommand : IRequest<Guid>
    {
        public Guid FileId { get; set; }
        public DomainFile File { get; set; }
        public DateTime FirstExperimentStart { get; set; }
        public DateTime LastExperimentStart { get; set; }
        public int MaxExperimentTime { get; set; }
        public int MinExperimentTime { get; set; }
        public double AvgExperimentTime { get; set; }
        public double AvgIndicator { get; set; }
        public double MedianIndicator { get; set; }
        public float MaxIndicator { get; set; }
        public float MinIndicator { get; set; }
        public int ExperimentCount { get; set; }
    }
}
