using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace P01WebApi.Models
{
    public class categoria
    {
        [Key]
        public int id_categoria { get; set; }

        public string nombreCategoria { get; set; }

        public string descripcion { get; set; } //poner el ? antes del nombre de la variable quiere decir que no acepta nulls
    }
}
