﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML
{
    public class Persona
    {
        public int IdPersona { get; set; }
        public string Nombre { get; set; }
        public string ApellidoPaterno { get; set; }
        public string ApellidoMaterno { get; set; }
        public string FechaNacimiento { get; set; }
        public string Sexo { get; set; }
        public string EstadoNacimiento { get; set; }
        public string Telefono { get; set; }
        public string CURP { get; set; }
        public ML.Direccion Direccion { get; set; }
        public List<object> Personas { get; set; }
    }
}
