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

        [HttpGet("BuscarPorTipo")]
        public async Task<IActionResult> GetColaboradoresPorTipo([FromQuery] bool isProfesor)
        {
            var response = await _colaboradorService.GetColaboradoresPorTipo(isProfesor);
            return Ok(response);
        }

        [HttpGet("BuscarPorFechaIngreso")]
        public async Task<IActionResult> GetColaboradoresPorFechaIngreso([FromQuery] DateTime FechaCreacion)
        {
           var response = await _colaboradorService.GetColaboradoresPorFechaIngreso(FechaCreacion);
            return Ok(response);
        }

        [HttpGet("GetPorEdad")]
        public async Task<IActionResult> GetColaboradoresEdad([FromQuery] int Edad)
        {
            var response = await _colaboradorService.GetColaboradoresEdad(Edad);
            return Ok(response);
        }
    }
}