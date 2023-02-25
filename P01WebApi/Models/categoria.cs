using System.ComponentModel.DataAnnotations;

namespace P01WebApi.Models
{
    public class categoria
    {
        [Key]
        public int id_categoria { get; set; }

        public string nombreCategoria { get; set; }

        public string descripcion { get; set; }
    }
}
