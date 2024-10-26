using EasyWay.Internals.Domain.SeedWorks;

namespace EasyWay.Internals.Application.Cancel
{
    internal interface ICancelRefreshToken
    {
        Task<SecurityResult> Cancel(string? refreshToken);
    }
}
