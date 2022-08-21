using NTT.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTT.Interfaces
{
    public interface IMovimientoRepostitory
    {
        Task<List<Movimiento>> FindAll();
        Task<Movimiento> FindById(int id);
        Task<Movimiento> Create(Movimiento entity);
        Task<Movimiento> Update(Movimiento entity);
        Task<Movimiento> Delete(Movimiento entity);
    }
}
