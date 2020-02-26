using AutoMapper;
using Domain;

namespace Application.JobActions
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<JobAction, JobActionDto>();
        }
    }
}