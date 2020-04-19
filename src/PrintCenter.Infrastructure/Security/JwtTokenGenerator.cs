using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace PrintCenter.Infrastructure.Security
{
    public class JwtTokenGenerator : IJwtTokenGenerator
    {
        private readonly IConfiguration configuration;

        public JwtTokenGenerator(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        
        public string CreateToken(int userId, string username, string role)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var keyval = configuration["Secret"];
            var key = Encoding.ASCII.GetBytes(keyval);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.UserId, userId.ToString()),
                    new Claim(ClaimTypes.UserName, username),
                    new Claim(ClaimTypes.Role, role)
                }),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature),

            };
            var token = tokenHandler.CreateToken(tokenDescriptor);

            var res = tokenHandler.WriteToken(token);
            return res;
        }
    }
}
