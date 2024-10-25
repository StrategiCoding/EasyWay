
namespace EasyWay.Internals.Storage
{
    internal class TokensStorage : ITokensStorage
    {
        private static List<StorageTokens> tokens = new List<StorageTokens>();

        public Task Add(StorageTokens storageTokens)
        {
            tokens.Add(storageTokens);

            return Task.CompletedTask;
        }

        public Task<StorageTokens?> Get(string refreshToken)
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
    }
}
