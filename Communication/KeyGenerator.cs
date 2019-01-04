using System.Text;
using System.Security.Cryptography;

namespace Data_Server.Communication {
    public sealed class KeyGenerator {
        private const string Words = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
        private const int Size = 15;

        public string GetUniqueKey() {
            char[] chars = Words.ToCharArray();
            byte[] data = new byte[Size];

            using (RNGCryptoServiceProvider crypto = new RNGCryptoServiceProvider()) {
                crypto.GetBytes(data);
            }

            StringBuilder result = new StringBuilder(Size);

            foreach (byte b in data) {
                result.Append(chars[b % (chars.Length)]);
            }

            return result.ToString();
        }
    }
}