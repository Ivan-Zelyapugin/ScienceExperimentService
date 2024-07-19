using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScienceExperimentService.Application.Files.Queries.GetResults.GetResultsByAvgTime
{
    public class GetResultsByAvgTimeQuery : IRequest<IEnumerable<ResultDto>>
    {
        public double? MinAvgTime { get; set; }
        public double? MaxAvgTime { get; set; }
    }
}
