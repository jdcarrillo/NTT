using System;
using System.Collections.Generic;

#nullable disable

namespace NTT.Entities.Models
{
    public partial class Persona
    {
        public Persona()
        {
            Clientes = new HashSet<Cliente>();
        }

        public int PersonaId { get; set; }
        public string Nombre { get; set; }
        public string Genero { get; set; }
        public byte Edad { get; set; }
        public string Identificacion { get; set; }
        public string Direccion { get; set; }
        public string Telefono { get; set; }

        public virtual ICollection<Cliente> Clientes { get; set; }
    }
}
