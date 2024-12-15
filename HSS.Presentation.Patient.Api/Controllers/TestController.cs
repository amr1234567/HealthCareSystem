using HSS.DataAccess.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HSS.Presentation.Patient.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController(Helper helper) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> Test()
        {
            await helper.Seed();
            return Ok();
        }
    }
}
