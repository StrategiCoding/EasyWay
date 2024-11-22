namespace EasyWay.Internals
{
    internal sealed class GivenAnd : IGivenAnd
    {
        public IGivenAnd And()
        {
            return new GivenAnd();
        }

        public IWhen When()
        {
            return When();
        }
    }
}
