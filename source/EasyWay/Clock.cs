using EasyWay.Internals.Clocks;

namespace EasyWay
{
    public sealed class Clock
    {
        public TimeProvider TimeProvider => InternalClock.TimeProvider;

        public DateTime UtcNow => InternalClock.UtcNow;
        
        internal Clock() { }
    }
}
