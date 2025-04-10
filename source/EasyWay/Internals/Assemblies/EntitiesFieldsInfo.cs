using System.Collections.ObjectModel;
using System.Reflection;

namespace EasyWay.Internals.Assemblies
{
    internal sealed class EntitiesFieldsInfo
    {
        internal static ReadOnlyDictionary<Type, FieldInfo[]> Dictionary;

        internal EntitiesFieldsInfo(IEnumerable<Type> types)
        {
            var dictionary = new Dictionary<Type, FieldInfo[]>();

            var aggregateRootTypes = types.Where(x => x.IsSubclassOf(typeof(AggregateRoot)));

            foreach (var aggregateRootType in aggregateRootTypes)
            {
                var fields = aggregateRootType.GetFields(BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Public);

                dictionary.Add(aggregateRootType, fields);
            }

            Dictionary = new ReadOnlyDictionary<Type, FieldInfo[]>(dictionary);
        }
    }
}
