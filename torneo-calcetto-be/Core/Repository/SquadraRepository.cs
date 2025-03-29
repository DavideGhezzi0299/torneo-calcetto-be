using Microsoft.EntityFrameworkCore;
using torneo_calcetto.EF.Context;
using torneo_calcetto.EF.Models;

namespace torneo_calcetto_be.Core.Repository
{
    public class SquadraRepository
    {
        private readonly TorneoCalcettoContext _context;
        public SquadraRepository(TorneoCalcettoContext context) 
        {
            _context = context;
        }
        public async Task<List<Squadra>> GetSquadre(bool classifica = false)
        {
            if(classifica)
            {
                return await _context.Squadre
                            .OrderByDescending(x => x.PuntiFatti)  // Ordina per punti in modo decrescente
                            .ThenByDescending(x => x.DifferenzaReti)
                            .ThenByDescending(x => x.GolFatti)// In caso di pareggio, ordina per differenza reti decrescente
                            .ToListAsync();
            }
            return await _context.Squadre.ToListAsync();
        }
        public async Task Reset()
        {
            var squadre = await _context.Squadre.ToListAsync();
            foreach (var item in squadre)
            {
                item.Sconfitte = 0;
                item.Vittorie = 0;
                item.Pareggi = 0;
                item.PuntiFatti = 0;
                item.GolFatti = 0;
                item.GolSubiti = 0;
                item.PartiteGiocate = 0;
                item.DifferenzaReti = 0;
            }
            await _context.SaveChangesAsync();
        }
    }
}
