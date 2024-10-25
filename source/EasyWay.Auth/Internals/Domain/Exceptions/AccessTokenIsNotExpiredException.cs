namespace EasyWay.Internals.Domain.Exceptions
{
    internal sealed class AccessTokenIsNotExpiredException : ForbiddenException
    {
        internal AccessTokenIsNotExpiredException() { }
    }
}
