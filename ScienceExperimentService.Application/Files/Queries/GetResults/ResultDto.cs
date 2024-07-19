using AutoMapper;
using ScienceExperimentService.Application.Common.Mappings;
using ScienceExperimentService.Domain.Entitys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScienceExperimentService.Application.Files.Queries.GetResults
{
    public class ResultDto : IMapWith<Results>
    {
        public string FileName { get; set; } = string.Empty;
        public DateTime FirstExperimentStart { get; set; }
        public DateTime LastExperimentStart { get; set; }
        public int MaxExperimentTime { get; set; }
        public int MinExperimentTime { get; set; }
        public double AvgExperimentTime { get; set; }
        public double AvgIndicator { get; set; }
        public double MedianIndicator { get; set; }
        public float MaxIndicator { get; set; }
        public float MinIndicator { get; set; }
        public int ExperimentCount { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<ResultDto, Results>()
                .ForMember(fileCommand => fileCommand.File,
                    opt => opt.MapFrom(fileDto => fileDto.FileName))
                .ForMember(fileCommand => fileCommand.FirstExperimentStart,
                    opt => opt.MapFrom(fileDto => fileDto.FirstExperimentStart))
                .ForMember(fileCommand => fileCommand.LastExperimentStart,
                    opt => opt.MapFrom(fileDto => fileDto.LastExperimentStart))
                .ForMember(fileCommand => fileCommand.MaxExperimentTime,
                    opt => opt.MapFrom(fileDto => fileDto.MaxExperimentTime))
                .ForMember(fileCommand => fileCommand.MinExperimentTime,
                    opt => opt.MapFrom(fileDto => fileDto.MinExperimentTime))
                .ForMember(fileCommand => fileCommand.AvgExperimentTime,
                    opt => opt.MapFrom(fileDto => fileDto.AvgExperimentTime))
                .ForMember(fileCommand => fileCommand.AvgIndicator,
                    opt => opt.MapFrom(fileDto => fileDto.AvgIndicator))
                .ForMember(fileCommand => fileCommand.MedianIndicator,
                    opt => opt.MapFrom(fileDto => fileDto.MedianIndicator))
                .ForMember(fileCommand => fileCommand.MaxIndicator,
                    opt => opt.MapFrom(fileDto => fileDto.MaxIndicator))
                .ForMember(fileCommand => fileCommand.MinIndicator,
                    opt => opt.MapFrom(fileDto => fileDto.MinIndicator))
                .ForMember(fileCommand => fileCommand.ExperimentCount,
                    opt => opt.MapFrom(fileDto => fileDto.ExperimentCount));
        }
    }
}
