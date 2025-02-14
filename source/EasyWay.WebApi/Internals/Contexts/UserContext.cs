using Microsoft.AspNetCore.Http;

namespace EasyWay.Internals.Contexts
{
    internal sealed class UserContext : IUserContext
    {
        public UserContext(IHttpContextAccessor httpContextAccessor) 
        {
            UserId = httpContextAccessor
                .HttpContext?
                .User?
                .Claims?
                .SingleOrDefault(x => x.Type == "sub")?
                .Value;
        }

        public string? UserId { get; }

        public bool IsAuthenticated => UserId is not null;
    }
}
