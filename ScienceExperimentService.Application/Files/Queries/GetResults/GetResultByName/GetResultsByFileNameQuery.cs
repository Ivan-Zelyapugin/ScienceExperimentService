using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScienceExperimentService.Application.Files.Queries.GetResults.GetResultByName
{
    public class GetResultsByFileNameQuery : IRequest<IEnumerable<ResultDto>>
    {
        public string FileName { get; set; }
    }
}
