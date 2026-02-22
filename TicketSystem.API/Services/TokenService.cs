using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TicketSystem.Aplicacion.Interfaces;
using TicketSystem.Dominio.Entidades;

public class TokenService : ITokenService
{
    private readonly IConfiguration _configuration;

    public TokenService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public string GenerarToken(Usuario usuario)
    {
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, usuario.Id.ToString()),
            new Claim(ClaimTypes.Email, usuario.Email),
            new Claim(ClaimTypes.Role, usuario.Rol.ToString()) // 🔥 CLAVE
        };

        var jwtKey = _configuration["Jwt:Key"] ?? "a_very_long_secret_key_that_is_at_least_32_characters_long";
        if (jwtKey.Length < 32) jwtKey = jwtKey.PadRight(32, '0');
        
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey));

        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            claims: claims,
            expires: DateTime.UtcNow.AddHours(4),
            signingCredentials: creds
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}