using System.Collections.Generic;

namespace NTT.WebApi.DTOs
{
    public class ReporteDTO
    {
        public ReporteDTO()
        {
            Cuenta = new HashSet<CuentaDTO>();
            Movimiento = new HashSet<MovimientoDTO>();
        }


        public virtual PersonaDTO Persona { get; set; }
        public virtual ICollection<CuentaDTO> Cuenta { get; set; }
        public virtual ICollection<MovimientoDTO> Movimiento { get; set; }

    }
}
