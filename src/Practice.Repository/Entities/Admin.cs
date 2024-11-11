using Practice.Repository.Common;

namespace Practice.Repository.Entities
{
    public class Admin : AuditableEntity
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
