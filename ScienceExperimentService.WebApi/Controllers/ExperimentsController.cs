using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using ScienceExperimentService.Application.Files.Commands.Create;
using ScienceExperimentService.Application.Files.Queries.GetResults;
using ScienceExperimentService.Application.Files.Queries.GetResults.GetAllResults;
using ScienceExperimentService.Application.Files.Queries.GetResults.GetResultByName;
using ScienceExperimentService.Application.Files.Queries.GetResults.GetResultsByAvgIndicator;
using ScienceExperimentService.Application.Files.Queries.GetResults.GetResultsByAvgTime;
using ScienceExperimentService.Application.Files.Queries.GetValues.GetValuesByName;

namespace ScienceExperimentService.WebApi.Controllers
{
    [Route("science/[controller]")]
    public class ExperimentsController : BaseController
    {
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public ExperimentsController(IMapper mapper, IMediator mediator)
        {
            _mapper = mapper;
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<ActionResult<Guid>> Create( IFormFile file)
        {
            if (Path.GetExtension(file.FileName).ToLower() != ".csv")
            {
                return BadRequest(new { Message = "Only .csv files are allowed." });
            }

            UploadExperimentCommand command = new UploadExperimentCommand
            {
                File = file
            };
            var fileId = await Mediator.Send(command);
            return Ok(fileId);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ResultDto>>> GetResults([FromQuery] string? fileName, [FromQuery] double? avgIndicatorMin, [FromQuery] double? avgIndicatorMax, [FromQuery] double? avgTimeMin, [FromQuery] double? avgTimeMax)
        {
            if (!string.IsNullOrEmpty(fileName))
            {
                var query = new GetResultsByFileNameQuery { FileName = fileName };
                var results = await _mediator.Send(query);
                return Ok(results);
            }
            else if (avgIndicatorMin.HasValue || avgIndicatorMax.HasValue)
            {
                var query = new GetResultsByAvgIndicatorQuery
                {
                    MinAvgIndicator = avgIndicatorMin,
                    MaxAvgIndicator = avgIndicatorMax
                };
                var results = await _mediator.Send(query);
                return Ok(results);
            }
            else if (avgTimeMin.HasValue || avgTimeMax.HasValue)
            {
                var query = new GetResultsByAvgTimeQuery
                {
                    MinAvgTime = avgTimeMin,
                    MaxAvgTime = avgTimeMax
                };
                var results = await _mediator.Send(query);
                return Ok(results);
            }
            var allResults = await Mediator.Send(new GetAllResultsQuery());
            return Ok(allResults);
        }

        [HttpGet("{fileName}")]
        public async Task<ActionResult<IEnumerable<ValueDto>>> GetValuesByFileName(string fileName)
        {
            if (string.IsNullOrEmpty(fileName))
            {
                return BadRequest(new { Message = "File name is required." });
            }

            var query = new GetValuesByFileNameQuery { FileName = fileName };
            var values = await _mediator.Send(query);

            return Ok(values);
        }
    }
}
