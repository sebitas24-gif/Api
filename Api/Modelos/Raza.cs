using System.ComponentModel.DataAnnotations;

namespace Api.Modelos
{
    public class Raza
    {
        [Key] public int Id { get; set; }
        public string Nombre { get; set; }

        // Navegacion
        public List<Animal>? Animales { get; set; }
    }
}
