using System.Text.Json.Serialization;

namespace Practice.Common.ViewModels
{
    /// <summary>
    /// 基本的 ViewModel
    /// </summary>
    public class ResponseModel
    {
        public ResponseModel() { }

        public ResponseModel(ResponseCode code)
        {
            Code = code;
        }

        public ResponseModel(ResponseCode code, string message)
        {
            Code = code;
            Message = message;
        }

        /// <summary>
        /// 是否為成功狀態
        /// </summary>
        [Newtonsoft.Json.JsonIgnore]
        [JsonIgnore]
        public bool IsSuccess => Code >= 0;

        /// <summary>
        /// 狀態代碼
        /// </summary>
        [Newtonsoft.Json.JsonProperty("code")]
        [JsonPropertyName("code")]
        public ResponseCode Code { get; set; }

        /// <summary>
        /// 訊息
        /// </summary>
        [Newtonsoft.Json.JsonProperty("msg")]
        [JsonPropertyName("msg")]
        public string Message { get; set; }
    }
}
