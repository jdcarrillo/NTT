using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NTT.Entities.DbContexts;
using NTT.Interfaces;
using NTT.Repository;
using NTT.WebApi.Controllers;
using System;
using System.Threading.Tasks;
using Xunit;

namespace UnitTest
{
    public class PersonaTest
    {
        private readonly PersonasController _controller;
        private readonly IPersonaRepository _service;
        private readonly IMapper _mapper;
        //private readonly ILogger<PersonaRepository> _logger;
        private NTTContext _context;

        public PersonaTest()
        {
            _service = new PersonaRepository(_context);
            _controller = new PersonasController(_service, _mapper);
        }
        [Fact]
        public void Get_Ok()
        {
            var result = _controller.Get();
            //Assert.IsType<OkObjectResult>(result); //error
            Assert.IsType<Task<ActionResult>>(result); //ok

        }

        [Fact]
        public void GetByid_Ok()
        {
            int id = 1;

            var result = (OkObjectResult)_controller.Get(id);

            var persona = Assert.IsType<OkObjectResult> (result);

        }

        [Fact]
        public void GetByid_Existe()
        {
            int id = 1;

            var result = (OkObjectResult)_controller.Get(id);
            var persona = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(persona?.Value,id); //error 

        }


    }
}
