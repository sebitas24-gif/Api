using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelos
{
    public class Especie
    {

        [Key] public int Codigo { get; set; }
        public string NombreComun { get; set; } = string.Empty;

        // Navegacion
        public List<Animal>? Animales { get; set; }
    }
}
