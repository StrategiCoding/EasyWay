namespace EasyWay.Internals.Domain.SeedWorks.Clocks
{
    internal static class SecurityClock
    {
        [ThreadStatic] private static DateTime? _customDateTime;

        public static DateTime UtcNow
        {
            get
            {
                if (_customDateTime.HasValue)
                {
                    return _customDateTime.Value;
                }

                return DateTime.UtcNow;
            }
        }

        internal static void Set(DateTime customDateTime)
        {
            if (customDateTime.Kind != DateTimeKind.Utc)
            {
                throw new CustomDateTimeMustBeUtcException();
            }

            _customDateTime = customDateTime;
        }

        internal static void Reset() => _customDateTime = null;
    }
}
