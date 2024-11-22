namespace EasyWay.Internals
{
    internal sealed class When : IWhen
    {
        public IThen Then()
        {
            return new Then();
        }
    }
}
