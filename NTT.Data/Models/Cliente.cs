using System;
using System.Collections.Generic;

#nullable disable

namespace NTT.Entities.Models
{
    public partial class Cliente
    {
        public Cliente()
        {
            Cuenta = new HashSet<Cuenta>();
        }

        public int ClienteId { get; set; }
        public string Contrasena { get; set; }
        public bool Estado { get; set; }
        public int PersonaId { get; set; }

        public virtual Persona Persona { get; set; }
        public virtual ICollection<Cuenta> Cuenta { get; set; }
    }
}
