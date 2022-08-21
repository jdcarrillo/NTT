using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NTT.Entities.DbContexts;
using NTT.Interfaces;
using NTT.Repository;
using NTT.WebApi.Controllers;
using System.Threading.Tasks;
using Xunit;

namespace UnitTest
{
    public class ClienteTest
    {

        private readonly ClientesController _controller;
        private readonly IClienteRepository _service;
        private readonly IMapper _mapper;
        //private readonly ILogger<PersonaRepository> _logger;
        private NTTContext _context;

        public ClienteTest()
        {
            _service = new ClienteRepository(_context);
            _controller = new ClientesController(_service, _mapper);
        }
        [Fact]
        public void Cliente_Get_Ok()
        {
            var result = _controller.Get();
            Assert.IsType<Task<OkObjectResult>>(result); //ok
            //Assert.IsType<Task<ActionResult>>(result); //error

        }

    }
}
