using System;
using System.Threading;
using System.Threading.Tasks;
using Application.Errors;
using AutoMapper;
using Domain;
using MediatR;
using Persistence;

namespace Application.JobActions
{
    public class Details
    {
        public class Query : IRequest<JobActionDto>
        {
            public Guid Id { get; set; }
        }

        public class Handler : IRequestHandler<Query, JobActionDto>
        {
            private readonly DataContext _context;
            private readonly IMapper _mapper;
            public Handler(DataContext context, IMapper mapper)
            {
                _mapper = mapper;
                _context = context;
            }

            public async Task<JobActionDto> Handle(Query request, CancellationToken cancellationToken)
            {
                var jobAction = await _context.JobActions
                    .FindAsync(request.Id); 
                    
                if (jobAction == null)
                    throw new RestException(System.Net.HttpStatusCode.NotFound, new { job = "Not Found" });

                var jobActionToReturn = _mapper.Map<JobAction, JobActionDto>(jobAction);

                return jobActionToReturn;
            }
        }
    }
}