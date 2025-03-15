namespace EasyWay.Internals.Queries.Results
{
    internal enum QueryErrorEnum : byte
    {
        None = 1,
        Validation = 2,
        NotFound = 3,
        Forbidden = 4,
        OperationCanceled = 5,
        UnknownException = 6
    }
}
