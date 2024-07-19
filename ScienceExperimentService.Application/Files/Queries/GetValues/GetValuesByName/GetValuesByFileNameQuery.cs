using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScienceExperimentService.Application.Files.Queries.GetValues.GetValuesByName
{
    public class GetValuesByFileNameQuery : IRequest<IEnumerable<ValueDto>>
    {
        public string FileName { get; set; }
    }
}
