using System.Collections.ObjectModel;

namespace EasyWay.Internals.DomainEvents.AggragateRootIds
{
    internal sealed class RelationshipsBetweenEntityAndAggragaeRoot : ReadOnlyDictionary<Type, Type>
    {
        public RelationshipsBetweenEntityAndAggragaeRoot(IDictionary<Type, Type> dictionary) 
            : base(dictionary) { }
    }
}
