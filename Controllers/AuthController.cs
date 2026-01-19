using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TrilhaApiDesafio.Context;
using TrilhaApiDesafio.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace TrilhaApiDesafio.Controllers;

/// <summary>
/// Controller responsável pela autenticação na API
/// </summary>
[ApiController]
[Route("auth")]
public class AuthController : ControllerBase
{
    private readonly OrganizadorContext _context;
    private readonly IConfiguration _configuration;    
    private readonly PasswordHasher<Usuario> _hasher = new();

    /// <summary>
    /// Construtor do AuthController
    /// </summary>
    /// <param name="context"></param>
    /// <param name="configuration"></param>
    public AuthController(OrganizadorContext context, IConfiguration configuration)
    {
        _context = context;
        _configuration = configuration;
    }

    /// <summary>
    /// Realiza login na API  e retorn um TOKEN de autenticação válido. (OBRIGATÓRIO GERAR PARA PODER ACESSAR OS ENDPOINTS PROTEGIDOS)
    /// </summary>
    /// <remarks>
    /// <b>Usuário padrão para testes:</b>
    /// <br/>Usuário: <c>admin</c>
    /// <br/>Senha: <c>123456</c><br/>
    /// <b>Como usar o token:</b><br/>
    /// 1 - Clique no botão <b>Authorize</b> no topo do Swagger<br/>
    /// 2 - Digite: <c>Bearer {seu token}</c><br/>
    /// 3 - Clique em <b>Authorize</b> e depois em <b>Close</b>
    /// </remarks>
    /// <returns>Token de autenticação</returns>
    [AllowAnonymous]
    [HttpPost("login")]
    public IActionResult Login([FromBody] AuthLoginRequest request)
    {
        var usuario = _context.Usuarios
            .FirstOrDefault(u => u.Username == request.Username);

        if (usuario == null)
            return Unauthorized("Usuário ou senha inválidos");
        
        var result = _hasher.VerifyHashedPassword(
            usuario,
            usuario.PasswordHash,
            request.Password
        );

        if (result == PasswordVerificationResult.Failed)
            return Unauthorized("Usuário ou senha inválidos");        
        
        var token = GerarToken(usuario);

        return Ok(new { token });
    }

    private string GerarToken(Usuario usuario)
    {
        var jwt = _configuration.GetSection("Jwt");
        var key = Encoding.ASCII.GetBytes(jwt["Key"]);

        var claims = new[]
        {
            new Claim(ClaimTypes.Name, usuario.Username),
            new Claim(ClaimTypes.NameIdentifier, usuario.Id.ToString())
        };

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddHours(2),
            Issuer = jwt["Issuer"],
            Audience = jwt["Audience"],
            SigningCredentials = new SigningCredentials(
                new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha256Signature
            )
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescriptor);

        return tokenHandler.WriteToken(token);
    }
}