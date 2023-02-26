namespace RRHH.Models.ViewModels
{
    public class EmpleadoListViewModel
    {
        public List<Empleado> Empleados { get; set; }
        public int EmpleadoTipoId { get; set; }
        public int EmpleadoAreaId { get; set; }
        public string FiltrarNombre { get; set; }

    }
}
