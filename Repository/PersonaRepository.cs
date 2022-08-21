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
    public class PersonaRepository : IPersonaRepository
    {
        //private readonly ILogger<PersonaRepository> _logger;
        private NTTContext _context;

        public PersonaRepository(NTTContext context)
        {
            _context = context;
            //_logger = logger;
        }

        public Persona FindById(int id)
        {
            Persona data = new Persona();
            try
            {
                data = _context.Personas.Where(w => w.PersonaId == id).FirstOrDefault();

            }
            catch (Exception ex)
            {
                throw;
            }
            return data;

        }

        public async Task<List<Persona>> FindAll()
        {
            List<Persona> data = new List<Persona>();
            try
            {
                data = _context.Personas
                                .ToList();

            }
            catch (Exception ex)
            {
                throw;
            }
            return await Task.Run(() => data);

        }



        public async Task<Persona> Create(Persona entity)
        {
            try
            {
                _context.Personas.Attach(entity);

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

        public async Task<Persona> Update(Persona entity)
        {
            try
            {
                _context.Personas.Update(entity);

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

        public async Task<Persona> Delete(Persona entity)
        {
            try
            {
                _context.Personas.Remove(entity);

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
