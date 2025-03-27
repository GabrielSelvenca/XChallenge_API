using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using XChallenge_API.Contexts;

namespace XChallenge_API.Controllers
{
    [Route("api/skills")]
    [ApiController]
    public class SkillsControllers : ControllerBase
    {
        private readonly MainContext _ctx;

        public SkillsControllers(MainContext ctx)
        {
            _ctx = ctx;
        }
    }
}
