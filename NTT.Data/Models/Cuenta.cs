using System;
using System.Collections.Generic;

#nullable disable

namespace NTT.Entities.Models
{
    public partial class Cuenta
    {
        public Cuenta()
        {
            CuentaMovimientos = new HashSet<CuentaMovimiento>();
        }

        public int CuentaId { get; set; }
        public string NumeroCuenta { get; set; }
        public string TipoCuenta { get; set; }
        public decimal SaldoInicial { get; set; }
        public bool Estado { get; set; }
        public int ClienteId { get; set; }

        public virtual Cliente Cliente { get; set; }
        public virtual ICollection<CuentaMovimiento> CuentaMovimientos { get; set; }
    }
}
