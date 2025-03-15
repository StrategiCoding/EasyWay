namespace EasyWay.Internals.Commands
{
    internal enum CommandErrorEnum : byte
    {
        None = 1,
        Validation = 2,
        BrokenBusinessRule = 3,
        ConcurrencyConflict = 4,
        OperationCanceled = 5,
        NotFound = 6,
        Forbidden = 7,
        UnknownException = 8
    }
}
