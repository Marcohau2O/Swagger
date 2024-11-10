﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Profesor
    {
        public int Id { get; set; }
        public int FkColaborador { get; set; }
        public string Correo { get; set; }
        public string Departamento { get; set; }

        [JsonIgnore]
        public Colaboradores Colaboradores { get; set; }
    }
}
