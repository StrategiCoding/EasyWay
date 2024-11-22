namespace EasyWay.Internals
{
    internal sealed class Given : IGiven
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
