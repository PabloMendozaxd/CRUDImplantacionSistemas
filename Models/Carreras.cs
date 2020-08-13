using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProyectoFinal.Models
{
    public class Carreras
    {
        public Guid Id { get; set; }
        public string Titulo { get; set; }
        public int Creditos { get; set; }
        public string Campus { get; set; }

    }
}
