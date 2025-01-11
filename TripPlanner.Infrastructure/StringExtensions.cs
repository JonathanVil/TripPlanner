namespace TripPlanner.Infrastructure;

public static class StringExtensions
{
    public static string GetSha256Hash(this string text)
    {
        if (string.IsNullOrEmpty(text))
            return string.Empty;

        using var sha = new System.Security.Cryptography.HMACSHA256(key: []);
        var textData = System.Text.Encoding.UTF8.GetBytes(text);
        var hash = sha.ComputeHash(textData);
        return Convert.ToHexString(hash);
    }
}