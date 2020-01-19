using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Application.Errors;
using MediatR;
using System.Net.Http;
using System.Net.Http.Headers;
using Persistence;

namespace Application.Jobs
{
    public class Run
    {
        public class Command : IRequest
        {
            public Guid Id { get; set; }
        }

        public class Handler : IRequestHandler<Command>
        {
            private readonly DataContext _context;

            private readonly HttpClient client = new HttpClient();

            public Handler(DataContext context)
            {
                _context = context;
                client = new HttpClient();

            }

            public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
            {

                var job = await _context.Jobs.FindAsync(request.Id);

                if (job==null)
                    throw new RestException(HttpStatusCode.NotFound, new {job= "Not Found"});
                
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Add("User-Agent", ".NET Foundation Repository Reporter");

                var stringTask = client.GetStringAsync("https://api.ensuredr.com/runjob");

                var success = await stringTask;
                
                if (success=="ok") return Unit.Value;

                throw new Exception("problem running job");


            }
        }

    }
}