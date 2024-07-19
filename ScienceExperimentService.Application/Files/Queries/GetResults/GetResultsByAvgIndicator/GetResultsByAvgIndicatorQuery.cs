using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScienceExperimentService.Application.Files.Queries.GetResults.GetResultsByAvgIndicator
{
    public class GetResultsByAvgIndicatorQuery : IRequest<IEnumerable<ResultDto>>
    {
        public double? MinAvgIndicator { get; set; }
        public double? MaxAvgIndicator { get; set; }
    }
}
