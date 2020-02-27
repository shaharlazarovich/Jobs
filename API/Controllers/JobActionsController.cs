using System;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Application.JobActions;

namespace API.Controllers
{
    public class JobActionsController : BaseController
    {  
        [HttpPost]
        public async Task<ActionResult<Unit>> Run(Run.Command command)
        {
            return await Mediator.Send(command);
        }
        
        [HttpGet]
        public async Task<ActionResult<List.JobActionsEnvelope>> List(
            int? limit,int? offset, DateTime? actionDate)
        {
            return await Mediator.Send(new List.Query(limit,offset,actionDate));
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult<JobActionDto>> Details(Guid id){
            return await Mediator.Send(new Details.Query{Id = id});
        } 

    }

}