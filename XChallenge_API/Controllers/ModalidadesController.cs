using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using XChallenge_API.Contexts;

namespace XChallenge_API.Controllers
{
    [Route("api/modalidades")]
    [Produces("application/json")]
    [ApiController]
    public class ModalidadesController : ControllerBase
    {
        private readonly MainContext _ctx;
        
        public ModalidadesController(MainContext ctx)
        {
            _ctx = ctx;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_ctx.Modalidades.OrderBy(m=>m.IdModalidade).ToList());
        }

        [HttpPut]
        public IActionResult ProcurarPorNome([FromBody] string pesquisa)
        {
            var modalidadesLista = _ctx.Modalidades.Where(m => m.NomeModalidade.ToLower().StartsWith(pesquisa.ToLower())).ToList();

            return Ok(modalidadesLista);
        }
    }
}
