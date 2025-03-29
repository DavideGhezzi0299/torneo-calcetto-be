using Microsoft.AspNetCore.Mvc;
using torneo_calcetto.EF.Models;
using torneo_calcetto_be.Core.Repository;

namespace torneo_calcetto_be.Core.Controllers
{
    [ApiController]
    [Route("api/partite")]
    public class PartiteController : ControllerBase
    {
        private readonly PartiteRepository _partiteRepository;

        public PartiteController(PartiteRepository partiteRepository)
        {
            _partiteRepository = partiteRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetPartite()
        {
            var partite = await _partiteRepository.GetPartite();
            return Ok(partite);
        }

        [HttpPost("calcolaCalendario")]
        public async Task<IActionResult> CalcolaCalendario([FromBody] Squadra squadra)
        {
            var partite = await _partiteRepository.GeneraCalendarioGirone();
            await _partiteRepository.CalcolaGiornate();
            return Ok(partite);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAvversariPerSquadra(int id)
        {
            var partita = await _partiteRepository.GetAvversariPerSquadra(id);
            return Ok(partita);
        }

        [HttpGet("{squadra}/{avversario}")]
        public async Task<IActionResult> GetPartitaFromNomiSquadra(string squadra, string avversario)
        {
            var partita = await _partiteRepository.GetPartitaFromNomiSquadra(squadra, avversario);
            return Ok(partita);
        }

        [HttpPost("inserisciRisultato")]
        public async Task<IActionResult> InserisciRisultato([FromBody] Partita partita)
        {
            var partitaAggiornata = new Partita();
            if (partita.FaseEliminatoria)
                partitaAggiornata = await _partiteRepository.InserisciRisultatoFaseEliminatoria(partita);
            else
                if(partita.Modifica)
                partitaAggiornata = await _partiteRepository.ModificaRisultato(partita);
                else
                    partitaAggiornata = await _partiteRepository.InserisciRisultato(partita);
            return Ok(partitaAggiornata);
        }

        [HttpGet("terminaFaseGironi")]
        public async Task<IActionResult> TerminaFaseGironi()
        {
            await _partiteRepository.TerminaFaseGironi();
            return Ok();
        }
        [HttpGet("faseEliminatoria/{idSquadraCasa}/{idSquadraOspite}")]
        public async Task<IActionResult> FaseEliminatoria(int idSquadraCasa, int idSquadraOspite)
        {
            var partita = await _partiteRepository.FaseEliminatoria(idSquadraCasa, idSquadraOspite);
            return Ok(partita);
        }
        [HttpGet("reset")]
        public async Task<IActionResult> Reset()
        {
            await _partiteRepository.Reset();
            return Ok();
        }
    }
}
