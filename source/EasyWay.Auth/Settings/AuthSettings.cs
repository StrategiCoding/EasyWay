namespace EasyWay.Settings
{
    public sealed class AuthSettings : IAuthSettings
    {
        public TimeSpan RefreshTokenLifetime { get; set; } = TimeSpan.FromDays(1);

        public TimeSpan AccessTokenLifetime { get; set; } = TimeSpan.FromMinutes(5);

        internal AuthSettings() { }
    }
}
