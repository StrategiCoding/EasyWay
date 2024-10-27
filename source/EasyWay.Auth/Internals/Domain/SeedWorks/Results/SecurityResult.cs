namespace EasyWay.Internals.Domain.SeedWorks.Results
{
    internal sealed class SecurityResult
    {
        internal bool IsSuccess { get; }

        internal bool IsFailure => !IsSuccess;

        internal SecurityError Error { get; }

        private SecurityResult()
        {
            IsSuccess = true;
            Error = new NoneSecurityError();
        }

        private SecurityResult(SecurityError securityError)
        {
            IsSuccess = false;
            Error = securityError;
        }

        internal static SecurityResult Success => new();

        internal static SecurityResult Failure<TSecurityResult>(TSecurityResult error)
            where TSecurityResult : SecurityError => new(error);
    }

    internal sealed class SecurityResult<TValue>
    {
        internal TValue Value { get; }

        internal bool IsSuccess { get; }

        internal bool IsFailure => !IsSuccess;

        internal SecurityError Error { get; }

        private SecurityResult(TValue value)
        {
            Value = value;
            IsSuccess = true;
            Error = new NoneSecurityError();
        }

        private SecurityResult(SecurityError securityError)
        {
            IsSuccess = false;
            Error = securityError;
        }

        internal static SecurityResult<TValue> Success<TValue>(TValue value) => new(value);

        internal static SecurityResult<TValue> Failure<TSecurityResult>(TSecurityResult error)
            where TSecurityResult : SecurityError => new(error);
    }
}


