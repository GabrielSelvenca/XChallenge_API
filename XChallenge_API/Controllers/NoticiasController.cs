using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using XChallenge_API.Contexts;

namespace XChallenge_API.Controllers
{
    [Route("api/noticias")]
    [Produces("application/json")]
    [ApiController]
    public class NoticiasController : ControllerBase
    {
        private readonly MainContext _ctx;

        public NoticiasController(MainContext ctx) { _ctx = ctx; }

        [HttpGet]
        public IActionResult Get()
        {
            var listaNoticia = _ctx.Noticia.ToList();

            var listaOrdenada = listaNoticia.OrderByDescending(l=>l.Data);

            return Ok(listaOrdenada);
        }
    }
}
