namespace EasyWay.Settings
{
    internal interface IAuthSettings
    {
        string SecretKey { get; }

        TimeSpan RefreshTokenLifetime { get; }

        TimeSpan AccessTokenLifetime { get; }
    }
}
