using AutoMapper;
using Domain;

namespace Application.TrackEvents
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<TrackEvent, TrackEventDto>();
        }
    }
}