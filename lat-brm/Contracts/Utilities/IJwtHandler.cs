namespace lat_brm.Contracts.Utilities
{
    public interface IJwtHandler
    {
        string GenerateToken(string email);
        string GetEmail(string tokenString);
    }
}
