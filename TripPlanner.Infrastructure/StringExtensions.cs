namespace TripPlanner.Infrastructure;

public static class StringExtensions
{
    public static string GetSha256Hash(this string text)
    {
        if (String.IsNullOrEmpty(text))
            return String.Empty;

        using var sha = new System.Security.Cryptography.HMACSHA256();
        var textData = System.Text.Encoding.UTF8.GetBytes(text);
        var hash = sha.ComputeHash(textData);
        return BitConverter.ToString(hash).Replace("-", String.Empty);
    }
}