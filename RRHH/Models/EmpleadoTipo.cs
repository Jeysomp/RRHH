using System.ComponentModel.DataAnnotations;

namespace RRHH.Models
{
    public class EmpleadoTipo
    {
        public int EmpleadoTipoId { get; set; }

        [StringLength(30, ErrorMessage = "30 carácteres máximos.")]
        public string Descripcion { get; set; }
    }
}
