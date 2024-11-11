using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;
using Practice.Web.Configuration;
using Practice.Common;
using Practice.Common.ViewModels;

namespace Practice.Web.Controllers
{
    [Route("api/admin")]
    [ApiExplorerSettings(GroupName = SwaggerConst.BackendDocs)]
    [Produces(MediaTypeNames.Application.Json)]
    [ApiController]
    public class AdminController : BaseApiController
    {
        public AdminController(ILogger<AdminController> logger) : base(logger)
        {
        }

        [HttpPost("login")]
        public async Task<ActionResult<ResponseModel>> Login()
        {
            return Success("Login success");
        }
    }
}
