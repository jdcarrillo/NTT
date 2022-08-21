using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using NTT.Entities.DbContexts;
using NTT.Entities.Models;
using NTT.Interfaces;
using NTT.Util.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlTypes;
using System.Globalization;

namespace NTT.Repository
{
    public class ReporteRepository : IReporteRepository
    {
        private readonly ILogger<ReporteRepository> _logger;
        private NTTContext _context;

        public ReporteRepository(NTTContext context, ILogger<ReporteRepository> logger)
        {
            _context = context;
            _logger = logger;
        }


        public async Task<dynamic> FindBy(string valor)
        {
            dynamic query;
            try
            {
                query = movimientosCuentaRpt(valor);

            }
            catch (Exception ex)
            {
                throw;
            }
            return await Task.Run(() => query);

        }
        public async Task<dynamic> FindAll()
        {
            dynamic query;
            try
            {
                query = movimientosCuentaRpt("");

            }
            catch (Exception ex)
            {
                throw;
            }
            return await Task.Run(() => query);


        }
        public async Task<dynamic> FindByDate(string fechaInicio, string FechaFin)
        {
            dynamic query;
            try
            {
                query = movimientosCuentaRpt(fechaInicio, FechaFin);
                
            }
            catch (Exception ex)
            {
                throw;
            }
            return await Task.Run(() => query);

        }
        #region Private
        private dynamic movimientosCuentaRpt(string valor = "")
        {
            dynamic query;
            decimal numericValue;

            bool isNumber = decimal.TryParse(valor, out numericValue);
            dynamic valorBuqueda;
            if (isNumber)
            {
                valorBuqueda = numericValue;
                var busqueda = _context.Cuenta.Where(w => w.NumeroCuenta == valor).FirstOrDefault();
                if (busqueda == null)
                    throw new AppException($"No existe registros de {valor}");
            }
            else
            {
                valorBuqueda = valor;
                var busqueda = _context.Personas.Where(w => w.Nombre.ToLower().Contains(valor)).FirstOrDefault();
                if (busqueda == null)
                    throw new AppException($"No existe registros de {valor}");
            }

            query =
               from per in _context.Personas
               join cli in _context.Clientes on per.PersonaId equals cli.PersonaId into perCliGroup
               from pcg in perCliGroup.DefaultIfEmpty()
               join cuen in _context.Cuenta on pcg.ClienteId equals cuen.ClienteId into cuenCliGroup
               from ccg in cuenCliGroup.DefaultIfEmpty()
               join mov in _context.Movimientos on ccg.CuentaId equals mov.CuentaId into movCliGroup
               from mcg in movCliGroup.DefaultIfEmpty()
               join mcu in _context.CuentaMovimientos on mcg.MovimientoId equals mcu.MovimientoId into movCuenGroup
               from mcueng in movCuenGroup.DefaultIfEmpty()
               where mcueng.Saldo != null &&
                ((isNumber == true) ? ccg.NumeroCuenta == valor : (string.IsNullOrEmpty(valor) ? per.Nombre.ToLower().Contains(valor) : 1 == 1))
               select new
               {
                   Fecha = mcg.Fecha,
                   Cliente = per.Nombre,
                   Numero_Cuenta = ccg.NumeroCuenta,
                   Tipo = ccg.TipoCuenta,
                   Saldo_Inicial = ccg.SaldoInicial,
                   Estado = ccg.Estado,
                   Movimiento = mcueng.Movimiento,
                   Saldo_Disponible = mcueng.Saldo
               };

            return query;
        }

        /// <summary>
        /// Busqueda por fecha
        /// </summary>
        /// <param name="fInicio"></param>
        /// <param name="fFin"></param>
        /// <returns></returns>
        private dynamic movimientosCuentaRpt(string fInicio = "", string fFin = "")
        {
            dynamic query;

            IFormatProvider culture = new CultureInfo("en-EC", true);
            DateTime fechaInicio = DateTime.ParseExact(fInicio, "dd/MM/yyyy", culture);
            DateTime fechaFin = DateTime.ParseExact(fFin, "dd/MM/yyyy", culture);


            query =
                   from per in _context.Personas
                   join cli in _context.Clientes on per.PersonaId equals cli.PersonaId into perCliGroup
                   from pcg in perCliGroup.DefaultIfEmpty()
                   join cuen in _context.Cuenta on pcg.ClienteId equals cuen.ClienteId into cuenCliGroup
                   from ccg in cuenCliGroup.DefaultIfEmpty()
                   join mov in _context.Movimientos on ccg.CuentaId equals mov.CuentaId into movCliGroup
                   from mcg in movCliGroup.DefaultIfEmpty()
                   join mcu in _context.CuentaMovimientos on mcg.MovimientoId equals mcu.MovimientoId into movCuenGroup
                   from mcueng in movCuenGroup.DefaultIfEmpty()
                   where mcueng.Saldo != null &&
                   (mcg.Fecha >= fechaInicio && mcg.Fecha <= fechaFin)

                   select new
                   {
                       Fecha = mcg.Fecha,
                       Cliente = per.Nombre,
                       Numero_Cuenta = ccg.NumeroCuenta,
                       Tipo = ccg.TipoCuenta,
                       Saldo_Inicial = ccg.SaldoInicial,
                       Estado = ccg.Estado,
                       Movimiento = mcueng.Movimiento,
                       Saldo_Disponible = mcueng.Saldo
                   };

            return query;
        }
        #endregion
    }
}
