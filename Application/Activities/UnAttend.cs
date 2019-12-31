using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Application.Errors;
using Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Activities
{
    public class UnAttend
    {
        public class Command : IRequest
        {
            public Guid Id { get; set; }
        }

        public class Handler : IRequestHandler<Command>
        {
        private readonly DataContext _context;
        private readonly IUserAccessor _userAccessor;
        public Handler(DataContext context, IUserAccessor userAccessor)
        {
            _userAccessor = userAccessor;
            _context = context;

        }

        public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
        {

            var activity = await _context.Activities.FindAsync(request.Id);

            if (activity == null) {
                throw new RestException(HttpStatusCode.NotFound, 
                  new { Activity = "Could not find activity" });
            }

            var user = await _context.Users.SingleOrDefaultAsync(x => x.UserName == //we can't use the simpler FindAsync method since our username is not the
                                // the primary key of our Users table - and FindAsync requires that 
            _userAccessor.GetCurrentUsername());

            if (user == null) {
                throw new RestException(HttpStatusCode.NotFound, 
                    new { Activity = "Could not find user" });
            }

            var attendance = await _context.UserActivities.SingleOrDefaultAsync(x =>
                x.ActivityId == activity.Id && x.AppUserId == user.Id);

            if (attendance == null)
                return Unit.Value; //basically we just want to return if the user was already removed from the acitivty

            if (attendance.IsHost) {
                throw new RestException(HttpStatusCode.BadRequest, 
                    new { Attendance = "You can't remove yourself as host" });
            }

            _context.UserActivities.Remove(attendance);
            
            var success = await _context.SaveChangesAsync() > 0;

            if (success) return Unit.Value;
                throw new Exception("problem saving changes");


            }
        }

    }
}