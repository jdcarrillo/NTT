using NTT.Entities.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NTT.Interfaces
{
    public interface ICuentaRepository
    {
        Task<List<Cuenta>> FindAll();
        Task<Cuenta> FindById(int id);
        Task<Cuenta> Create(Cuenta entity);
        Task<Cuenta> Update(Cuenta entity);
        Task<Cuenta> Delete(Cuenta entity);
    }
}
