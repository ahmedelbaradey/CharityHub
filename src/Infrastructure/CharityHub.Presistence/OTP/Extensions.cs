using System.Security.Cryptography;

namespace CharityHub.DomainService.OTP
{
    internal static class Extensions
    {
        internal static byte[] ComputeHash(this HMACAlgo algo, byte[] key, byte[] payload)
        {
            HMAC hmac = algo switch
            {
                HMACAlgo.Sha1 => new HMACSHA1(key),
                HMACAlgo.Sha256 => new HMACSHA256(key),
                HMACAlgo.Sha512 => new HMACSHA512(key),
                _ => throw new ArgumentOutOfRangeException(nameof(algo), "Unknown HMAC algorithm"),
            };

            return hmac.ComputeHash(payload);
        }
        internal static byte[] ToBytes(this string hex)
        {
            if (hex.Length % 2 == 1)
                throw new Exception("The binary key cannot have an odd number of digits");

            byte[] arr = new byte[hex.Length >> 1];

            for (int i = 0; i < hex.Length >> 1; ++i)
                arr[i] = (byte)((GetHexVal(hex[i << 1]) << 4) + GetHexVal(hex[(i << 1) + 1]));

            return arr;
        }

        private static int GetHexVal(char hex)
        {
            // cast to int
            int val = hex;
            return val - (val < 58 ? 48 : val < 97 ? 55 : 87);
        }
    }
}
