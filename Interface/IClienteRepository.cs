using NTT.Entities.Models;
using NTT.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTT.Interfaces
{
    public interface IClienteRepository
    {

        Task<List<Cliente>> FindAll();
        Task<Cliente> FindById(int id);
        Task<List<Cliente>> FindByName(string name);
        Task<Cliente> Create(Cliente entity);
        Task<Cliente> Update(Cliente entity);
        Task<Cliente> Delete(Cliente entity);


    }
}
