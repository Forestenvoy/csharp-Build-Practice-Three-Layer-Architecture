using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;
using Practice.Web.Configuration;
using Practice.Common;
using Practice.Common.ViewModels;
using Practice.Service.Implements;
using Practice.Service.Dtos.Response.Product;
using Practice.Service.Dtos.Request.Product;

namespace Practice.Web.Controllers
{
    [Route("api/product")]
    [ApiExplorerSettings(GroupName = SwaggerConst.BackendDocs)]
    [Produces(MediaTypeNames.Application.Json)]
    [ApiController]
    public class ProductController : BaseApiController
    {
        private readonly ProductService _productService;

        public ProductController(
            ILogger<ProductController> logger, 
            ProductService productService) 
            : base(logger)
        {
            _productService = productService;
        }


        //[HttpGet("getall")]
        //public async Task<ActionResult<ResponsePagingDataModel<ProductViewModel>>> GetPageList([FromQuery] SearchViewModel request)
        //{
        //}


        // 新增產品

        // 編輯產品

        // 刪除產品

        [HttpGet]
        public async Task<ActionResult<ResponseModel>> Get()
        {
            return Success();
        }
    }
}
