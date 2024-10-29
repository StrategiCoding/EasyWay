namespace EasyWay.Settings
{
    internal interface IAuthSettings
    {
        string SecretKey { get; }

        TimeSpan RefreshTokenLifetime { get; }

        int RefreshTokenSize { get; }

        string RefreshTokenSalt { get; }

        TimeSpan AccessTokenLifetime { get; }
    }
}
