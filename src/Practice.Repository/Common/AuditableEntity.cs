using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Practice.Common;

namespace Practice.Repository.Common
{
    public abstract class AuditableEntity
    {
        protected AuditableEntity()
        {
            Create = DateTimeHelper.PrunedUtcNow();
        }

        /// <summary>
        /// 識別碼
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 建立時間
        /// </summary>
        public DateTime Create { get; set; }

        /// <summary>
        /// 最後修改時間
        /// </summary>
        public DateTime? LastModified { get; set; }

        public void SetLastModified()
        {
            LastModified = DateTimeHelper.PrunedUtcNow();
        }
    }
}
