using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScienceExperimentService.Application.Files.Commands.Create
{
    public class UploadExperimentCommand : IRequest<int>
    {
        public IFormFile File { get; set; }
    }
}
