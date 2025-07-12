using Microsoft.AspNetCore.Mvc;

namespace ContactList.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ContactController : Controller
    {
        [HttpGet(Name = "GetContact")]
        public IActionResult Index()
        {
            return Ok("erro");
        }


    }
}
