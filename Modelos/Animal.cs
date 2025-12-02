using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelos
{
    public class Animal
    {
        [Key] public int Id { get; set; }
        public string Nombres { get; set; } = string.Empty;
        public int Edad { get; set; }
        public string Genero { get; set; } = string.Empty;

        // FK
        public int EspecieCodigo { get; set; }
        public int RazaId { get; set; }

        // Navegacion
        public Especie? Especie { get; set; }
        public Raza? Raza { get; set; }
    }
}
