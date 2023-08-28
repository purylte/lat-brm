using System.Security.Claims;

namespace lat_brm.Contracts.Utilities
{
    public interface IJwtHandler
    {
        string GenerateToken(ClaimsIdentity subject);
        string GetEmail(string tokenString);
        string GetFullName(string tokenString);
    }
}
