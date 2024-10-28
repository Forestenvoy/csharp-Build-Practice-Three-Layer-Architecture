namespace Practice.Common
{
    public static class TimeUtil
    {
        public static void ThrowExceptionIfNotInUtc08Timezone()
        {
            DateTime localNow = DateTime.UtcNow;
            TimeZoneInfo timeZoneInfo = TimeZoneInfo.Local;
            TimeSpan utcOffset = timeZoneInfo.GetUtcOffset(localNow);

            if (utcOffset != TimeSpan.FromHours(8))
            {
                throw new NotSupportedException($"系統時區非 +8 時間");
            }
        }
    }
}
