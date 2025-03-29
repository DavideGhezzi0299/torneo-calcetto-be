using Microsoft.AspNetCore.Mvc;
using torneo_calcetto_be.Core.Repository;

namespace torneo_calcetto_be.Core.Controllers
{
    [ApiController]
    [Route("api/squadre")]
    public class SquadraController : ControllerBase
    {
        private readonly SquadraRepository _squadraRepository;
        public SquadraController(SquadraRepository squadraRepository)
        {
            _squadraRepository = squadraRepository;
        }
        [HttpGet("{classifica}")]
        public async Task<IActionResult> GetSquadre(bool classifica = false)
        {
            return Ok(await _squadraRepository.GetSquadre(classifica));
        }
        [HttpGet("reset")]
        public async Task<IActionResult> Reset()
        {
            await _squadraRepository.Reset();
            return Ok();
        }
    }
}
