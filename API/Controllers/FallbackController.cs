using System.IO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    //we will inherit controller and not our base controller since this controller has view support
    //and here we would need to return a view - since this controller handles the react routes
    //and it will return our index.html physical file
    [AllowAnonymous]
    public class FallbackController: Controller 
    {
        public IActionResult Index()
        {
            return PhysicalFile(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "index.html"), "text/HTML");
        }
        
    }
}