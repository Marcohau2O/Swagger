﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.DTOs
{
    public class EstudianteDto
    {
        public int? IdEstudiante {  get; set; }
        public string nombre {  get; set; }
        public int edad { get; set; }
        public string correo { get; set; }
    }
}
