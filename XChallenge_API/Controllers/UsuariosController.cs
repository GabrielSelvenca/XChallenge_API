using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using XChallenge_API.Contexts;
using XChallenge_API.Models;
using XChallenge_API.Services;

namespace XChallenge_API.Controllers
{
    [Route("api/usuario")]
    [Produces("application/json")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        private readonly MainContext _ctx;
        private readonly TokenService _tokenService;
        public UsuariosController(MainContext ctx, TokenService tc)
        {
            _ctx = ctx;
            _tokenService = tc;
        }

        [Authorize]
        [HttpPost("verify")]
        public IActionResult VerificarToken()
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            if (userIdClaim == null)
            {
                return Unauthorized("Token inválido");
            }

            var user = _ctx.Acessos.FirstOrDefault(u=> u.IdAcesso.ToString() == userIdClaim);

            if (user == null)
            {
                return Unauthorized("Usuário não encontrado");
            }

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
                user.IdAcesso,
                user.Nome,
                user.Email,
                novoAcesso
            });
        }

        [HttpPut("{id}")]
        public IActionResult AlterarUsuario(int id, [FromBody] string Nome, string Email, string Senha)
        {
            var user = _ctx.Acessos.FirstOrDefault(u=>u.IdAcesso == id);
            if (user == null)
            {
                return NotFound("Usuário não encontrado");
            }

            if (string.IsNullOrEmpty(Email)) { Email = user.Email ?? "NULL"; }
            if (string.IsNullOrEmpty(Nome)) { Nome = user.Nome ?? "NULL"; }
            if (string.IsNullOrEmpty(Senha)) { Senha = user.SenhaAcesso ?? "NULL"; }


            var newContent = new Acesso
            {
                IdAcesso = id,
                Email = Email,
                SenhaAcesso = Senha,
                Nome = Nome
            };

            return Ok(new
            {
                user.IdAcesso,
                user.Nome,
                user.Email,
                newContent
            });
        }
    }
}
