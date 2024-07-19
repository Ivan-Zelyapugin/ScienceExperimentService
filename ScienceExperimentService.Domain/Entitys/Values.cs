using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScienceExperimentService.Domain.Entitys
{
    public class Values
    {
        public Guid Id { get; set; }
        public DateTime DateTime { get; set; }
        public int Time { get; set; }
        public float Indicator { get; set; }
        public Guid FileId { get; set; }
        public Files File { get; set; }
    }
}
