using System.Threading.Tasks;
using Application.Photos;
using Domain;
using Microsoft.AspNetCore.Mvc;
using MediatR;

namespace API.Controllers
{
    public class PhotosController: BaseController
    {
        [HttpPost]
        public async Task<ActionResult<Photo>> Add([FromForm]Add.Command command) { //inside the square brackets we add a "hint" where to look the data in the commanid
                                            //in case it is not recognized as a photo
            return await Mediator.Send(command);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Unit>> Delete(string id) {
            return await Mediator.Send(new Delete.Command{ Id = id});
        }

        [HttpPost("{id}/setmain")]
        public async Task<ActionResult<Unit>> SetMain(string id) {
            return await Mediator.Send(new SetMain.Command{Id = id});
        }
    }
}