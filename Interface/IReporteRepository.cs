using NTT.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTT.Interfaces
{
    public interface IReporteRepository
    {
        Task<dynamic> FindAll();
        Task<dynamic> FindBy(string valor);
    }
}
