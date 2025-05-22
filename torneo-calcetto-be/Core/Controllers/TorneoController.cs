using Microsoft.AspNetCore.Mvc;
using torneo_calcetto.EF.Models;
using torneo_calcetto_be.Core.Models;
using torneo_calcetto_be.Core.Repository;

namespace torneo_calcetto_be.Core.Controllers
{
    [ApiController]
    [Route("api/tornei")]
    public class TorneoController : ControllerBase
    {
        private readonly TorneoRepository _torneoRepository;

        public TorneoController(TorneoRepository torneoRepository)
        {
            _torneoRepository = torneoRepository;
        }

        [HttpPost]
        public async Task<IActionResult> CreaTorneo(NuovoTorneoDto torneo)
        {
            try
            {
                var esito = await _torneoRepository.CreaTorneo(torneo);
                if(!esito)
                  return Problem("Creazione torneo non avvenuta correttamente");

                return Ok(esito);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        [HttpGet("utente/{idUtente}")]
        public async Task<IActionResult> PrendiTorneiPerUtente(int idUtente)
        {
            try
            {
                var tornei = await _torneoRepository.PrendiTorneiPerUtente(idUtente);
                if (tornei == null)
                    return Problem("Non ci sono tornei per l'utente!");
                return Ok(tornei);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> PrendiTorneoPerId(int id)
        {
            try
            {
                var torneo = await _torneoRepository.PrendiTorneoPerId(id);
                if (torneo == null)
                    return Problem("Torneo non trovato");
                return Ok(torneo);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        [HttpPatch("{idTorneo}")]
        public async Task<IActionResult> AggiornaTorneo(int idTorneo, List<GironeDto> gironi)
        {
            try
            {
                if (!await _torneoRepository.AggiornaTorneo(idTorneo, gironi))
                    return Problem("Errore durante l'aggiornamento del torneo");
                return Ok(true);
            }
            catch (Exception ex)
            {
                throw;
            }

        }

        [HttpDelete("{idTorneo}")]
        public async Task<IActionResult> EliminaTorneo(int idTorneo)
        {
            try
            {
                if (!await _torneoRepository.EliminaTorneo(idTorneo))
                    return Problem("Errore durante l'eliminazione del torneo");
                return Ok(true);
            }
            catch (Exception ex)
            {
                throw;
            }

        }

        [HttpPost("{idTorneo}")]
        public async Task<IActionResult> SorteggiaGironi(int idTorneo, AggiornaTorneoDto aggiornaTorneoDto)
        {
            try
            {
                var torneo = await _torneoRepository.SorteggiaGironi(idTorneo, aggiornaTorneoDto);
                if (torneo == null)
                    return Problem("Errore durante il sorteggio dei gironi");
                return Ok(torneo);
            }catch(Exception e)
            {
                throw;
            }
        }
    }
}
