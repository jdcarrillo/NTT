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
    public class MovimientosController : ControllerBase
    {

        private readonly ILogger<MovimientosController> _logger;
        private readonly IMovimientoRepostitory _movimientoRepository;
        private readonly IMapper _mapper;



        public MovimientosController(IMovimientoRepostitory movimientoRepository, ILogger<MovimientosController> logger, IMapper mapper)
        {
            _logger = logger;
            _movimientoRepository = movimientoRepository;

            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            MessageDTO messageDTO = new MessageDTO();
            try
            {
                var result = await _movimientoRepository.FindAll();
                var data = _mapper.Map<List<Movimiento>, List<MovimientoDTO>>(result);
                messageDTO = new MessageDTO() { message = "success", state = true, entity = data };
            }
            catch (AppException ex)
            {
                messageDTO = new MessageDTO() { message = ex.Message, state = false, entity = null };
            }
            return await Task.Run(() => Ok(messageDTO));
        }

        [HttpGet("id")]
        public async Task<IActionResult> Get(int id)
        {
            MessageDTO messageDTO = new MessageDTO();
            try
            {
                var result = await _movimientoRepository.FindById(id);
                var data = _mapper.Map<Movimiento, MovimientoDTO>(result);
                messageDTO = new MessageDTO() { message = "success", state = true, entity = data };
            }
            catch (AppException ex)
            {
                messageDTO = new MessageDTO() { message = ex.Message, state = false, entity = null };
            }
            return await Task.Run(() => Ok(messageDTO));
        }

        [HttpPost]
        public async Task<ActionResult> Post(MovimientoDTO entidad)
        {
            MessageDTO messageDTO = new MessageDTO();
            try
            {
                var data = _mapper.Map<MovimientoDTO, Movimiento>(entidad);
                await _movimientoRepository.Create(data);
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
        [HttpPut]
        public async Task<ActionResult> Put(MovimientoDTO entidad)
        {
            MessageDTO messageDTO = new MessageDTO();
            try
            {
                var data = _mapper.Map<MovimientoDTO, Movimiento>(entidad);
                await _movimientoRepository.Update(data);
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

        [HttpDelete]
        public async Task<ActionResult> Delete(MovimientoDTO entidad)
        {
            MessageDTO messageDTO = new MessageDTO();
            try
            {
                var data = _mapper.Map<MovimientoDTO, Movimiento>(entidad);
                await _movimientoRepository.Delete(data);
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
