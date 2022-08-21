using AutoMapper;
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
    public class ClientesController : ControllerBase
    {
        private readonly IClienteRepository _clienteRepository;
        private readonly IMapper _mapper;

        public ClientesController(IClienteRepository clienteRepository, IMapper mapper)
        {
            _clienteRepository = clienteRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<OkObjectResult> Get()
        {
            MessageDTO messageDTO = new MessageDTO();
            List<ClienteDTO> result = new List<ClienteDTO>();
            try
            {
                var data = await _clienteRepository.FindAll();
                result = _mapper.Map<List<ClienteDTO>>(data);

                //messageDTO = new MessageDTO() { message = "success", state = true, entity = result };


                //if (result.Count() == 0)
                //{
                //    messageDTO = new MessageDTO() { message = "Sin registros", state = true, entity = data };
                //}

            }
            catch (AppException ex)
            {
                //messageDTO = new MessageDTO() { message = ex.Message, state = false, entity = null };
            }
            //return await Task.Run(() => Ok(messageDTO));
            return await Task.Run(() => Ok(result));

        }

        [HttpGet("{id}")]

        public async Task<IActionResult> Get(int id)
        {
            MessageDTO messageDTO = new MessageDTO();
            try
            {
                var data = await _clienteRepository.FindById(id);
                var result = _mapper.Map<ClienteDTO>(data);

                messageDTO = new MessageDTO() { message = "success", state = true, entity = result };


                if (result == null)
                {
                    messageDTO = new MessageDTO() { message = "Sin registros", state = true, entity = data };
                }

            }
            catch (AppException ex)
            {
                messageDTO = new MessageDTO() { message = ex.Message, state = false, entity = null };
            }
            return await Task.Run(() => Ok(messageDTO));
        }

        [HttpPost]
        public async Task<IActionResult> Post(ClienteDTO entidad)
        {
            MessageDTO messageDTO = new MessageDTO();
            try
            {
                var data = _mapper.Map<ClienteDTO, Cliente>(entidad);
                var result = await _clienteRepository.Create(data);
                entidad = _mapper.Map<Cliente, ClienteDTO>(result);

                messageDTO = new MessageDTO() { message = "success", state = true, entity = entidad };

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
        public async Task<IActionResult> Put(ClienteDTO entidad)
        {
            MessageDTO messageDTO = new MessageDTO();
            try
            {
                var data = _mapper.Map<ClienteDTO, Cliente>(entidad);
                await _clienteRepository.Update(data);
                messageDTO = new MessageDTO() { message = "success", state = true, entity = data };

                if (data == null)
                {
                    messageDTO = new MessageDTO() { message = "Error", state = true, entity = data };
                    return await Task.Run(() => Ok(messageDTO));
                }

            }
            catch (AppException ex)
            {
                messageDTO = new MessageDTO() { message = ex.Message, state = false, entity = null };
            }
            return await Task.Run(() => Ok(messageDTO));

        }

        [HttpDelete]
        public async Task<IActionResult> Delete(ClienteDTO entidad)
        {
            MessageDTO messageDTO = new MessageDTO();
            try
            {
                var data = _mapper.Map<ClienteDTO, Cliente>(entidad);
                await _clienteRepository.Delete(data);
                messageDTO = new MessageDTO() { message = "success", state = true, entity = data };

                if (data == null)
                {
                    messageDTO = new MessageDTO() { message = "Error", state = true, entity = data };
                    return await Task.Run(() => Ok(messageDTO));
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
