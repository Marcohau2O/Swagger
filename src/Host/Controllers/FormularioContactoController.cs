using ApplicationCore.DTOs;
using ApplicationCore.Interfaces;
using ApplicationCore.Wrappers;
using Infraestructure.Services;
using Microsoft.AspNetCore.Mvc;

namespace Host.Controllers
{
    [Route("Api/FormularioContacto")]
    [ApiController]
    public class FormularioContactoController : ControllerBase
    {
        private readonly IFormularioContactoService _service;
        private readonly IEmailService _emailService;
        public FormularioContactoController (IFormularioContactoService service, IEmailService emailService)
        {
            _service = service;
            _emailService = emailService;
        }

        [HttpPost("Create")]
        public async Task<ActionResult<Response<int>>> CreateFormulario([FromBody] FormularioContactoDto request)
        {
            var result = await _service.CreateFormularioContacto(request);
            return Ok(result);
        }

        [HttpPost("send")]
        public async Task<IActionResult> SendEmail([FromBody] EmailRequest request)
        {
            if (string.IsNullOrEmpty(request.ToEmail) || string.IsNullOrEmpty(request.Subject) || string.IsNullOrEmpty(request.Body))
            {
                return BadRequest("Todos los campos son obligatorios.");
            }

            var result = await _emailService.SendEmailAsync(request.ToEmail, request.Subject, request.Body);

            if (result)
            {
                return Ok("Correo enviado correctamente.");
            }

            return StatusCode(500, "Error al enviar el correo.");
        }

        public class EmailRequest
        {
            public string ToEmail { get; set; }
            public string Subject { get; set; }
            public string Body { get; set; }
        }
    }
}
