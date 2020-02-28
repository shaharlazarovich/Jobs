using System;
using System.Threading;
using System.Threading.Tasks;
using Domain;
using FluentValidation;
using MediatR;
using Persistence;
using Application.Interfaces;
using System.Net;
using Application.Errors;

namespace Application.Remote
{
    public class Run
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
                RuleFor(x => x.jobId).NotEmpty();
                RuleFor(x => x.jobName).NotEmpty();
                RuleFor(x => x.userId).NotEmpty();
                RuleFor(x => x.eventTracked).NotEmpty();
                RuleFor(x => x.source).NotEmpty();
                RuleFor(x => x.remoteIP).NotEmpty();
                RuleFor(x => x.actionDate).NotEmpty(); 
            }   
        }
        public class Handler : IRequestHandler<Command>
        {
            private readonly DataContext _context;
            private readonly IRemoteJobAccessor _remote;
            public Handler(DataContext context, IRemoteJobAccessor remote)
            {
                _context = context;
                _remote = remote;
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
        
                if (success) {
                    var runJob = await _remote.PostRemote(request.remoteIP,request.eventTracked,request.jobName);
                    if (!runJob.Contains("successfully")) {
                        throw new Exception("Failed Running Remote Job");
                    }
                    else {
                        //return Unit.Value;
                        var response = await _context.TrackEvents.FindAsync(request.Id);
                        if (response==null)
                            throw new RestException(HttpStatusCode.NotFound, new {TrackEvent= "Not Found"});
                        else {
                            response.RemoteResponse = runJob;
                            var responseSuccess = await _context.SaveChangesAsync(false) > 0;
                            if (success) return Unit.Value;
                                throw new Exception("problem updating response");
                        }
                    }
                }
                else
                    throw new Exception("problem saving changes");
            }
        }  
    }
}