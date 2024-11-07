using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Administrativo
    {
        public int Id { get; set; }
        public int FkColaborador { get; set; }
        public string Correo { get; set; }
        public string Puesto { get; set; }
        public int Nomina { get; set; }
    }
}
