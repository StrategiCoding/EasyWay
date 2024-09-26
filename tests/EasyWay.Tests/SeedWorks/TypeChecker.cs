namespace EasyWay.Tests.SeedWorks
{
    internal static class TypeChecker
    {
        public static Type IsPublic(this Type type)
        {
            Assert.True(type.IsPublic, $"{type.Name} must be public class");

            return type;
        }

        public static Type IsSealed(this Type type)
        {
            Assert.True(type.IsSealed, $"{type.Name} must be sealed class");

            return type;
        }

        public static Type IsAbstract(this Type type)
        {
            Assert.True(type.IsAbstract, $"{type.Name} must be abstract class");

            return type;
        }

        public static Type HasFullName(this Type type, string fullNameType)
        {
            Assert.True(fullNameType == type.FullName, "Incorrect full name type");

            return type;
        }

        public static Type InheritanceFrom(this Type type, Type baseClass)
        {
            Assert.True(type.IsSubclassOf(baseClass), $"{type.Name} must inheritance from {baseClass.Name}");

            return type;
        }
    }
}
