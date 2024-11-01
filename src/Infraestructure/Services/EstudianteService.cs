using ApplicationCore.DTOs;
using ApplicationCore.Interfaces;
using ApplicationCore.Wrappers;
using AutoMapper;
using Infraestructure.Persistence;
using Microsoft.EntityFrameworkCore;
using ApplicationCore.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Net;
using DevExpress.DataAccess.ObjectBinding;

namespace Infraestructure.Services
{
    public class EstudianteService : IEstudianteServices
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IMapper _mapper;

        public EstudianteService(ApplicationDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<Response<object>> GetEstudiante()
        {
            object estudiante = await _dbContext.Estudiante.ToListAsync();
            return new Response<object>(estudiante);
        }
        public async Task<Response<string>> GetClientIpAddress()
        {
            IPHostEntry host = Dns.GetHostEntry(Dns.GetHostName());

            IPAddress ipAddress = host.AddressList.FirstOrDefault(ip => ip.AddressFamily == AddressFamily.InterNetwork);

            var ipAddressString = ipAddress?.ToString() ?? "No se pudo determinar la direccion";

            return new Response<string>(ipAddressString);

        }
        public async Task<Response<int>> CreateLog(LogDto request)
        {
            try
            {

                var ipAddress = await GetClientIpAddress();
                var ip = ipAddress.Message;

                var l = new LogDto();
                l.fecha = request.fecha;
                l.mensaje = request.mensaje;
                l.ipAddress = ip;
                l.NomFuncion = request.NomFuncion;
                l.StatusLog = request.StatusLog;
                l.Datos = request.Datos;


                var lo = _mapper.Map<Domain.Entities.logs>(l);
                await _dbContext.logs.AddAsync(lo);
                await _dbContext.SaveChangesAsync();
                return new Response<int>(lo.idLog, "Registro creado");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en SaveChangesAsync: {ex.Message}");
                throw;
            }
        }

        public async Task<Response<int>> CreateEstudiante(EstudianteDto request)
        {
            try
            {
                var e = new CreateEstudianteCommand();
                e.nombre = request.nombre;
                e.edad = request.edad;
                e.correo = request.correo;

                var es = _mapper.Map<Domain.Entities.Estudiante>(e);
                await _dbContext.Estudiante.AddAsync(es);
                var req = await _dbContext.SaveChangesAsync();
                var res = new Response<int>(es.Id, "Reegistro Creado");

                var log = new LogDto();
                log.Datos = "Datos";
                log.fecha = DateTime.Now.ToString();
                log.NomFuncion = "Create";
                log.mensaje = res.Message;
                log.StatusLog = "200";

                await CreateLog(log);

                return res;
            }
            catch (Exception e)
            {
                var errorLog = new LogDto();
                errorLog.Datos = "Datos";
                errorLog.fecha = DateTime.Now.ToString();
                errorLog.NomFuncion = "Create";

                if (e.InnerException != null)
                {
                    errorLog.mensaje = $"Error desconocido al create el registro. Mensaje interno: {e.InnerException.Message}";
                }
                else
                {
                    errorLog.mensaje = "Error desconocido al crear el registro";
                }

                errorLog.StatusLog = "500";

                await CreateLog(errorLog);
                throw;
            }
        }

        public async Task<Response<int>> UpdateEstudiante(EstudianteDto request)
        {
            try
            {
                var estudianteExiste = await _dbContext.Estudiante.FindAsync(request.IdEstudiante);

                if(estudianteExiste == null)
                {
                    return new Response<int>(0, "Estudiante No Encontrado");
                }

                estudianteExiste.nombre = request.nombre;
                estudianteExiste.edad = request.edad;
                estudianteExiste.correo = request.correo;
                await _dbContext.SaveChangesAsync();

                return new Response<int>(estudianteExiste.Id, "Estudiante Actualizado Correctamente");
            }
            catch (Exception ex)
            {
                var errorLog = new LogDto();
                errorLog.Datos = request.ToString();
                errorLog.fecha =DateTime.Now.ToString();
                errorLog.NomFuncion = "UPDATE JUGADOR";

                if (ex.InnerException != null)
                {
                    errorLog.mensaje = $"Error desconocido al actualizar el registro. Mensaje Interno {ex.InnerException.Message}";
                }
                else
                {
                    errorLog.mensaje = "Error desconocido al actualizar el registro";
                }
                errorLog.StatusLog = "500";

                await CreateLog(errorLog);
                throw;
            }
        }

        public async Task<Response<int>> DeleteEstudiante(int Id)
        {
            try
            {
                var estudiante = await _dbContext.Estudiante.FindAsync(Id);

                if (estudiante == null)
                {
                    return new Response<int>(0, "Estudiante no Encontrado");
                }

                _dbContext.Estudiante.Remove(estudiante);
                await _dbContext.SaveChangesAsync();
                return new Response<int>(Id, "Estudiante Eliminado Correctamente");
            }
            catch (Exception ex)
            {
                return new Response<int>(0, "Error al Eliminar el Estudiante: " + ex.Message);
            }
        }

        public async Task<byte[]> GetPDF()
        {
            ObjectDataSource source = new ObjectDataSource();

            var report = new ApplicationCore.PDF.Report1();


            var estudiantes = await (from e in _dbContext.Estudiante
                                     select new EstudianteDto
                                     {
                                         IdEstudiante = e.Id,
                                         edad = e.edad,
                                         nombre = e.nombre,
                                         correo = e.correo,
                                     }).ToListAsync();

            EstudiantesPDFDTO reportePdf = new EstudiantesPDFDTO();
            reportePdf.Fecha = DateTime.Now.ToString("dd/MM/yyyy");
            reportePdf.Hora = DateTime.Now.ToString("hh:mm");
            reportePdf.Estudiantes = estudiantes;

            source.DataSource = reportePdf;
            report.DataSource = source;
            using (var memory = new MemoryStream())
            {
                await report.ExportToPdfAsync(memory);
                memory.Position = 0;
                return memory.ToArray();
            }
        }
    }
}
