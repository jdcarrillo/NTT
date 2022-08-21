using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace NTT.WebApi.DTOs
{
    public class MovimientoDTO
    {
        public MovimientoDTO()
        {
            CuentaMovimientos = new HashSet<CuentaMovimientoDTO>();
        }

        public int MovimientoId { get; set; }
        public int CuentaId { get; set; }
        public DateTime Fecha { get; set; }
        public string TipoMovimiento { get; set; }
        public bool Estado { get; set; }
        public decimal Saldo { get; set; }
        [NotMapped]
        [JsonIgnore]
        public virtual ICollection<CuentaMovimientoDTO> CuentaMovimientos { get; set; }
    }
}
