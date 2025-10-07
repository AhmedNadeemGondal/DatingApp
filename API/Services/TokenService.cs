using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using API.Entities;
using API.Interfaces;
using Microsoft.IdentityModel.Tokens;

namespace API.Services;

public class TokenService(IConfiguration config) : ITokenService
{
    public string CreateToken(AppUser user)
    {
        var tokenKey = config["TokenKey"] ?? throw new Exception("Cannot get token key.");
        if (tokenKey.Length < 64) throw new Exception("Your token key needs to be >=64 chars.");
        // var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenKey));
        SymmetricSecurityKey key = new(Encoding.UTF8.GetBytes(tokenKey));

        // var claims = new List<Claim>
        // {
        //     new Claim(ClaimTypes.Email, user.Email),
        //     new Claim(ClaimTypes.NameIdentifier, user.Id)
        // };
        List<Claim> claims =
        [
            new Claim(ClaimTypes.Email, user.Email),
            new Claim(ClaimTypes.NameIdentifier, user.Id)
        ];

        SigningCredentials creds = new(key, SecurityAlgorithms.HmacSha512Signature);

        SecurityTokenDescriptor tokenDescriptor = new()
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddDays(7),
            SigningCredentials = creds
        };

        JwtSecurityTokenHandler tokenHandler = new();
        var token = tokenHandler.CreateToken(tokenDescriptor);

        return tokenHandler.WriteToken(token);
    }
}
