﻿using System.Security.Cryptography;

namespace EasyWay.Internals.GuidGenerators
{
    // Based on code from https://github.com/jhtodd/SequentialGuid/tree/master
    internal static class GuidGenerator
    {
        private static readonly RandomNumberGenerator _random = RandomNumberGenerator.Create();

        private static GuidGeneratorMode _mode = GuidGeneratorMode.SequentialAsString;

        [ThreadStatic] private static Guid? _customId;

        internal static Guid New => _customId.HasValue ? _customId.Value : Create();

        internal static void Set(Guid id) => _customId = id;

        internal static void Reset() => _customId = null;

        internal static void ChangeMode(GuidGeneratorMode mode) => _mode = mode;

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
                case GuidGeneratorMode.SequentialAsString:
                case GuidGeneratorMode.SequentialAsBinary:

                    Buffer.BlockCopy(timestampBytes, 2, guidBytes, 0, 6);
                    Buffer.BlockCopy(randomBytes, 0, guidBytes, 6, 10);

                    if (_mode == GuidGeneratorMode.SequentialAsString && BitConverter.IsLittleEndian)
                    {
                        Array.Reverse(guidBytes, 0, 4);
                        Array.Reverse(guidBytes, 4, 2);
                    }

                    break;

                case GuidGeneratorMode.SequentialAtEnd:

                    Buffer.BlockCopy(randomBytes, 0, guidBytes, 0, 10);
                    Buffer.BlockCopy(timestampBytes, 2, guidBytes, 10, 6);

                    break;
            }

            return new Guid(guidBytes);
        }
    }
}
