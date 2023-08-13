using System.Security.Cryptography;
using System.Text;

namespace EppendorfAuthApi.Implementations;

public static class StringExtensions
{
    public static bool IsBase64(this string base64)
    {
        Span<byte> buffer = new(new byte[base64.Length]);

        return Convert.TryFromBase64String(base64, buffer, out var _);
    }

    public static string ToSha256(this string plainText)
    {
        var hashBuilder = new StringBuilder();

        using var hash = SHA256.Create();

        var result = hash.ComputeHash(Encoding.UTF8.GetBytes(plainText));

        foreach (byte b in result)
            hashBuilder.Append(b.ToString("x2"));

        return hashBuilder.ToString();
    }
}