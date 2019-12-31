using System.Linq;
using AutoMapper;
using Domain;

namespace Application.Activities
{
    public class MappingProfile : Profile
    {
        //here in the mapping profile class we have a challenge because we don't
        //have access to the GetCurrentUser, nor do we want to inject the userAccessor
        //so how can we figure out , weather the user attending the event is also following us?
        //for this we have a "custom value resolver" which implements the IValueResolver interface
        //from AutoMapper - which will basically, map the UserActivity to AttendeeDto and return a bool
        //indicating if the user is following
        public MappingProfile()
        {
            CreateMap<Activity, ActivityDto>();
            CreateMap<UserActivity, AttendeeDto>()
                .ForMember(d => d.UserName, o => o.MapFrom(s => s.AppUser.UserName))
                .ForMember(d => d.DisplayName, o => o.MapFrom(s => s.AppUser.DisplayName))
                .ForMember(d => d.Image, o => o.MapFrom(s => s.AppUser.Photos.FirstOrDefault(
                    x => x.IsMain).Url
                ))
                .ForMember(d => d.Following, o => o.MapFrom<FollowingResolver>());
        }
    }
}