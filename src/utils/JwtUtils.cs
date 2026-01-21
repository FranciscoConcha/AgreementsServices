using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using src.model;

namespace src.utils;

public class JwtUtils{

    private readonly IConfiguration _config;
    
    public JwtUtils(IConfiguration config)
    {
        _config = config;
    }

    public string CreateToken(UserModel user)
    {
        var claims  = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Email, user.Email),
            new Claim(ClaimTypes.Name, user.Name),
            new Claim(ClaimTypes.Role, user.Rol.Name),
            new Claim("Charge", user.Charge)
        };

        var keyString  = _config.GetSection("AppSettings:Token").Value ?? throw new Exception("JWT no encontrada");

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(keyString));

        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);
        var tokenDescription = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.Now.AddDays(1),
            SigningCredentials = credentials
        };

        var tokenHandler = new System.IdentityModel.Tokens.Jwt.JwtSecurityTokenHandler();
        var token =tokenHandler.CreateToken(tokenDescription);

        return tokenHandler.WriteToken(token);
    }

}
