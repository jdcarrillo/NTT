using NTT.Entities.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NTT.Interfaces
{
    public interface IPersonaRepository
    {
        Persona FindById(int id);
        Task<List<Persona>> FindAll();
        
        Task<Persona> Create(Persona entity);
        Task<Persona> Update(Persona entity);
        Task<Persona> Delete(Persona entity);

    }
}