using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NTT.Entities.Models;
using NTT.Interfaces;
using NTT.Util.Helpers;
using NTT.WebApi.DTOs;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;

namespace NTT.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonasController : ControllerBase
    {

        //private readonly ILogger<PersonasController> _logger;
        private readonly IPersonaRepository _personaRepository;
        private readonly IMapper _mapper;


        public PersonasController(IPersonaRepository personaRepository, IMapper mapper)
        {
            _personaRepository = personaRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult> Get()
        {
            MessageDTO messageDTO = new MessageDTO();
            try
            {
                var result = await _personaRepository.FindAll();
                var data = _mapper.Map<List<PersonaDTO>>(result);

                messageDTO = new MessageDTO() { message = "success", state = true, entity = result };

                if (result.Count == 0)
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

        [HttpGet("{id}")]
        public ActionResult Get(int id)
        {
            PersonaDTO data = new PersonaDTO();
            try
            {
                var result = _personaRepository.FindById(id);
                data = _mapper.Map<PersonaDTO>(result);

                if (data == null)
                {
                    return NotFound();
                }

            }
            catch (AppException ex)
            {
                return Ok(ex.Message);

            }
            return Ok(data);
        }


        [HttpPost]
        public async Task<IActionResult> Post(PersonaDTO entidad)
        {
            MessageDTO messageDTO = new MessageDTO();
            try
            {
                var data = _mapper.Map<PersonaDTO, Persona>(entidad);
                await _personaRepository.Create(data);
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
        public async Task<IActionResult> Put(PersonaDTO entidad)
        {
            MessageDTO messageDTO = new MessageDTO();
            try
            {
                var data = _mapper.Map<PersonaDTO, Persona>(entidad);
                await _personaRepository.Update(data);
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
        public async Task<IActionResult> Delete(PersonaDTO entidad)
        {
            MessageDTO messageDTO = new MessageDTO();
            try
            {
                var data = _mapper.Map<PersonaDTO, Persona>(entidad);
                await _personaRepository.Delete(data);
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
