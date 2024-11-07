using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.DTOs
{
    public class ProfesorDto
    {
        public string Id { get; set; }
        public string FKColaborador { get; set; }
        public string Correo { get; set; }
        public string Departamento { get; set; }
    }
}
