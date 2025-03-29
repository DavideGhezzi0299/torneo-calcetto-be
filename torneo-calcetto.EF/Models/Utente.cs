using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace torneo_calcetto.EF.Models
{
    public class Utente
    {
        [Key]
        public int IdPk { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string? Email { get; set; }

    }
}
