using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using NTT.Entities.DbContexts;
using NTT.Entities.Models;
using NTT.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NTT.Repository
{
    public class ClienteRepository : IClienteRepository
    {
        //private readonly ILogger<ClienteRepository> _logger;
        private NTTContext _context;

        public ClienteRepository(NTTContext context)
        {
            _context = context;
            //_logger = logger;
        }

        public async Task<Cliente> FindById(int id)
        {
            Cliente data = new Cliente();
            try
            {
                data = await _context.Clientes.FindAsync(id);


            }
            catch (Exception ex)
            {
                throw;
            }
            return await Task.Run(() => data);

        }

        public async Task<List<Cliente>> FindAll()
        {
            List<Cliente> data = new List<Cliente>();
            try
            {
                data = _context.Clientes
                                //.Include(i => i.Cliente)
                                .ToList();

            }
            catch (Exception ex)
            {
                throw;
            }
            return await Task.Run(() => data);

        }

        public async Task<List<Cliente>> FindByName(string name)
        {
            List<Cliente> data = new List<Cliente>();
            try
            {
                data = _context.Clientes
                                .Include(i => i.Persona)
                                .Where(a => a.Persona.Nombre.ToString().ToLower().Contains(name.ToLower()))
                                .ToList();

            }
            catch (Exception ex)
            {
                throw;
            }
            return await Task.Run(() => data);

        }

        public async Task<Cliente> Create(Cliente entity)
        {

            try
            {
                _context.Clientes.Attach(entity);

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


        public async Task<Cliente> Update(Cliente entity)
        {
            try
            {
                _context.Clientes.Update(entity);

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

        public async Task<Cliente> Delete(Cliente entity)
        {
            try
            {
                _context.Clientes.Remove(entity);

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
