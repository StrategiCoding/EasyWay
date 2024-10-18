namespace EasyWay.Internals.AggregateRoots
{
    internal sealed class ConcurrencyTokenUpdater : IConcurrencyTokenUpdater
    {
        private readonly IAggregateRootsContext _aggragateRootsContext;

        public ConcurrencyTokenUpdater(
            IAggregateRootsContext aggragateRootsContext)
        {
            _aggragateRootsContext = aggragateRootsContext;
        }

        public void Update()
        {
            var aggragateRoots = _aggragateRootsContext.GetAggregateRoots();

            foreach (var aggragateRoot in aggragateRoots)
            {
                aggragateRoot.UpdateConcurrencyToken();
            }
        }
    }
}
