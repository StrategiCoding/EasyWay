namespace EasyWay.Internals
{
    internal sealed class Then : IThen
    {
        public IThenAnd And()
        {
            return new ThenAnd();
        }
    }
}
