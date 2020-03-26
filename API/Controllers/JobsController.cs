using System;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Application.Jobs;

namespace API.Controllers
{
    public class JobsController : BaseController
    {
        [HttpGet]
        public async Task<ActionResult<List.JobsEnvelope>> List(
            int? limit,int? offset, DateTime? lastRun)
        {
            return await Mediator.Send(new List.Query(limit,offset,lastRun));
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult<JobDto>> Details(long id){
            return await Mediator.Send(new Details.Query{Id = id});
        }

        [HttpPost]
        public async Task<ActionResult<Unit>> Create(Create.Command command)
        {
            return await Mediator.Send(command);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Unit>> Edit(long id, Edit.Command command)
        {
            command.Id = id;
            return await Mediator.Send(command);
        }

        [HttpPut("/run/{id}")]
        public async Task<ActionResult<Unit>> Run(long id, Edit.Command command)
        {
            command.Id = id;
            return await Mediator.Send(command);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Unit>> Delete(long id)
        {
            return await Mediator.Send(new Delete.Command{Id = id});    
        }

    }
}