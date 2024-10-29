using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Practice.Common.ViewModels;

namespace Practice.Common
{
    [Produces(ConstKey.JsonContentTypeWithCharset)]
    [ApiController]
    public abstract class BaseApiController : ControllerBase
    {
        protected ILogger Logger { get; }

        protected BaseApiController(ILogger logger)
        {
            Logger = logger;
        }

        /// <summary>
        /// 操作成功
        /// </summary>
        /// <param name="message">訊息</param>
        /// <returns></returns>
        protected static JsonResult Success(string message = null)
        {
            return Json(new ResponseModel(ResponseCode.SUCCESS, message));
        }

        /// <summary>
        /// 非法參數
        /// </summary>
        /// <param name="message">訊息</param>
        /// <returns></returns>
        protected static JsonResult BadParams(string message = null)
        {
            return Json(new ResponseModel(ResponseCode.BAD_PARAMS, message));
        }

        /// <summary>
        /// 非法參數
        /// </summary>
        /// <param name="message">訊息</param>
        /// <param name="data"></param>
        /// <returns></returns>
        protected static JsonResult BadParams<T>(string message, T data)
        {
            return Json(new ResponseDataModel<T>()
            {
                Code = ResponseCode.BAD_PARAMS,
                Message = message,
                Data = data
            });
        }

        /// <summary>
        /// 失敗
        /// </summary>
        /// <param name="message">訊息</param>
        /// <returns></returns>
        protected static JsonResult Fail(string message = null)
        {
            return Json(new ResponseModel(ResponseCode.FAIL, message));
        }

        /// <summary>
        /// 系統錯誤
        /// </summary>
        /// <param name="message">訊息</param>
        /// <returns></returns>
        protected static JsonResult Error(string message = null)
        {
            return Json(new ResponseModel(ResponseCode.ERROR, message));
        }

        /// <summary>
        /// 資料
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        protected static JsonResult Data<T>(T data, string message = null)
        {
            return Json(new ResponseDataModel<T>()
            {
                Code = ResponseCode.SUCCESS,
                Message = message,
                Data = data
            });
        }

        /// <summary>
        /// 空的資料
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="message"></param>
        /// <returns></returns>
        protected static JsonResult EmptyData<T>(string message = null)
        {
            return Json(new ResponseDataModel<T>()
            {
                Code = ResponseCode.SUCCESS,
                Message = message,
                Data = default
            });
        }

        /// <summary>
        /// 空的陣列資料
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="message"></param>
        /// <returns></returns>
        protected static JsonResult EmptyArrayData<T>(string message = null)
        {
            return Json(new ResponseDataModel<ICollection<T>>()
            {
                Code = ResponseCode.SUCCESS,
                Message = message,
                Data = Array.Empty<T>()
            });
        }

        /// <summary>
        /// 分頁資料
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="totalCount">總資料數量</param>
        /// <param name="list">資料集合</param>
        /// <param name="message"></param>
        /// <returns></returns>
        protected static JsonResult PagingData<T>(int totalCount, IEnumerable<T> list, string message = null)
        {
            return Json(new ResponsePagingDataModel<T>()
            {
                Code = ResponseCode.SUCCESS,
                Message = message,
                TotalCount = totalCount,
                Data = list,
            });
        }

        protected static JsonResult EmptyPagingData()
        {
            return Json(new ResponsePagingDataModel<object>()
            {
                Code = ResponseCode.SUCCESS,
                TotalCount = 0,
                Data = Array.Empty<object>(),
            });
        }

        /// <summary>
        /// 空的分頁資料
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="message"></param>
        /// <returns></returns>
        protected static JsonResult EmptyPagingData<T>(string message = null)
        {
            return Json(new ResponsePagingDataModel<T>()
            {
                Code = ResponseCode.SUCCESS,
                Message = message,
                TotalCount = 0,
                Data = Array.Empty<T>()
            });
        }

        /// <summary>
        /// 自定義Code代碼
        /// </summary>
        /// <param name="code"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        protected static JsonResult Code(ResponseCode code, string message = null)
        {
            return Json(new ResponseModel(code, message));
        }

        /// <summary>
        /// 自定義Code代碼與資料
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="code"></param>
        /// <param name="message"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        protected static JsonResult Code<T>(ResponseCode code, string message, T data)
        {
            return Json(new ResponseDataModel<T>()
            {
                Code = code,
                Message = message,
                Data = data
            });
        }


        /// <summary>
        /// 返回Json結果
        /// </summary>
        /// <param name="viewModel"></param>
        /// <returns></returns>
        protected static JsonResult Json(object viewModel)
        {
            return new JsonResult(viewModel);
        }
    }
}
