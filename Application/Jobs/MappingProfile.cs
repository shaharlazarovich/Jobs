using System.Linq;
using AutoMapper;
using Domain;

namespace Application.Jobs
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Job, JobDto>();
        }
    }
}