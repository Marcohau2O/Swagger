using ApplicationCore.Commands;
using ApplicationCore.DTOs;
using ApplicationCore.Interfaces;
using ApplicationCore.Wrappers;
using AutoMapper;
using DevExpress.CodeParser;
using Domain.Entities;
using Infraestructure.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Channels;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.Services
{
    public class FormularioContactoService : IFormularioContactoService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public FormularioContactoService(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Response<int>> CreateFormularioContacto(FormularioContactoDto formularioContactoDto)
        {
            try
            {

                var e = new CreateFormularioContactoCommand();
                e.Nombre = formularioContactoDto.Nombre;
                e.Email = formularioContactoDto.Email;
                e.Telefono = formularioContactoDto.Telefono;
                e.Mensaje = formularioContactoDto.Mensaje;

                var es = _mapper.Map<Domain.Entities.FormularioContacto>(e);
                await _context.FormularioContactos.AddAsync(es);
                var req = await _context.SaveChangesAsync();
                var res = new Response<int>(es.Id, "Registro Creado");

                return res;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al Guardar los Cambios" + ex.InnerException?.Message, ex);
            }
        }
    }
}
