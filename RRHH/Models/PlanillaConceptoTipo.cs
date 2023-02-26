using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RRHH.Models
{
    public class PlanillaConceptoTipo
    {
        public int PlanillaConceptoTipoId { get; set; }

        [StringLength(30, ErrorMessage = "30 carácteres máximos.")]
        public string Descripcion { get; set; }
        public int Signo { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal Orden { get; set; }

    }
}
