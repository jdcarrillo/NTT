using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using NTT.Entities.DbContexts;
using NTT.Entities.Models;
using NTT.Interfaces;
using NTT.Util.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NTT.Repository
{
    public class CuentaRepository : ICuentaRepository
    {
        private readonly ILogger<CuentaRepository> _logger;
        private NTTContext _context;

        public CuentaRepository(NTTContext context, ILogger<CuentaRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<List<Cuenta>> FindAll()
        {
            List<Cuenta> data = new List<Cuenta>();
            try
            {
                data = _context.Cuenta
                                //.Include(i => i.Movimientos)
                                .ToList();

            }
            catch (Exception ex)
            {
                throw;
            }
            return await Task.Run(() => data);

        }

        public async Task<Cuenta> FindById(int id)
        {
            Cuenta data = new Cuenta();
            try
            {
                data = _context.Cuenta
                            .Where(w => w.CuentaId == id).FirstOrDefault();

            }
            catch (Exception ex)
            {
                throw;
            }
            return await Task.Run(() => data);

        }

        public async Task<Cuenta> Create(Cuenta entity)
        {

            using (var tx = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    using (var contexts = _context)
                    {
                        var accounts = contexts.Cuenta.Where(w => w.NumeroCuenta == entity.NumeroCuenta).FirstOrDefault();
                        if (accounts != null)
                        {
                            throw new AppException($"La cuenta {entity.NumeroCuenta} ya existe.");
                        }

                        //Inserta Cuenta
                        await contexts.Cuenta.AddAsync(entity);
                        await contexts.SaveChangesAsync();
                        int cuentaId = entity.CuentaId;

                        ////Actualiza el Saldo de la cuenta
                        //var resultCuenta = contexts.Cuenta.Where(w => w.CuentaId == entity.CuentaId).FirstOrDefault();
                        //var saldoTotal = resultCuenta.SaldoInicial;

                        //resultCuenta.SaldoInicial = saldoTotal;
                        //contexts.Cuenta.Update(resultCuenta);


                        //Inserta Movimiento
                        Movimiento movimiento = new Movimiento()
                        {
                            CuentaId = entity.CuentaId,
                            Fecha = DateTime.Now,
                            TipoMovimiento = "Depósito",
                            Estado = true,
                            Saldo = entity.SaldoInicial,
                        };
                        await contexts.Movimientos.AddAsync(movimiento);
                        await contexts.SaveChangesAsync();
                        int movimientoId = movimiento.MovimientoId;

                        //Inserta Cuenta Movimiento
                        //CuentaMovimiento cuentaMovimiento = new CuentaMovimiento()
                        //{
                        //    CuentaId = entity.CuentaId,
                        //    MovimientoId = movimientoId,
                        //    Saldo = entity.SaldoInicial,
                        //    Movimiento = 0,
                        //};

                        //await contexts.CuentaMovimientos.AddAsync(cuentaMovimiento);

                        //await contexts.SaveChangesAsync()
                        //    .ConfigureAwait(false);
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


        public async Task<Cuenta> Update(Cuenta entity)
        {
            try
            {
                _context.Cuenta.Update(entity);

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

        public async Task<Cuenta> Delete(Cuenta entity)
        {
            try
            {
                _context.Cuenta.Remove(entity);

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
