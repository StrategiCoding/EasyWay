namespace EasyWay.Internals.Settings
{
    internal sealed class AuthServerSettings : IAuthServerSettings
    {
        public string Domain { get; set; } = "localhost";

        public bool Secure { get; set; } = true;

        internal AuthServerSettings() { }
    }
}
