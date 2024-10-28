using System.Text.Json.Serialization;

namespace Practice.Common.ViewModels
{
    /// <summary>
    /// 包含資料的ViewModel
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ResponseDataModel<T> : ResponseModel
    {
        public ResponseDataModel() { }

        public ResponseDataModel(ResponseCode code) : base(code) { }

        public ResponseDataModel(ResponseCode code, string message) : base(code, message) { }

        public ResponseDataModel(T data) : base(ResponseCode.SUCCESS)
        {
            Data = data;
        }

        public ResponseDataModel(T data, ResponseCode code) : base(code)
        {
            Data = data;
        }

        /// <summary>
        /// 附帶的資料內容
        /// </summary>
        [Newtonsoft.Json.JsonProperty("data")]
        [JsonPropertyName("data")]
        public T Data { get; set; }
    }
}
