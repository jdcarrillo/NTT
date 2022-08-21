using AutoMapper;
using NTT.Entities.Models;
using NTT.WebApi.DTOs;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NTT.WebApi
{
    public class AutoMapperConfig : Profile
    {
        public AutoMapperConfig()
        {
            CreateMap<Persona, PersonaDTO>();
            CreateMap<PersonaDTO, Persona>();

            CreateMap<Cliente, ClienteDTO>();
            CreateMap<ClienteDTO, Cliente>();

            CreateMap<Cuenta, CuentaDTO>();
            CreateMap<CuentaDTO, Cuenta>();

            CreateMap<Movimiento, MovimientoDTO>();
            CreateMap<MovimientoDTO, Movimiento>();

            CreateMap<CuentaMovimiento, CuentaMovimientoDTO>();
            CreateMap<CuentaMovimientoDTO, CuentaMovimiento>();

        }
    }
}
