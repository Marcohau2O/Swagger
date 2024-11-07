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

        //Task<bool> AddToProfesorTable(Profesores profesor);
        //Task<bool> AddToAdministradorTable(Administradores administrador);
        // Task FiltrarColaboradores(DateTime fechaInicio, DateTime fechaFin, bool esProfesor);
    }
}