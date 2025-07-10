using datingapp.data.Data;
using datingapp.data.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace datingapp.api.Controllers
{
    public class BuggyController : BaseApiController
    {

        private readonly DataContext _context;

        public BuggyController(DataContext context)
        {
            _context = context;
        }

        [Authorize]
        [HttpGet]
        [Route("auth")]
        public ActionResult<string> GetSecret()
        {
            return "secret text";
        }

        [HttpGet]
        [Route("not-found")]
        public ActionResult<AppUser> GetNotFound()
        {
            var thing = _context.Users.Find(-1);

            if (thing == null) return NotFound();

            return thing;
        }

        [HttpGet]
        [Route("server-error")]
        public ActionResult<string> GetServerError()
        {
            var thing = _context.Users.Find(-1);
            var thingToReturn = thing.ToString(); // it will generate error exception

            return thingToReturn;
        }

        [HttpGet]
        [Route("bad-request")]
        public IActionResult GetBadRequest()
        {
            return BadRequest("This was not good request");
        }
    }
}