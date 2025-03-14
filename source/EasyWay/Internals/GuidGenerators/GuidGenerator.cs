using EasyWay.Internals.Clocks;

namespace EasyWay.Internals.GuidGenerators
{
    internal static class GuidGenerator
    {
        [ThreadStatic] private static Guid? _customId;

        internal static Guid New => _customId.HasValue ? _customId.Value : Create();

        internal static void Set(Guid id) => _customId = id;

        internal static void Reset() => _customId = null;

        private static Guid Create() => Guid.CreateVersion7(InternalClock.TimeProvider.GetUtcNow());
    }
}
