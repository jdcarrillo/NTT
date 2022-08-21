using NTT.Entities.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace NTT.WebApi.DTOs
{
    public class PersonaDTO
    {

        public PersonaDTO()
        {
            Clientes = new HashSet<ClienteDTO>();
        }

        public int PersonaId { get; set; }
        public string Nombre { get; set; }
        public string Genero { get; set; }
        public byte Edad { get; set; }
        public string Identificacion { get; set; }
        public string Direccion { get; set; }
        public string Telefono { get; set; }
        [NotMapped]
        [JsonIgnore]
        public virtual ICollection<ClienteDTO> Clientes { get; set; }
    }
}
