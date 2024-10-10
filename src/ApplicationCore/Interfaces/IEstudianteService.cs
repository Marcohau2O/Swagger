using ApplicationCore.DTOs;
using ApplicationCore.Wrappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Interfaces
{
    public interface IEstudianteServices
    {
            Task<Response<object>> GetEstudiante();

        Task<Response<int>> CreateEstudiante(EstudianteDto estudianteDto);
        Task<Response<int>> UpdateEstudiante(EstudianteDto estudianteDto);

        Task<Response<int>> DeleteEstudiante(int Id);

    }
}
