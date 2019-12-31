using System;
using System.Threading;
using System.Threading.Tasks;
using Application.Errors;
using AutoMapper;
using Domain;
using MediatR;
using Persistence;

namespace Application.Activities
{
    public class Details
    {
        public class Query : IRequest<ActivityDto>
        {
            public Guid Id { get; set; }
        }

        public class Handler : IRequestHandler<Query, ActivityDto>
        {
            private readonly DataContext _context;
            private readonly IMapper _mapper;
            public Handler(DataContext context, IMapper mapper)
            {
                _mapper = mapper;
                _context = context;
            }

            public async Task<ActivityDto> Handle(Query request, CancellationToken cancellationToken)
            {
                //throw new Exception("Computer says no!");//to test for 500 internal server error
                var activity = await _context.Activities
                    .FindAsync(request.Id); //since we're switching from eager loading to lazy loading we'll comment out the below
                    //.Include(x => x.UserActivities) //this is how we do eager loading, when the data is included with the query
                    //.ThenInclude(x => x.AppUser)
                    //.SingleOrDefaultAsync(x => x.Id == request.Id);


                if (activity == null)
                    throw new RestException(System.Net.HttpStatusCode.NotFound, new { activity = "Not Found" });

                var activityToReturn = _mapper.Map<Activity, ActivityDto>(activity);

                return activityToReturn;
            }
        }
    }
}