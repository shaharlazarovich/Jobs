using System;
using System.Threading;
using System.Threading.Tasks;
using Domain;
using FluentValidation;
using MediatR;
using Persistence;
using static Application.Jobs.Enums;

namespace Application.Jobs
{
    public class Create
    {
        public class Command : IRequest
        {
            public long Id { get; set; }
            public string JobName { get; set; }
            public string Company { get; set; }
            public string Replication { get; set; }
            public string Servers { get; set; }
            public DateTime LastRun { get; set; }
            public string RTA { get; set; }
            public string Results { get; set; }
            public string Key { get; set; }
            public string RTONeeded { get; set; }
            public string JobIP { get; set; }
            public JobStatus JobStatus { get; set; }
        
        }

        public class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator()
            {
                RuleFor(x => x.RTONeeded).NotEmpty();
                RuleFor(x => x.Company).NotEmpty();
                RuleFor(x => x.Replication).NotEmpty();
                RuleFor(x => x.Servers).NotEmpty();
                RuleFor(x => x.LastRun).NotEmpty();
                RuleFor(x => x.RTA).NotEmpty();
                RuleFor(x => x.Results).NotEmpty();
                RuleFor(x => x.Key).NotEmpty();
                RuleFor(x => x.JobName).NotEmpty();
                RuleFor(x => x.JobIP).NotEmpty();

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

                var job = new Job
                {
                    JobName = request.JobName,
                    Company = request.Company,
                    Replication = request.Replication,
                    Servers = request.Servers,
                    LastRun = request.LastRun,
                    RTA = request.RTA,
                    Results = request.Results,
                    Key = request.Key,
                    RTONeeded = request.RTONeeded,
                    JobIP = request.JobIP,
                    jobStatus = Domain.Enums.JobStatus.Active
                };
                
                _context.Jobs.Add(job);

                var success = await _context.SaveChangesAsync() > 0;
                //throw new Exception($"problem saving changes: id: {request.Id} jobname {request.JobName} company {request.Company} rep {request.Replication} servers {request.Servers} lastrun {request.LastRun} rta {request.RTA} results {request.Results} key{request.Key} rto {request.RTONeeded}");
                
                if (success) return Unit.Value;

                throw new Exception("problem saving changes");


            }
        }
    }


}