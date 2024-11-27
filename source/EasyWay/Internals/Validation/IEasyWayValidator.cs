namespace EasyWay.Internals.Validation
{
    internal interface IEasyWayValidator
    {
        IDictionary<string, string[]> Validate<T>(T objectToValidate);
    }
}
