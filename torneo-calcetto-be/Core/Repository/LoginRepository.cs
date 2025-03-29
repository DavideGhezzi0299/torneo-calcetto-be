using Microsoft.EntityFrameworkCore;
using torneo_calcetto.EF.Context;
using torneo_calcetto.EF.Models;
using torneo_calcetto_be.Core.Utility;

namespace torneo_calcetto_be.Core.Repository
{
    public class LoginRepository
    {
        private readonly TorneoCalcettoContext _context;
        public LoginRepository(TorneoCalcettoContext context) {
            _context = context; 
        }

        public async Task<bool> RegistraUtente(string username, string password)
        {
            try
            {
                var utenti = await _context.Utenti.Where(x => x.Username.Trim().ToLower() == username).FirstOrDefaultAsync();
                if (utenti is null)
                {
                    return false;
                }

                var nuovoUtente = new Utente
                {
                    Username = username,
                    Password = PasswordHasher.HashPassword(password),
                    Email = null
                };

                _context.Utenti.Add(nuovoUtente);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex) { 
               throw;
            }
        }

        public async Task<bool> Login(string username, string password)
        {
            try
            {
                var utente = await _context.Utenti.Where(x => x.Username.Trim().ToLower() == username).FirstOrDefaultAsync();
                if (utente is null)
                {
                    return false;
                }

                return PasswordHasher.VerifyPassword(password, utente.Password);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
