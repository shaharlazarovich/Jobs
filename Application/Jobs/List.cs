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

namespace Application.Jobs
{
    public class List
    {
        public class JobsEnvelope
        {
            //we refactored this class for paging purposes - instead of returning
            //the ActivityDto itself, we use the envelope to be able to add a count
            //of activities, and use this count to break the list into pages
            public List<JobDto> Jobs { get; set; }
            public int JobsCount { get; set; }
        }
        public class Query : IRequest<JobsEnvelope>
        {
            public Query(int? limit, int? offset, DateTime? lastRun)
            {
                Limit = limit;
                Offset = offset;
                LastRun = lastRun ?? new DateTime(1900,1,1);//we'll set 1/1/1900 as default date in case no lastRun was sent
            }
            public int? Limit { get; set; }
            public int? Offset { get; set; }
            public DateTime? LastRun { get; set; }
        }
        public class Handler : IRequestHandler<Query, JobsEnvelope>
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

            public async Task<JobsEnvelope> Handle(Query request, CancellationToken cancellationToken)
            {
                var queryable = _context.Jobs
                    .Where(x => x.LastRun >= request.LastRun)
                    .OrderBy(x => x.LastRun) //this is our way to get a sorting by date functionality
                    .AsQueryable();

                //the below two parameters effect our paging - from where do we begin to page (offset)
                //while default is 0 , but we can decide to page after X records, and how many records
                //do we take for each page (limit) - so for example, we start paging after 20 records,
                //and page every 5 from that point on
                //maximum records with no supplied limit is 6
                var jobs = await queryable
                    .Skip(request.Offset ?? 0)
                    .Take(request.Limit ?? 6).ToListAsync();

                return new JobsEnvelope
                {
                    Jobs = _mapper.Map<List<Job>, List<JobDto>>(jobs),
                    JobsCount = queryable.Count()
                };
            }
        }
    }
}



