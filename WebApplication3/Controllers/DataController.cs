using Microsoft.AspNetCore.Mvc;
using WebApplication3.Models;
using WebApplication3.Services;

namespace WebApplication3.Controllers
{
    [Route("api/")]
    [ApiController]
    public class DataController : ControllerBase
    {
        private readonly ICustomDataServices _services;

        public DataController(ICustomDataServices services)
        {
            _services = services;
        }

        [HttpGet]
//        [Route("getData")]
        public ActionResult<ModelObject> GetData()
        {
            var modelObjects = _services.IterateDb();

            if(modelObjects == null)
            {
                return NotFound();
            }

            return Ok(modelObjects);
        }
    }
}