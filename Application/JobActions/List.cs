using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Interfaces;
using AutoMapper;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.JobActions
{
    public class List
    {
        public class JobActionsEnvelope
        {
            //we refactored this class for paging purposes - instead of returning
            //the JobDto itself, we use the envelope to be able to add a count
            //of Jobs, and use this count to break the list into pages
            public List<JobActionDto> JobActions { get; set; }
            public int JobActionsCount { get; set; }
        }
        public class Query : IRequest<JobActionsEnvelope>
        {
            public Query(int? limit, int? offset, DateTime? actionDate)
            {
                Limit = limit;
                Offset = offset;
                actionDate = actionDate ?? new DateTime(1900,1,1);//we'll set 1/1/1900 as default date
            }
            public int? Limit { get; set; }
            public int? Offset { get; set; }
            public DateTime? actionDate { get; set; }
        }
        public class Handler : IRequestHandler<Query, JobActionsEnvelope>
        {
            private readonly DataContext _context;
            private readonly IMapper _mapper;
            private readonly IUserAccessor _userAccessor;
            public Handler(DataContext context, IMapper mapper, IUserAccessor userAccessor)
            {
                _userAccessor = userAccessor;
                _mapper = mapper;
                _context = context;
            }

            public async Task<JobActionsEnvelope> Handle(Query request, CancellationToken cancellationToken)
            {
                var queryable = _context.JobActions
                    .OrderBy(x => x.ActionDate) //this is our way to get a sorting by date functionality
                    .AsQueryable();

                //the below two parameters effect our paging - from where do we begin to page (offset)
                //while default is 0 , but we can decide to page after X records, and how many records
                //do we take for each page (limit) - so for example, we start paging after 20 records,
                //and page every 5 from that point on
                //maximum records with no supplied limit is 6
                var jobActions = await queryable
                    .Skip(request.Offset ?? 0)
                    .Take(request.Limit ?? 6).ToListAsync();

                return new JobActionsEnvelope
                {
                    JobActions = _mapper.Map<List<JobAction>, List<JobActionDto>>(jobActions),
                    JobActionsCount = jobActions.Count()
                };
            }
        }
    }
}



