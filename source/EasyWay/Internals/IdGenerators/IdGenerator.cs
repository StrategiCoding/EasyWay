using System.Security.Cryptography;

namespace EasyWay.Internals.IdGenerators
{
    // Based on code from https://github.com/jhtodd/SequentialGuid/tree/master
    internal static class IdGenerator
    {
        private static readonly RandomNumberGenerator _random = RandomNumberGenerator.Create();

        private static IdGeneratorMode _mode = IdGeneratorMode.SequentialAsString;

        internal static Guid New => Create();

        internal static void ChangeMode(IdGeneratorMode mode) => _mode = mode;

        private static Guid Create()
        {
            var randomBytes = new byte[10];
            _random.GetBytes(randomBytes);

            long timestamp = DateTime.UtcNow.Ticks / 10000L;

            byte[] timestampBytes = BitConverter.GetBytes(timestamp);

            if (BitConverter.IsLittleEndian)
            {
                Array.Reverse(timestampBytes);
            }

            byte[] guidBytes = new byte[16];

            switch (_mode)
            {
                case IdGeneratorMode.SequentialAsString:
                case IdGeneratorMode.SequentialAsBinary:

                    Buffer.BlockCopy(timestampBytes, 2, guidBytes, 0, 6);
                    Buffer.BlockCopy(randomBytes, 0, guidBytes, 6, 10);

                    if (_mode == IdGeneratorMode.SequentialAsString && BitConverter.IsLittleEndian)
                    {
                        Array.Reverse(guidBytes, 0, 4);
                        Array.Reverse(guidBytes, 4, 2);
                    }

                    break;

                case IdGeneratorMode.SequentialAtEnd:

                    Buffer.BlockCopy(randomBytes, 0, guidBytes, 0, 10);
                    Buffer.BlockCopy(timestampBytes, 2, guidBytes, 10, 6);

                    break;
            }

            return new Guid(guidBytes);
        }
    }
}
