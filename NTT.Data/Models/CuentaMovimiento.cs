using System;
using System.Collections.Generic;

#nullable disable

namespace NTT.Entities.Models
{
    public partial class CuentaMovimiento
    {
        public int CuentaMovimientoId { get; set; }
        public int MovimientoId { get; set; }
        public int CuentaId { get; set; }
        public decimal? Saldo { get; set; }
        public decimal? Movimiento { get; set; }

        public virtual Cuenta Cuenta { get; set; }
        public virtual Movimiento MovimientoNavigation { get; set; }
    }
}
