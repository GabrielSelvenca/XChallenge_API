using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using XChallenge_API.Contexts;
using XChallenge_API.Models;

namespace XChallenge_API.Services
{
    public class TokenService
    {
        private readonly string _secretkey = "xchallenge-secretkey-api-1234567890*-*xchallenge-secretkey-api-1234567890";
        private readonly MainContext _ctx;

        public TokenService(MainContext ctx)
        {
            _ctx = ctx;
        }

        public string TokenGenerate(Acesso user)
        {
            var handler = new JwtSecurityTokenHandler();

            var key = Encoding.UTF8.GetBytes(_secretkey);
            var credentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = ClaimsGenerate(user),
                SigningCredentials = credentials,
                Expires = DateTime.UtcNow.AddHours(1),
            };

            var token = handler.CreateToken(tokenDescriptor);

            return handler.WriteToken(token);
        }

        private static ClaimsIdentity ClaimsGenerate(Acesso user)
        {
            var ci = new ClaimsIdentity();

            ci.AddClaim(new Claim(ClaimTypes.NameIdentifier, user.IdAcesso.ToString()));
            ci.AddClaim(new Claim(ClaimTypes.Name, user.Nome));
            ci.AddClaim(new Claim(ClaimTypes.Email, user.Email));

            return ci;
        }
    }
}
