using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScienceExperimentService.Application.Files.Queries.GetResults.GetAllResults
{
    public class GetAllResultsQuery : IRequest<IEnumerable<ResultDto>>
    {
    }
}
