using System;
using System.Collections.Generic;
using System.Text;

namespace InvernaderoApp.Dtos
{
    class Historico
    {
        public List<Datos> temperatura { get; set; }
        public List<Datos> humedad { get; set; }
        public List<Datos> ppm { get; set; }
    }

    class Datos
    {
        public DateTime fecha { get; set; }
        public float dato { get; set; }
    }
}
