using EasyWay.Internals.Domain;

namespace EasyWay.Internals.Infrastructure
{
    internal sealed class SecurityTokensRepository : ISecurityTokensRepository
    {
        private static List<SecurityTokens> tokens = new List<SecurityTokens>();

        public Task Add(SecurityTokens storageTokens)
        {
            tokens.Add(storageTokens);

            return Task.CompletedTask;
        }

        public Task<SecurityTokens?> Get(string refreshToken)
        {
            var token = tokens.SingleOrDefault(x => x.HashedRefreshToken == refreshToken);

            return Task.FromResult(token);
        }

        public Task<bool> Exists(Guid userId)
        {
            return Task.FromResult(tokens.Any(x => x.UserId == userId));
        }

        public Task Remove(Guid userId)
        {
            var token = tokens.SingleOrDefault(x => x.UserId == userId);

            if (token != null)
            {
                tokens.Remove(token);
            }

            return Task.CompletedTask;
        }

        public Task Remove(string refreshToken)
        {
            var token = tokens.SingleOrDefault(x => x.HashedRefreshToken == refreshToken);

            if (token != null)
            {
                tokens.Remove(token);
            }

            return Task.CompletedTask;
        }
    }
}
