using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace NTT.WebApi.DTOs
{
    public class CuentaDTO
    {
        public CuentaDTO()
        {
            CuentaMovimientos = new HashSet<CuentaMovimientoDTO>();
        }

        public int CuentaId { get; set; }
        public string NumeroCuenta { get; set; }
        public string TipoCuenta { get; set; }
        public decimal SaldoInicial { get; set; }
        public bool Estado { get; set; }
        public int ClienteId { get; set; }
        [NotMapped]
        [JsonIgnore]
        public virtual ClienteDTO Cliente { get; set; }
        [NotMapped]
        [JsonIgnore]
        public virtual ICollection<CuentaMovimientoDTO> CuentaMovimientos { get; set; }
    }
}
