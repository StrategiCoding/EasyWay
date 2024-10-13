using Microsoft.AspNetCore.Http;

namespace EasyWay.Internals.Contexts
{
    internal sealed class UserContext : IUserContext
    {
        public UserContext(IHttpContextAccessor httpContextAccessor) 
        {
            var userId = httpContextAccessor
                .HttpContext?
                .User?
                .Claims?
                .SingleOrDefault(x => x.Type == "sub")?
                .Value;

            UserId = userId is null ? null : new Guid(userId);
        }

        public Guid? UserId { get; }

        public bool IsAuthenticated => UserId is not null;
    }
}
