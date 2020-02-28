using System;
using System.Threading;
using System.Threading.Tasks;
using Domain;
using FluentValidation;
using MediatR;
using Persistence;
using Application.Interfaces;

namespace Application.TrackEvents
{
    public class Track
    {
        public class Command : IRequest
        {
            public Guid Id { get; set; }
            public string jobId { get; set; }
            public string jobName { get; set; }
            public string userId { get; set; }
            public string remoteIP { get; set; }
            public string remoteResponse { get; set; }
            public string requestProperties { get; set; }
            public DateTime actionDate { get; set; }
            public string source { get; set; }
            public string eventTracked { get; set; }
        }
        public class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator()
            {
                RuleFor(x => x.Id).NotEmpty();
                RuleFor(x => x.eventTracked).NotEmpty();
                RuleFor(x => x.source).NotEmpty();
                RuleFor(x => x.actionDate).NotEmpty(); 
            }   
        }
        public class Handler : IRequestHandler<Command>
        {
            private readonly DataContext _context;
            public Handler(DataContext context, IRemoteJobAccessor remote)
            {
                _context = context;
            } 
            public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
            {
                var trackEvent = new TrackEvent
                {
                    Id = request.Id,
                    JobId = request.jobId,
                    JobName = request.jobName,
                    UserId = request.userId,
                    Event = request.eventTracked,
                    Source = request.source,
                    ActionDate = request.actionDate,
                    RemoteIP = request.remoteIP,
                    RequestProperties = request.requestProperties,
                    RemoteResponse = ""
                };
                
                _context.TrackEvents.Add(trackEvent);
        
                //handler logic
                var success = await _context.SaveChangesAsync(false) > 0;
        
                if (success) return Unit.Value;

                throw new Exception("problem saving changes");        
            }  
        }
    }
}