using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Application.Comments;
using MediatR;
using Microsoft.AspNetCore.SignalR;

namespace API.SignalR
{
    public class ChatHub : Hub
    {
        private readonly IMediator _mediator;
        public ChatHub(IMediator mediator)
        {
            _mediator = mediator;
        }

        //contrary to regular endpoints which the client accesses via the route in the controller
        //like [HttpGet/activities] for example, in SignalR the client access the end point via
        //the method name
        public async Task SendComment(Create.Command command)
        {
            //here we will get a comment from a user, and send them to all clients
            //which are connected to that activity. 
            //we will access the Hub Context - which is not the same as the HTTP context
            //we're used to, but the syntax is the same
            string username = GetUserName();

            command.UserName = username;

            var comment = await _mediator.Send(command);

            await Clients.Group(command.ActivityId.ToString()).SendAsync("ReceiveComment", comment);
        }

        private string GetUserName()
        {
            return Context.User?.Claims?.FirstOrDefault(x => x.Type
                        == ClaimTypes.NameIdentifier)?.Value;
        }

        public async Task AddToGroup(string groupName) {
            await Groups.AddToGroupAsync(Context.ConnectionId, groupName);
            var username = GetUserName();
            await Clients.Group(groupName).SendAsync("Send", $"{username} has joined the group");
        }

        public async Task RemoveFromGroup(string groupName) {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, groupName);
            var username = GetUserName();
            await Clients.Group(groupName).SendAsync("Send", $"{username} has left the group");
        }
    }
}