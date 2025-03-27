using System.Reflection.Metadata.Ecma335;
using Microsoft.AspNetCore.Mvc;
using XChallenge_API.Contexts;
using XChallenge_API.Models;
using XChallenge_API.Services;

namespace XChallenge_API.Controllers
{
    public class LoginModel()
    {
        public string Email { get; set; }
        public string Senha { get; set; }
    }

    [Route("api/login")]
    [Produces("application/json")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly MainContext _ctx;
        private readonly TokenService _tokenService;
        
        public LoginController(MainContext ctx, TokenService tc)
        {
            _ctx = ctx;
            _tokenService = tc;
        }

        [HttpPost]
        public IActionResult Login([FromBody] LoginModel login)
        {
            var user = _ctx.Acessos.FirstOrDefault(u => u.Email == login.Email);
            if (user == null)
            {
                return BadRequest(new { message = "Email ou senha inválidos." });
            }

            bool validPassword = user.SenhaAcesso == login.Senha;

            if (!validPassword)
            {
                return BadRequest(new { message = "Email ou senha inválidos." });
            }

            var token = _tokenService.TokenGenerate(user);

            var novoAcesso = new LogAcesso
            {
                Idusuario = user.IdAcesso,
                DataHoraAcesso = DateTime.Now,
                DataHoraSaida = null
            };

            _ctx.LogAcessos.Add(novoAcesso);
            _ctx.SaveChanges();

            return Ok(new
            {
                Token = token,
                usuario = new
                {
                    Id = user.IdAcesso,
                    Email = user.Email ?? "NULL",
                    Senha = user.SenhaAcesso ?? "NULL",
                },
                horarioAcesso = DateTime.Now.ToString("dd/MM/yyyy - HH:mm"),
            });
        }
    }
}
