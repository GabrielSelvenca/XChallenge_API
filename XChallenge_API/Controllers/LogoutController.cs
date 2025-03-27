using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using XChallenge_API.Contexts;
using XChallenge_API.Models;

namespace XChallenge_API.Controllers
{
    [Route("api/logout")]
    [ApiController]
    public class LogoutController : ControllerBase
    {
        private readonly MainContext _ctx;

        public LogoutController(MainContext ctx)
        {
            _ctx = ctx;
        }

        [HttpGet]
        [Authorize]
        public IActionResult Get()
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            if (userIdClaim == null)
            {
                return NotFound("Não foi posssível achar o usuário para salvar o logout");
            }

            var userLogAcesso = _ctx.LogAcessos.FirstOrDefault(ul=>ul.Idusuario.ToString() == userIdClaim);

            if (userLogAcesso == null)
            {
                return NotFound("Não foi posssível achar o usuário para salvar o logout");
            }

            var newContent = new LogAcesso
            {
                Idusuario = userLogAcesso.Idusuario,
                DataHoraAcesso = userLogAcesso.DataHoraAcesso,
                DataHoraSaida = DateTime.Now,
            };

            _ctx.LogAcessos.Add(newContent);
            _ctx.SaveChanges();

            return Ok();
        }
    }
}
