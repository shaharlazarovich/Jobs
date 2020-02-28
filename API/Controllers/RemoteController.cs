using System;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Application.Remote;

namespace API.Controllers
{
    public class RemoteController : BaseController
    {  
        [HttpPost]
        public async Task<ActionResult<Unit>> Run(Run.Command command)
        {
            return await Mediator.Send(command);
        }
    }

}