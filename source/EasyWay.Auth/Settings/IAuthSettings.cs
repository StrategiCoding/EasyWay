namespace EasyWay.Settings
{
    internal interface IAuthSettings
    {
        string SecretKey { get; }

        TimeSpan RefreshTokenLifetime { get; }

        int RefreshTokenSize { get; }

        TimeSpan AccessTokenLifetime { get; }
    }
}
