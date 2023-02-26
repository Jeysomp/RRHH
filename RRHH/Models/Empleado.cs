using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RRHH.Models
{
    public class Empleado
    {
        public int EmpleadoId { get; set; }

        [Display(Name = "*Código")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Debe escribir un nombre.")]
        [StringLength(6, ErrorMessage = "6 carácteres máximos.")]
        public string Codigo { get; set; }

        [Display(Name = "# Seguro social")]
        public int NSS { get; set; }

        [Display(Name = "*Cedula")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Especifique el número de cédula.")]
        [StringLength(12, ErrorMessage = "12 carácteres máximos.")] 
        public string NumeroCedula { get; set; }

        [Display(Name = "*Primer nombre")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Debe escribir un nombre.")]
        [StringLength(30, ErrorMessage = "30 carácteres máximos.")]
        public string PrimerNombre { get; set; }

        [Display(Name = "Segundo nombre")]
        [StringLength(30, ErrorMessage = "30 carácteres máximos.")]
        public string SegundoNombre { get; set; }

        [Display(Name = "*Primer apellido")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Debe escribir un apellido.")]
        [StringLength(30, ErrorMessage = "30 carácteres máximos.")]
        public string PrimerApellido { get; set; }

        [Display(Name = "Segundo apellido")]
        [StringLength(30, ErrorMessage = "30 carácteres máximos.")]
        public string SegundoApellido { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Fecha de nacimiento")]
        public DateTime FechaNacimiento { get; set; }

        public int Edad { get; set; }
        public string Sexo { get; set; }

        [Display(Name = "Dirección")]
        [StringLength(200, ErrorMessage = "200 carácteres máximos.")] 
        public string Direccion { get; set; }

        [StringLength(20, ErrorMessage = "30 carácteres máximos.")] 
        public string Telefono { get; set; }

        [Display(Name = "Salario")]
        [Column(TypeName = "decimal(18,2)")]
        public decimal SalarioOrdinario { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "*Fecha ingreso")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Debe especificar una fecha de ingreso.")] 
        public DateTime FechaIngreso { get; set; }

        [Display(Name = "# cta Banco")]
        public int NumeroCuentaBanco { get; set; }

        [Display(Name = "Tipo de empleado")]
        public int EmpleadoTipoId { get; set; }

        [Display(Name = "Área")]
        public int EmpleadoAreaId { get; set; }

        [Display(Name = "Cargo")]
        public int EmpleadoCargoId { get; set; }

        [Display(Name = "Estado")]
        public int EmpleadoEstadoId { get; set; }

        [Display(Name = "Nombre del empleado")]
        public string NombreCompleto { get { return $"{PrimerNombre} {SegundoNombre} {PrimerApellido} {SegundoApellido}"; } }

        [Display(Name = "Tipo")]
        public virtual EmpleadoTipo? EmpleadoTipo { get; set; }

        [Display(Name = "Area")]
        public virtual EmpleadoArea? EmpleadoArea { get; set; }

        [Display(Name = "Cargo")]
        public virtual EmpleadoCargo? EmpleadoCargo { get; set; }

        [Display(Name = "Estado")]
        public virtual EmpleadoEstado? EmpleadoEstado { get; set; }

        //TODO: MANEJAR EL HISTÓRICO DE CAMBIOS DE ÁREA, CARGO, ESTADO Y TIPOS PARA EL EMPLEADO.
    }
}
