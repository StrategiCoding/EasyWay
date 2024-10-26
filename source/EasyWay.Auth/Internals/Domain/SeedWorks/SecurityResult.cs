namespace EasyWay.Internals.Domain.SeedWorks
{
    internal class SecurityResult
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

        internal static SecurityResult Success() => new();

        internal static SecurityResult Failure<TSecurityResult>(TSecurityResult error)
            where TSecurityResult : SecurityError => new (error);
    }
}
