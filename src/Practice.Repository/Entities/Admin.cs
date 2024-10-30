using Practice.Common;

namespace Practice.Repository.Entities
{
    public class Admin : BaseEntity
    {
        /// <summary>
        /// 帳號
        /// </summary>
        public string Account { get; set; }

        /// <summary>
        /// 密碼
        /// </summary>
        public string Password { get; set; }
    }
}
