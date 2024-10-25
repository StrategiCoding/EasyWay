namespace EasyWay.Settings
{
    internal interface IAuthSettings
    {
        TimeSpan RefreshTokenLifetime { get; }

         TimeSpan AccessTokenLifetime { get; }
    }
}
