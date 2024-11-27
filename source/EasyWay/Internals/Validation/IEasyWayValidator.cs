namespace EasyWay.Internals.Validation
{
    internal interface IEasyWayValidator<T>
    {
        IDictionary<string, string[]> Validate(T objectToValidate);
    }
}
