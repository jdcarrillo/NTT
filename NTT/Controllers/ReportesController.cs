using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NTT.Interfaces;
using NTT.Util.Helpers;
using NTT.WebApi.DTOs;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NTT.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportesController : ControllerBase
    {

        private readonly IMapper _mapper;
        private readonly IReporteRepository _reporteRepository;

        public ReportesController(ILogger<ReportesController> logger, IMapper mapper, IReporteRepository reporteRepository)
        {

            _mapper = mapper;
            _reporteRepository = reporteRepository;
        }


        [HttpGet]
        public async Task<IActionResult> Get(string fechaInicio, string fechaFin)
        {
            MessageDTO messageDTO = new MessageDTO();
            try
            {
                var data = await _reporteRepository.FindByDate(fechaInicio, fechaFin);
                var result = _mapper.Map<dynamic>(data);

                messageDTO = new MessageDTO() { message = "success", state = true, entity = result };


            }
            catch (AppException ex)
            {
                messageDTO = new MessageDTO() { message = ex.Message, state = false, entity = null };
            }
            return await Task.Run(() => Ok(messageDTO));
        }


        #region OtrosReportes - Comentado
        ////[HttpGet]
        //public async Task<IActionResult> Get()
        //{
        //    MessageDTO messageDTO = new MessageDTO();
        //    try
        //    {
        //        var data = await _reporteRepository.FindAll();
        //        var result = _mapper.Map<dynamic>(data);

        //        messageDTO = new MessageDTO() { message = "success", state = true, entity = result };

        //    }
        //    catch (AppException ex)
        //    {
        //        messageDTO = new MessageDTO() { message = ex.Message, state = false, entity = null };
        //    }
        //    return await Task.Run(() => Ok(messageDTO));
        //}

        ////[HttpGet("{valor}")]
        //public async Task<IActionResult> Get(string valor)
        //{
        //    MessageDTO messageDTO = new MessageDTO();
        //    try
        //    {
        //        var data = await _reporteRepository.FindBy(valor);
        //        var result = _mapper.Map<dynamic>(data);

        //        messageDTO = new MessageDTO() { message = "success", state = true, entity = result };


        //    }
        //    catch (AppException ex)
        //    {
        //        messageDTO = new MessageDTO() { message = ex.Message, state = false, entity = null };
        //    }
        //    return await Task.Run(() => Ok(messageDTO));
        //}

        #endregion

    }
}
