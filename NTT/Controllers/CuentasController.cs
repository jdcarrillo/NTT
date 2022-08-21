using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NTT.Entities.Models;
using NTT.Interfaces;
using NTT.Util.Helpers;
using NTT.WebApi.DTOs;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NTT.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CuentasController : ControllerBase
    {

        private readonly ILogger<CuentasController> _logger;
        private readonly ICuentaRepository _cuentaRepository;
        private readonly IMapper _mapper;

        public CuentasController(ICuentaRepository cuentaRepository, ILogger<CuentasController> logger, IMapper mapper)
        {
            _logger = logger;
            _cuentaRepository = cuentaRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<List<CuentaDTO>> Get()
        {
            var result = await _cuentaRepository.FindAll();
            var data = _mapper.Map<List<Cuenta>, List<CuentaDTO>>(result);
            return data;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            MessageDTO messageDTO = new MessageDTO();
            try
            {
                var result = await _cuentaRepository.FindById(id);
                var data = _mapper.Map<Cuenta, CuentaDTO>(result);
                messageDTO = new MessageDTO() { message = "success", state = true, entity = data };
            }
            catch (AppException ex)
            {
                messageDTO = new MessageDTO() { message = ex.Message, state = false, entity = null };
            }
            return await Task.Run(() => Ok(messageDTO));
        }

        [HttpPost]
        public async Task<ActionResult> Post(CuentaDTO entidad)
        {
            MessageDTO messageDTO = new MessageDTO();
            try
            {
                var data = _mapper.Map<CuentaDTO, Cuenta>(entidad);
                var result = await _cuentaRepository.Create(data);
                entidad = _mapper.Map<Cuenta, CuentaDTO>(result);

                messageDTO = new MessageDTO() { message = "success", state = true, entity = data };

                if (data == null)
                {
                    messageDTO = new MessageDTO() { message = "Error", state = true, entity = result };
                }

            }
            catch (AppException ex)
            {
                messageDTO = new MessageDTO() { message = ex.Message, state = false, entity = null };
            }
            return await Task.Run(() => Ok(messageDTO));

        }
        [HttpPut]
        public async Task<ActionResult> Put(CuentaDTO entidad)
        {
            MessageDTO messageDTO = new MessageDTO();
            try
            {
                var data = _mapper.Map<CuentaDTO, Cuenta>(entidad);
                await _cuentaRepository.Update(data);
                messageDTO = new MessageDTO() { message = "success", state = true, entity = entidad };

                if (data == null)
                {
                    messageDTO = new MessageDTO() { message = "Error", state = true, entity = data };
                }

            }
            catch (AppException ex)
            {
                messageDTO = new MessageDTO() { message = ex.Message, state = false, entity = null };
            }
            return await Task.Run(() => Ok(messageDTO));

        }

        [HttpDelete]
        public async Task<ActionResult> Delete(CuentaDTO entidad)
        {
            MessageDTO messageDTO = new MessageDTO();
            try
            {
                var data = _mapper.Map<CuentaDTO, Cuenta>(entidad);
                await _cuentaRepository.Delete(data);
                messageDTO = new MessageDTO() { message = "success", state = true, entity = data };

                if (data == null)
                {
                    messageDTO = new MessageDTO() { message = "Error", state = true, entity = data };
                }

            }
            catch (AppException ex)
            {
                messageDTO = new MessageDTO() { message = ex.Message, state = false, entity = null };
            }
            return await Task.Run(() => Ok(messageDTO));

        }
    }
}
