namespace EasyWay.Internals.Clocks
{
    internal static class InternalClock
    {
        [ThreadStatic] private static TimeProvider? _testTimeProvider;

        private static TimeProvider _timeProvider;

        internal static TimeProvider TimeProvider => _testTimeProvider is null ? _timeProvider : _testTimeProvider;

        internal static DateTime UtcNow => TimeProvider.GetUtcNow().UtcDateTime;

        internal static void Initialize() => _timeProvider = TimeProvider.System;

        internal static void Test(TimeProvider timeProvider) => _testTimeProvider = timeProvider;
    }
}
