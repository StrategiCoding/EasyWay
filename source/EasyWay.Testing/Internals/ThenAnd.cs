namespace EasyWay.Internals
{
    internal sealed class ThenAnd : IThenAnd
    {
        public IThenAnd And()
        {
            return new ThenAnd();
        }
    }
}
