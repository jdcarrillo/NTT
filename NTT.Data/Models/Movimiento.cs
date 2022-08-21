using System;
using System.Collections.Generic;

#nullable disable

namespace NTT.Entities.Models
{
    public partial class Movimiento
    {
        public Movimiento()
        {
            CuentaMovimientos = new HashSet<CuentaMovimiento>();
        }

        public int MovimientoId { get; set; }
        public int CuentaId { get; set; }
        public DateTime Fecha { get; set; }
        public string TipoMovimiento { get; set; }
        public bool Estado { get; set; }
        public decimal Saldo { get; set; }

        public virtual ICollection<CuentaMovimiento> CuentaMovimientos { get; set; }
    }
}
