using System.Text;
using System.Text.Json;

namespace LeaveRequests.Client.Auth;

public class JwtService
{
   
    public static Dictionary<string, object>? GetClaims(string token)
    {
       
            var parts = token.Split('.');
            if (parts.Length != 3)
                return null;

            var payload = parts[1];
            var padded = payload
                .Replace("-", "+")
                .Replace("_", "/");

            switch (padded.Length % 4)
            {
                case 2: padded += "=="; break;
                case 3: padded += "="; break;
                
            }
            var bytes = Convert.FromBase64String(padded);
            var json = Encoding.UTF8.GetString(bytes);
            return JsonSerializer.Deserialize<Dictionary<string, object>> (json);
    }

    public static string? GetRole(string token)
    {
        var claims = GetClaims(token);
        if (claims is null)
            return null;

        if (claims.TryGetValue("role", out var role))
            return role?.ToString();
        return null;
    }
}