using System.Security.Cryptography;
using System.Text;

namespace BSharp.Wallet.SHA
{
    internal static class SHA256
    {
        internal static byte[] GenerateHashBytes(this string secret)
        {
            byte[] textBytes = Encoding.UTF8.GetBytes(secret);
#pragma warning disable SYSLIB0021 // Type or member is obsolete
            byte[] hashBytes = new SHA256Managed().ComputeHash(textBytes);
#pragma warning restore SYSLIB0021 // Type or member is obsolete

            return hashBytes;
        }
    }
}
