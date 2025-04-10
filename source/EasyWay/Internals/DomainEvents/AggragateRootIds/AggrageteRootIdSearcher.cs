using EasyWay.Internals.Assemblies;
using System.Collections;
using System.Reflection;

namespace EasyWay.Internals.DomainEvents.AggragateRootIds
{
    internal sealed class AggrageteRootIdSearcher
    {
        private readonly IEntitiesContext _context;

        private readonly RelationshipsBetweenEntityAndAggragaeRoot _relation;

        public AggrageteRootIdSearcher(
            IEntitiesContext context,
            RelationshipsBetweenEntityAndAggragaeRoot relation)
        {
            _context = context;
            _relation = relation;
        }

        public Guid SearchId(Entity entity)
        {
            if (entity.GetType().IsSubclassOf(typeof(AggregateRoot)))
            {
                return entity.Id;
            }

            var aggrageteRoots = _context.GetAggregateRoots();

            if (aggrageteRoots.Count() == 1)
            {
                return aggrageteRoots.Single().Id;
            }

            var aggragateRootType = _relation[entity.GetType()];

            var aggragateRoots = _context.GetAggregateRoots().Where(x => x.GetType() == aggragateRootType);

            foreach (var aggregateRoot in aggragateRoots)
            {
                var id = SearchInAggragates(entity, aggregateRoot);

                if (id is not null)
                {
                    return id.Value;
                }
            }

            throw new NotFoundAggragateIdForDomainEvent();
        }

        private static Guid? SearchInAggragates(Entity entity, AggregateRoot aggregateRoot)
        {
            var fields = EntitiesFieldsInfo.Dictionary[aggregateRoot.GetType()];

            foreach (var field in fields)
            {
                var isEntity = field.FieldType.IsSubclassOf(typeof(Entity));

                if (isEntity)
                {
                    var isEqual = field.GetValue(aggregateRoot).Equals(entity);

                    if (isEqual)
                    {
                        return aggregateRoot.Id;
                    }
                }

                if (field.FieldType != typeof(string) && typeof(IEnumerable).IsAssignableFrom(field.FieldType))
                {
                    if (field.GetValue(aggregateRoot) is IEnumerable enumerable)
                    {
                        foreach (var en in enumerable)
                        {
                            if (en is Entity entityItem)
                            {
                                if (entityItem == entity)
                                {
                                    return aggregateRoot.Id;
                                }

                            }
                        }
                    }
                }
            }
            return null;
        }
    }
}
