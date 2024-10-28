using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;
using Practice.Web.Configuration;
using Practice.Common.ViewModels;
using Practice.Common;

namespace Practice.Web.Controllers
{
    [Route("api/user")]
    [ApiExplorerSettings(GroupName = SwaggerConst.FrontendDocs)]
    [Produces(MediaTypeNames.Application.Json)]
    [ApiController]
    public class UserController : BaseApiController
    {
        public UserController(ILogger<UserController> logger) : base(logger)
        {
        }

        [HttpGet]
        public async Task<ActionResult<ResponseModel>> Get()
        {
            return Success();
        }
    }
}
