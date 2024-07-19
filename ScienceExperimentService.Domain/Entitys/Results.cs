using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScienceExperimentService.Domain.Entitys
{
    public class Results
    {
        public Guid Id { get; set; }
        public Guid FileId { get; set; }
        public Files File { get; set; }
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
