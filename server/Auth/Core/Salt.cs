using System;
using System.Security.Cryptography;

namespace Auth.Core
{
    public class Salt
    {
        private const uint DefaultSize = 32;

        public byte[] Data { get; private set; }

        public string AsString => Convert.ToBase64String(Data);

        public uint Size { get; private set; }

        public Salt(uint size = DefaultSize)
        {
            Size = size;

            Data = new byte[size];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(Data);
            }
        }

        public Salt(byte[] data)
        {
            Data = data;
        }
    }
}