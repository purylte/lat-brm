using lat_brm.Contracts.Authentications;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace lat_brm.Authentications
{
    public class JwtAuthentication : IJwtAuthentication
    {
        private readonly IConfiguration _configuration;

        public JwtAuthentication(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string GenerateToken(string email)
        {
            var issuer = _configuration["Jwt:Issuer"];
            var audience = _configuration["Jwt:Audience"];
            var key = Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]);
            var expiry = DateTime.Now.AddMinutes(120);

            var subject = new ClaimsIdentity(new Claim[] {
                new Claim(JwtRegisteredClaimNames.Jti,
                Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email, email),
            });


            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = subject,
                Expires = expiry,
                Issuer = issuer,
                Audience = audience,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha512Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public string GetEmail(string tokenString)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            JwtSecurityToken token = tokenHandler.ReadJwtToken(tokenString);

            return token.Claims.Single(claim => claim.Type.Equals(JwtRegisteredClaimNames.Email)).Value;
        }
    }
}
