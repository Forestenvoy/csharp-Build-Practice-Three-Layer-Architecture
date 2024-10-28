namespace Practice.Common
{
    public static class DateTimeHelper
    {
        public static DateTime PrunedUtcNow()
        {
            return DateTime.UtcNow.PruneToMilliseconds();
        }
    }
}
