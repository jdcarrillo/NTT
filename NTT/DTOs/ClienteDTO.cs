using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace NTT.WebApi.DTOs
{
    public class ClienteDTO
    {
        public ClienteDTO()
        {
            Cuenta = new HashSet<CuentaDTO>();
        }

        public int ClienteId { get; set; }
        public string Contrasena { get; set; }
        public bool Estado { get; set; }
        public int PersonaId { get; set; }
        [NotMapped]
        [JsonIgnore]
        public virtual PersonaDTO Persona { get; set; }
        [NotMapped]
        [JsonIgnore]
        public virtual ICollection<CuentaDTO> Cuenta { get; set; }
    }
}
