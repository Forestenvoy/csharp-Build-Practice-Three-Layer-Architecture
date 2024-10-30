namespace Practice.Service.Dtos.Response.Product
{
    public class ProductViewModel
    {
        /// <summary>
        /// 產品 ID
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 名稱
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 價格
        /// </summary>
        public int Price { get; set; }

        /// <summary>
        /// 建立時間
        /// </summary>
        public DateTime AddTime { get; set; }

        /// <summary>
        /// 編輯時間
        /// </summary>
        public DateTime UpdateTime { get; set; }
    }
}
