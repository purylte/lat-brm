namespace lat_brm.Contracts.Utilities
{
    public interface IJwtHandler
    {
        string GenerateToken(string email, string fullName);
        string GetEmail(string tokenString);
        string GetFullName(string tokenString);
    }
}
