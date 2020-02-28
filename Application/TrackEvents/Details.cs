using System;
using System.Threading;
using System.Threading.Tasks;
using Application.Errors;
using AutoMapper;
using Domain;
using MediatR;
using Persistence;

namespace Application.TrackEvents
{
    public class Details
    {
        public class Query : IRequest<TrackEventDto>
        {
            public Guid Id { get; set; }
        }

        public class Handler : IRequestHandler<Query, TrackEventDto>
        {
            private readonly DataContext _context;
            private readonly IMapper _mapper;
            public Handler(DataContext context, IMapper mapper)
            {
                _mapper = mapper;
                _context = context;
            }

            public async Task<TrackEventDto> Handle(Query request, CancellationToken cancellationToken)
            {
                var trackEvent = await _context.TrackEvents
                    .FindAsync(request.Id); 
                    
                if (trackEvent == null)
                    throw new RestException(System.Net.HttpStatusCode.NotFound, new { trackevent = "Not Found" });

                var trackEventToReturn = _mapper.Map<TrackEvent, TrackEventDto>(trackEvent);

                return trackEventToReturn;
            }
        }
    }
}