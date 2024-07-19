using AutoMapper;
using ScienceExperimentService.Application.Files.Queries.GetResults;
using ScienceExperimentService.Domain.Entitys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScienceExperimentService.Application.Files.Queries.GetValues.GetValuesByName
{
    public class ValueDto
    {
        public DateTime DateTime { get; set; }
        public int Time { get; set; }
        public float Indicator { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<ValueDto, Values>()
                .ForMember(fileCommand => fileCommand.DateTime,
                    opt => opt.MapFrom(fileDto => fileDto.DateTime))
                .ForMember(fileCommand => fileCommand.Time,
                    opt => opt.MapFrom(fileDto => fileDto.Time))
                .ForMember(fileCommand => fileCommand.Indicator,
                    opt => opt.MapFrom(fileDto => fileDto.Indicator));
        }
    }  
}
