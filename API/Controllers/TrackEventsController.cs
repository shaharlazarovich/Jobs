using System;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Application.TrackEvents;

namespace API.Controllers
{
    public class TrackEventsController : BaseController
    {  
        [HttpPost]
        public async Task<ActionResult<Unit>> Track(Track.Command command)
        {
            return await Mediator.Send(command);
        }
        
        [HttpGet]
        public async Task<ActionResult<List.TrackEventsEnvelope>> List(
            int? limit,int? offset, DateTime? actionDate)
        {
            return await Mediator.Send(new List.Query(limit,offset,actionDate));
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult<TrackEventDto>> Details(Guid id){
            return await Mediator.Send(new Details.Query{Id = id});
        } 

    }

}