namespace Practice.Common
{
    public abstract class BaseEntity
    {
        protected BaseEntity()
        {
            var now = DateTimeHelper.PrunedUtcNow();

            AddTime = now;
            UpdateTime = now;
        }

        /// <summary>
        /// 自動增長ID (表ID)
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 新增時間
        /// </summary>
        public DateTime AddTime { get; set; }

        /// <summary>
        /// 最後更新時間
        /// </summary>
        public DateTime UpdateTime { get; set; }
    }
}
