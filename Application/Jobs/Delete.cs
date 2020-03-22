using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Application.Errors;
using MediatR;
using Persistence;

namespace Application.Jobs
{
    public class Delete
    {
        public class Command : IRequest
        {
            public long Id { get; set; }
        }

        public class Handler : IRequestHandler<Command>
        {
            private readonly DataContext _context;
            public Handler(DataContext context)
            {
                _context = context;

            }

            public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
            {

                var job = await _context.Jobs.FindAsync(request.Id);

                if (job==null)
                    throw new RestException(HttpStatusCode.NotFound, new {job= "Not Found"});

                job.jobStatus = Domain.Enums.JobStatus.Deleted;
                _context.Update(job);

                var success = await _context.SaveChangesAsync() > 0;

                if (success) return Unit.Value;

                throw new Exception("problem saving changes");


            }
        }

    }
}