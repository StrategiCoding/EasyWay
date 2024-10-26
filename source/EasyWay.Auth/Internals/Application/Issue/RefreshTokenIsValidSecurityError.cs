using EasyWay.Internals.Domain.SeedWorks;

namespace EasyWay.Internals.Application.Issue
{
    internal sealed class RefreshTokenIsValidSecurityError : SecurityError
    {
        internal override string Message => "Refresh token is valid";
    }
}
