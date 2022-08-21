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
using System.Transactions;

namespace NTT.Repository
{
    public class MovimientoRepository : IMovimientoRepostitory
    {

        private readonly ILogger<MovimientoRepository> _logger;
        private NTTContext _context;

        public MovimientoRepository(NTTContext context, ILogger<MovimientoRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<Movimiento> FindById(int id)
        {
            Movimiento data = new Movimiento();
            try
            {
                data = await _context.Movimientos.FindAsync(id);


            }
            catch (Exception ex)
            {
                throw;
            }
            return await Task.Run(() => data);

        }

        public async Task<List<Movimiento>> FindAll()
        {
            List<Movimiento> data = new List<Movimiento>();
            try
            {
                data = _context.Movimientos
                                //.Include(i => i.Cliente)
                                .ToList();

            }
            catch (Exception ex)
            {
                throw;
            }
            return await Task.Run(() => data);

        }

        public async Task<List<Movimiento>> FindByName(string name)
        {
            List<Movimiento> data = new List<Movimiento>();
            try
            {
                data = _context.Movimientos
                                //.Include(i => i.Persona)
                                //.Where(a => a.Persona.Nombre.ToString().ToLower().Contains(name.ToLower()))
                                .ToList();

            }
            catch (Exception ex)
            {
                throw;
            }
            return await Task.Run(() => data);

        }

        public async Task<Movimiento> Create(Movimiento entity)
        {
            string[] tiposMovimientoDescr = { "depósito", "deposito" };

            using (var tx = await _context.Database.BeginTransactionAsync())
            {
                try
                {

                    var movimientoDescripcion = tiposMovimientoDescr.AsQueryable().Contains(entity.TipoMovimiento.ToLower().Trim()) ? "Depósito" : (entity.TipoMovimiento.ToLower().Trim().Contains("retiro")) ? $"Retiro" : null;
                    if (movimientoDescripcion == null)
                        throw new AppException("No ingresó el tipo de movimiento: Retiro o Depósito");

                    var transaccion = entity.Saldo;
                    if (movimientoDescripcion == "Retiro")
                        transaccion = -transaccion;

                    using (var contexts = _context)
                    {
                        //Actualiza el Saldo de la cuenta
                        var resultCuenta = contexts.Cuenta.Where(w => w.CuentaId == entity.CuentaId).FirstOrDefault();
                        if (movimientoDescripcion == "Retiro" && (resultCuenta.SaldoInicial == 0 || resultCuenta.SaldoInicial < entity.Saldo))
                        {
                            throw new AppException("Saldo no disponible");
                        }

                        var saldoTotal = resultCuenta.SaldoInicial + (tiposMovimientoDescr.AsQueryable().Contains(movimientoDescripcion.ToLower()) ? transaccion : (movimientoDescripcion.ToLower().Contains("retiro")) ? transaccion : 0);

                        resultCuenta.SaldoInicial = saldoTotal;
                        contexts.Cuenta.Update(resultCuenta);


                        //Inserta Movimiento
                        Movimiento movimiento = new Movimiento()
                        {
                            CuentaId = entity.CuentaId,
                            Fecha = DateTime.Now,
                            TipoMovimiento = movimientoDescripcion,
                            Estado = true,
                            Saldo = transaccion,
                        };
                        await contexts.Movimientos.AddAsync(movimiento);
                        await contexts.SaveChangesAsync();
                        int movimientoId = movimiento.MovimientoId;

                        //Inserta Cuenta Movimiento
                        CuentaMovimiento cuentaMovimiento = new CuentaMovimiento()
                        {
                            CuentaId = entity.CuentaId,
                            MovimientoId = movimientoId,
                            Saldo = saldoTotal,
                            Movimiento = transaccion,
                        };

                        await contexts.CuentaMovimientos.AddAsync(cuentaMovimiento);

                        await contexts.SaveChangesAsync()
                            .ConfigureAwait(false);
                    }
                    await tx.CommitAsync();



                }
                catch (Exception ex)
                {
                    await tx.RollbackAsync();
                    //throw new Exception($"Ocurrió un error {ex.Message}");
                    throw;
                    entity = null;
                }
            }
            return entity;

        }


        public async Task<Movimiento> Update(Movimiento entity)
        {
            string[] tiposMovimientoDescr = { "depósito", "deposito" };

            using (var tx = await _context.Database.BeginTransactionAsync())
            {
                try
                {

                    var movimientoDescripcion = tiposMovimientoDescr.AsQueryable().Contains(entity.TipoMovimiento.ToLower().Trim()) ? "Depósito" : (entity.TipoMovimiento.ToLower().Trim().Contains("retiro")) ? $"Retiro" : null;
                    if (movimientoDescripcion == null)
                        throw new AppException("No ingresó el tipo de movimiento: Retiro o Depósito");

                    var transaccion = entity.Saldo;
                    if (movimientoDescripcion == "Retiro")
                        transaccion = -transaccion;

                    Movimiento movimiento = new Movimiento()
                    {
                        MovimientoId = entity.MovimientoId,
                        CuentaId = entity.CuentaId,
                        TipoMovimiento = entity.TipoMovimiento,
                        Estado = entity.Estado,
                        Saldo = transaccion,
                    };

                    using (var contexts = _context)
                    {
                        var resultCuenta = contexts.Cuenta.Where(w => w.CuentaId == movimiento.CuentaId).AsNoTrackingWithIdentityResolution().FirstOrDefault();

                        var saldoMovimiento = contexts.Movimientos.Where(w => w.MovimientoId == movimiento.MovimientoId).AsNoTracking().FirstOrDefault();

                        //cuando el saldo inicial es 0 y el valor del saldo es superior al de la transaccion
                        if (resultCuenta.SaldoInicial >= 0 && resultCuenta.SaldoInicial >= transaccion)
                        {
                            //Diferencial de Saldo
                            var difSaldo = Math.Abs(saldoMovimiento.Saldo) + movimiento.Saldo;

                            //Actualiza Movimientos
                            contexts.Movimientos.Update(movimiento);
                            await contexts.SaveChangesAsync();

                            //Actualiza el movimiento cuenta de la cuenta
                            var resultCuentaMovimiento = contexts.CuentaMovimientos.Where(w => w.MovimientoId == movimiento.MovimientoId).FirstOrDefault();
                            resultCuentaMovimiento.Movimiento = movimiento.Saldo;
                            resultCuentaMovimiento.Saldo = resultCuenta.SaldoInicial + difSaldo;
                            contexts.CuentaMovimientos.Update(resultCuentaMovimiento);
                            await contexts.SaveChangesAsync();


                            //Actualiza la diferencia de Saldo en la cuenta
                            resultCuenta.SaldoInicial = resultCuenta.SaldoInicial + difSaldo;
                            contexts.Cuenta.Update(resultCuenta);

                        }
                        else
                        ////Actualiza el Saldo de la cuenta
                        if (movimientoDescripcion == "Retiro" && (resultCuenta.SaldoInicial == 0 || resultCuenta.SaldoInicial < entity.Saldo))
                        {
                            throw new AppException("Saldo no disponible");
                        }


                        await _context.SaveChangesAsync()
                            .ConfigureAwait(false);

                    }
                    await tx.CommitAsync();



                }
                catch (Exception ex)
                {
                    await tx.RollbackAsync();
                    await tx.DisposeAsync();
                    //throw new Exception($"Ocurrió un error {ex.Message}");
                    throw;
                    entity = null;
                }
            }
            return entity;
        }

        public async Task<Movimiento> Delete(Movimiento entity)
        {
            try
            {
                _context.Movimientos.Remove(entity);

                await _context.SaveChangesAsync()
                    .ConfigureAwait(false);


            }
            catch (Exception ex)
            {
                throw new Exception($"Ocurrió un error {ex.Message}");
                entity = null;
            }
            return entity;
        }

    }
}
