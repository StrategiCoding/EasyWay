using EasyWay.Internals.SystemCloks;

namespace EasyWay
{
    public static class SystemClock
    {
        [ThreadStatic]
        private static TimeSpan? _differenceBetweenMoments;

        public static DateTime UtcNow
        {
            get
            {
                if (_differenceBetweenMoments.HasValue)
                {
                    return DateTime.UtcNow.Add(_differenceBetweenMoments.Value);
                }

                return DateTime.UtcNow;
            }
        }

        public static void Set(DateTime customDateTime)
        {
            if (customDateTime.Kind != DateTimeKind.Utc)
            {
                throw new CustomDateTimeMustBeUtcException();
            }

            _differenceBetweenMoments = customDateTime - DateTime.UtcNow;
        }

        public static void Reset() => _differenceBetweenMoments = null;
    }
}
