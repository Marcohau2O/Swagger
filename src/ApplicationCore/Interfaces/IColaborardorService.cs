using ApplicationCore.DTOs;
using ApplicationCore.Wrappers;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Interfaces
{
    public interface IColaborardorService
    {

        Task<Response<object>> CreateColaborador(Colaboradores colaboradores);

        Task<Response<List<Colaboradores>>> GetColaboradoresPorFecha(DateTime FechaInicio, DateTime FechaFin);
        Task<Response<List<Colaboradores>>> GetColaboradoresPorFechaIngreso(DateTime FechaCreacion);
        Task<Response<List<Colaboradores>>> GetColaboradoresEdad(int Edad);
        Task<Response<List<Colaboradores>>> GetColaboradoresPorTipo(bool isProfesor);
        //Task<bool> AddToAdministradorTable(Administradores administrador);
        // Task FiltrarColaboradores(DateTime fechaInicio, DateTime fechaFin, bool esProfesor);
    }
}