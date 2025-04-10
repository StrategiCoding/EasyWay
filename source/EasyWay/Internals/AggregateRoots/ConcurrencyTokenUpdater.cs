using EasyWay.Internals.DomainEvents;

namespace EasyWay.Internals.AggregateRoots
{
    internal sealed class ConcurrencyTokenUpdater
    {
        private readonly IEntitiesContext _context;

        public ConcurrencyTokenUpdater(IEntitiesContext context)
        {
            _context = context;
        }

        public void Update()
        {
            var aggragateRoots = _context.GetAggregateRoots();

            foreach (var aggragateRoot in aggragateRoots)
            {
                aggragateRoot.UpdateConcurrencyToken();
            }
        }
    }
}
