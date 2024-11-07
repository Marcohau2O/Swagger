using ApplicationCore.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MediatR;
using Domain.Entities;

namespace Host.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ColaboradorController : ControllerBase
    {
        private readonly IColaborardorService _colaboradorService;
        public ColaboradorController(IColaborardorService service)
        {
            _colaboradorService = service;
        }

        [HttpGet("BuscarPorFecha")]
        public async Task<IActionResult> GetColaboradoresPorFecha([FromQuery] DateTime FechaInicio, DateTime FechaFin)
        {
            var response = await _colaboradorService.GetColaboradoresPorFecha(FechaInicio, FechaFin);
            return Ok(response);
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateColaborador([FromBody] Colaboradores colaboradores)
        {
            var results = await _colaboradorService.CreateColaborador(colaboradores);
            return Ok(results);
        }



    }
}