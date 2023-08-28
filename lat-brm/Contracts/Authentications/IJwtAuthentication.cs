namespace lat_brm.Contracts.Authentications
{
    public interface IJwtAuthentication
    {
        string GenerateToken(string email);
        string GetEmail(string tokenString);
    }
}
