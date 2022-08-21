using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace NTT.WebApi.DTOs
{
    public class CuentaMovimientoDTO
    {
        public int CuentaMovimientoId { get; set; }
        public int MovimientoId { get; set; }
        public int CuentaId { get; set; }
        public decimal? Saldo { get; set; }
        public decimal? Movimiento { get; set; }
        [NotMapped]
        [JsonIgnore]
        public virtual CuentaDTO Cuenta { get; set; }
        [NotMapped]
        [JsonIgnore]
        public virtual MovimientoDTO MovimientoNavigation { get; set; }
    }
}