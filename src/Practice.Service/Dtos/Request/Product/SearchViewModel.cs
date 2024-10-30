using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Practice.Service.Dtos.Request.Product
{
    public class SearchViewModel
    {
        /// <summary>
        /// 關鍵字
        /// </summary>
        [StringLength(100, MinimumLength = 1)]
        public string keyword { get; set; }

        /// <summary>
        /// 開始時間
        /// </summary>
        public DateTime? startTime { get; set; }

        /// <summary>
        /// 結束時間
        /// </summary>
        public DateTime? endTime { get; set; }

        /// <summary>
        /// 頁碼
        /// </summary>
        [Required]
        [Range(1, 999999)]
        public int? pageIndex { get; set; }

        /// <summary>
        /// 筆數
        /// </summary>
        [Required]
        [Range(1, 100)]
        public int? pageSize { get; set; }
    }
}
