using Atmira.NasaApi.Svc.Data.DTOs;
using AutoMapper;
using System;
using System.Linq;

namespace Atmira.NasaApi.Svc.Configuration
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<NearEarthObject, Asteroid>()
                .ForMember(dest => dest.Nombre, opt => opt.MapFrom(sour => sour.name))
                .ForMember(dest => dest.Diametro, opt => opt.MapFrom(sour => (sour.estimated_diameter.kilometers.estimated_diameter_min + sour.estimated_diameter.kilometers.estimated_diameter_max) / 2))
                .ForMember(dest => dest.Velocidad, opt => opt.MapFrom(sour => sour.close_approach_data.FirstOrDefault().relative_velocity.kilometers_per_hour))
                .ForMember(dest => dest.Fecha, opt => opt.MapFrom(sour => DateTimeOffset.FromUnixTimeMilliseconds(sour.close_approach_data.FirstOrDefault().epoch_date_close_approach).DateTime))
                .ForMember(dest => dest.Planeta, opt => opt.MapFrom(sour => sour.close_approach_data.FirstOrDefault().orbiting_body));
        }
    }
}
