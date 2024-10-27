using EasyWay.Internals.Domain.SeedWorks.Results;

namespace EasyWay.Internals.Application.Cancel
{
    internal interface ICancelRefreshToken
    {
        Task<SecurityResult> Cancel(string? refreshToken);
    }
}
