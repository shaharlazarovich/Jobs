using System;
using System.Threading;
using System.Threading.Tasks;
using Application.Interfaces;
using Domain;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Jobs
{
    public class Create
    {
        public class Command : IRequest
        {
            public Guid Id { get; set; }
            public string JobName { get; set; }
            public string Company { get; set; }
            public string Replication { get; set; }
            public string Servers { get; set; }
            public DateTime LastRun { get; set; }
            public int RTA { get; set; }
            public string Results { get; set; }
            public string Key { get; set; }
            public int RTONeeded { get; set; }
        
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
                var job = new Job
                {
                    Id = request.Id,
                    JobName = request.JobName,
                    Company = request.Company,
                    Replication = request.Replication,
                    Servers = request.Servers,
                    LastRun = request.LastRun,
                    RTA = request.RTA,
                    Results = request.Results,
                    Key = request.Key,
                    RTONeeded = request.RTONeeded,
                };

                _context.Jobs.Add(job);

                var success = await _context.SaveChangesAsync() > 0;

                if (success) return Unit.Value;

                throw new Exception("problem saving changes");


            }
        }
    }


}