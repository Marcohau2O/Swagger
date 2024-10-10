using ApplicationCore.DTOs;
using ApplicationCore.Interfaces;
using ApplicationCore.Wrappers;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Host.Controllers
{
    [Route("api/estudiante")]
    [ApiController]
    public class Estudiante :ControllerBase
    {
        private readonly IEstudianteServices _service;
        public Estudiante(IEstudianteServices service)
        {
            _service = service;
        }

        [Route("getEstudiante")]
        [HttpGet]
        public async Task<IActionResult> GetEstudiante()
        {
            var result = await _service.GetEstudiante();
            return Ok(result);
        }

        [HttpPost("create")]
        public async Task<ActionResult<Response<int>>> Create([FromBody] EstudianteDto request)
        {
            var result = await _service.CreateEstudiante(request);
            return Ok(result);
        }

        [HttpPost("update-estudiante")]
        public async Task<ActionResult<Response<int>>> UpdateEstudiante([FromBody] EstudianteDto request)
        {
            var result = await _service.UpdateEstudiante(request);
            return Ok(result);
        }

        [HttpPost("delete/{Id}")]
        public async Task<ActionResult<Response<int>>> deleteEstudiante([FromRoute] int Id)
        {
            var result = await _service.DeleteEstudiante(Id);
            return Ok(result);
        }
    }
}
