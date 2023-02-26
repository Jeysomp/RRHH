using System.ComponentModel.DataAnnotations;

namespace RRHH.Models
{
    public class PlanillaConcepto
    {
        public int PlanillaConceptoId { get; set; }

        [Display(Name = "Tipo de concepto")]
        public int PlanillaConceptoTipoId { get; set; }

        [Display(Name = "Concepto")]
        [StringLength(30, ErrorMessage = "30 carácteres máximos.")]
        public string Descripcion { get; set; }

        [Display(Name = "¿Es pago?")]
        public bool EsPago { get; set; }

        [Display(Name = "¿Es retencion?")]
        public bool EsRetencion { get; set; }

        [Display(Name = "¿Paga IR?")]
        public bool PagaIR { get; set; }

        [Display(Name = "¿Paga S. Social?")]
        public bool PagaSS { get; set; }

        [Display(Name = "¿Paga INATEC?")]
        public bool PagaINATEC { get; set; }

        [Display(Name = "¿Es IR?")]
        public bool EsIR { get; set; }

        [Display(Name = "¿Es S. Social?")]
        public bool EsSS { get; set; }

        [Display(Name = "Tipo de concepto")]
        public PlanillaConceptoTipo? PlanillaConceptoTipo { get; set; }
    }
}
