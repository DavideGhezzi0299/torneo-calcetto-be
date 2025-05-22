using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using torneo_calcetto.EF.Context;
using torneo_calcetto.EF.Migrations;
using torneo_calcetto.EF.Models;
using torneo_calcetto_be.Core.Models;
using torneo_calcetto_be.Core.Utility;

namespace torneo_calcetto_be.Core.Repository
{
    public class TorneoRepository
    {
        private readonly TorneoCalcettoContext _context;
        public TorneoRepository(TorneoCalcettoContext context) {
        
          _context = context;
        }

        public async Task<List<Torneo>> PrendiTorneiPerUtente(int idUtente)
        {
            try
            {
                var tornei = new List<Torneo>();
                if (idUtente != 0)
                    tornei = await _context.Tornei.Where(x => x.FkIdUtenteCreatore == idUtente).ToListAsync();
                else
                    tornei = await _context.Tornei.ToListAsync();
                return tornei;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<bool> CreaTorneo(NuovoTorneoDto torneo)
        {
            try
            {
                var utente = await _context.Utenti.Where(x => x.IdPk == 1).FirstOrDefaultAsync();
                if (utente == null)
                {
                    return false;
                }
                var nuovoTorneo = TorneoMapper.DtoToEntity(torneo);
                nuovoTorneo.UtenteNavigation = utente;
                await _context.Tornei.AddAsync( nuovoTorneo );
                await _context.SaveChangesAsync();
                return true;

            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<Torneo> PrendiTorneoPerId(int idTorneo)
        {
            try
            {
                 var torneo = await _context.Tornei
                                        .Include(x =>x.Gironi)
                                            .ThenInclude(g => g.Squadre)
                                        .FirstOrDefaultAsync(x => x.IdPk == idTorneo);
                return torneo;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<bool> AggiornaTorneo(int idTorneo, List<GironeDto> gironi)
        {
            try
            {
                var torneo = await _context.Tornei.FirstOrDefaultAsync(x => x.IdPk == idTorneo);
                if ( torneo == null )
                    return false;

                var newGironi = new List<Girone>();
                foreach ( var girone in gironi)
                {
                    newGironi.Add(GironeMapper.GironeDtoToEntity(girone));
                }
                await _context.Gironi.AddRangeAsync(newGironi);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<bool> EliminaTorneo(int idTorneo)
        {
            try
            {

                var torneo = await _context.Tornei.FirstOrDefaultAsync(x => x.IdPk == idTorneo);
                if ( torneo == null )
                    return false;

                _context.Tornei.Remove(torneo);
                
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<Torneo> SorteggiaGironi(int idTorneo, AggiornaTorneoDto aggiornaTorneoDto)
        {
            try
            {

                var torneo = await _context.Tornei.FirstOrDefaultAsync(x => x.IdPk == idTorneo);
                if (torneo == null)
                    return null;

                return torneo;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
