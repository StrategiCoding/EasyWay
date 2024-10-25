namespace EasyWay.Internals.Settings
{
    internal interface IAuthServerSettings
    {
        string Domain { get; }

        bool Secure { get; }
    }
}
