using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ViewModelAnd;

namespace RoleBasedAndJWT
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestAPIController : ControllerBase
    {
        [HttpGet]
        [Route("TestIfApiWorks")]
        public Response TestIfApiWorks()
        {
            var Response = new Response { Message = "Ok", Status = "Ok Status" };
            return Response;
        }
    }
}
 