using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace RestaranApp.Controllers
{

    [Route("")]
    public class HomeController : Controller
    {
        [HttpGet]
        public string Home()
        {
            return "Hello";
        }
    }
}
