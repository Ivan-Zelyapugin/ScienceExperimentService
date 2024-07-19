using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScienceExperimentService.Application.Files.Commands.Update.UpdateValue
{
    public class UpdateValueCommand : IRequest<Unit>
    {
        public Guid Id { get; set; }
        public DateTime DateTime { get; set; }
        public int Time { get; set; }
        public float Indicator { get; set; }
    }
}
