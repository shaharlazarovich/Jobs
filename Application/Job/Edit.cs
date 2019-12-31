using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Application.Errors;
using FluentValidation;
using MediatR;
using Persistence;

namespace Application.Jobs
{
    public class Edit
    {
        public class Command : IRequest
        {
            public Guid Id { get; set; }
            public string JobName { get; set; }
            public string Company { get; set; }
            public string Replication { get; set; }
            public string Servers { get; set; }
            public DateTime? LastRun { get; set; }
            public int? RTA { get; set; }
            public string Results { get; set; }
            public string Key { get; set; }
            public int? RTONeeded { get; set; }
        }
    
        public class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator()
            {
                RuleFor(x => x.Id).NotEmpty();
                RuleFor(x => x.RTONeeded).NotEmpty();
                RuleFor(x => x.Company).NotEmpty();
                RuleFor(x => x.Replication).NotEmpty();
                RuleFor(x => x.Servers).NotEmpty();
                RuleFor(x => x.LastRun).NotEmpty();
                RuleFor(x => x.RTA).NotEmpty();
                RuleFor(x => x.Results).NotEmpty();
                RuleFor(x => x.Key).NotEmpty();
                RuleFor(x => x.JobName).NotEmpty();
                
            }
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
                
                job.JobName = request.JobName ?? job.JobName;
                job.Company = request.Company ?? job.Company;
                job.Replication = request.Replication ?? job.Replication;
                job.Servers = request.Servers ?? job.Servers;
                job.LastRun = request.LastRun ?? job.LastRun;
                job.RTA = request.RTA ?? job.RTA;
                job.Results = request.Results ?? job.Results;
                job.Key = request.Key ?? job.Key;
                job.RTONeeded = request.RTONeeded ?? job.RTONeeded;

                var success = await _context.SaveChangesAsync() > 0;
                if (success) return Unit.Value;
                throw new Exception("problem saving changes");
            }
        }    
    }
}