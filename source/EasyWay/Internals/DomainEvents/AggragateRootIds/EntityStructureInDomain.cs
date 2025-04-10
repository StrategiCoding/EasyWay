using EasyWay.Internals.Assemblies;
using System.Collections;
using System.Reflection;

namespace EasyWay.Internals.DomainEvents.AggragateRootIds
{
    internal static class EntityStructureInDomain
    {
        internal static RelationshipsBetweenEntityAndAggragaeRoot Get(IEnumerable<Assembly> assemblies)
        {
            var relation = new Dictionary<Type, Type?>();

            var types = assemblies.SelectMany(x => x.GetTypes());

            var aggregateRoots = types.Where(x => x.IsSubclassOf(typeof(AggregateRoot)));


            foreach (var aggregateType in aggregateRoots)
            {
                var fields = EntitiesFieldsInfo.Dictionary[aggregateType];

                foreach (var field in fields)
                {
                    var fieldType = field.FieldType;

                    var isEntity = fieldType.IsSubclassOf(typeof(Entity));

                    if (isEntity)
                    {
                        relation.Add(fieldType, aggregateType);
                    }

                    if (fieldType != typeof(string) && typeof(IEnumerable).IsAssignableFrom(fieldType))
                    {
                        relation.Add(fieldType, aggregateType);

                        //TODO UseCase (very rarely) entity in entity 
                    }
                }
            }

            return new RelationshipsBetweenEntityAndAggragaeRoot(relation);
        }
    }
}
