using System.Text.Json.Serialization;

namespace Practice.Common.ViewModels
{
    /// <summary>
    /// 已分頁的資料ViewModel
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ResponsePagingDataModel<T> : ResponseDataModel<IEnumerable<T>>
    {
        public ResponsePagingDataModel() { }

        public ResponsePagingDataModel(ResponseCode code) : base(code)
        {
            Data = Array.Empty<T>();
        }

        public ResponsePagingDataModel(ResponseCode code, string message) : base(code, message)
        {
            Data = Array.Empty<T>();
        }

        public ResponsePagingDataModel(int totalCount, IEnumerable<T> data) : base(ResponseCode.SUCCESS)
        {
            TotalCount = totalCount;
            Data = data;
        }

        [Newtonsoft.Json.JsonProperty("totalCount")]
        [JsonPropertyName("totalCount")]
        public int TotalCount { get; set; }
    }
}
