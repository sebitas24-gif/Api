using System.ComponentModel.DataAnnotations;

namespace Api.Modelos
{
    public class Especie
    {

        [Key] public int Codigo { get; set; }
        public string NombreComun { get; set; } = string.Empty;

        // Navegacion
        public List<Animal>? Animales { get; set; }
    }
}
