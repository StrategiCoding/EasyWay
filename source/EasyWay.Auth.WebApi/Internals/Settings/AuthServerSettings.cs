using EasyWay.Settings;

namespace EasyWay.Internals.Settings
{
    internal sealed class AuthServerSettings : IAuthServerSettings, IAuthSettings
    {
        public string Domain { get; set; } = "localhost";

        public TimeSpan RefreshTokenLifetime { get; set; } = TimeSpan.FromDays(1);

        public TimeSpan AccessTokenLifetime { get; set; } = TimeSpan.FromSeconds(3);

        //TODO
        public string SecretKey { get; set; } = "XN32ifS0ZumZ0QZTAFyY86GdQRPnTHjwzh42KpflDemEZ+Ewlzpgb3N5l8u9/jWV";

        public int RefreshTokenSize { get; set; } = 32;

        public string RefreshTokenSalt { get; set; } = "wdxWsh+ZRpRhe4MByN2RzTESn4XbVE8xn/cKg5jp450=";

        internal AuthServerSettings() { }
    }
}
