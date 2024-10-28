namespace Practice.Common
{
    public static class DateTimeExtension
    {
        private const long TicksPerMillisecond = 10000;

        /// <summary>
        /// 取得毫秒精確度的 Unix 時間戳
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        public static long ConvertToUnixTimeMilliseconds(this DateTime dateTime)
        {
            if (dateTime.Kind == DateTimeKind.Unspecified)
            {
                throw new ArgumentException("dateTime.Kind cannot be unspecified");
            }

            return new DateTimeOffset(dateTime).ToUnixTimeMilliseconds();
        }

        /// <summary>
        /// 取得毫秒精確度的 Unix 時間戳
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        public static long? ConvertToUnixTimeMilliseconds(this DateTime? dateTime)
        {
            if (!dateTime.HasValue)
            {
                return null;
            }

            return dateTime.Value.ConvertToUnixTimeMilliseconds();
        }

        /// <summary>
        /// 將時間精度刪減至毫秒
        /// </summary>
        /// <param name="dateTime"></param>
        public static DateTime PruneToMilliseconds(this DateTime dateTime)
        {
            return new DateTime(ticks: dateTime.Ticks - dateTime.Ticks % TicksPerMillisecond,
                kind: dateTime.Kind);
        }

        /// <summary>
        /// 將時間精度刪減至毫秒
        /// </summary>
        /// <param name="dateTimeOffset"></param>
        public static DateTimeOffset PruneToMilliseconds(this DateTimeOffset dateTimeOffset)
        {
            return new DateTimeOffset(ticks: dateTimeOffset.Ticks - dateTimeOffset.Ticks % TicksPerMillisecond,
                offset: dateTimeOffset.Offset);
        }
    }
}
