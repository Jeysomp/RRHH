using RRHH.Models;

namespace RRHH.Data
{
    public class DbInitializer
    {
        public static void Initialize(RRHHContext context)
        {
            context.Database.EnsureCreated();

            //Validando si existe almenos un registro en una tabla catálogo.
            if (context.EmpleadoArea.Any()) {
                return; //Base de datos ya ha sido iniciada.
            }

            if (!context.EmpleadoTipo.Any()) {
                var empleadoTipos = new EmpleadoTipo[] {
                    new EmpleadoTipo{Descripcion="Permanente"},
                    new EmpleadoTipo{Descripcion="Temporal"}
                };
                foreach (EmpleadoTipo et in empleadoTipos) {
                    context.EmpleadoTipo.Add(et);
                }
                context.SaveChanges();
            }
            else {
                return;
            }

            if (!context.EmpleadoEstado.Any()) {
                var empleadoEstados = new EmpleadoEstado[] {
                    new EmpleadoEstado{Descripcion="Activo"},
                    new EmpleadoEstado{Descripcion="Inactivo"}
                };
                foreach (EmpleadoEstado ee in empleadoEstados) {
                    context.EmpleadoEstado.Add(ee);
                }
                context.SaveChanges();
            }

            var empleadoCargos = new EmpleadoCargo[] {
                new EmpleadoCargo{Descripcion="Gerente"},
                new EmpleadoCargo{Descripcion="Contador"},
                new EmpleadoCargo{Descripcion="Administrador"}
            };
            foreach (EmpleadoCargo ec in empleadoCargos) {
                context.EmpleadoCargo.Add(ec);
            }
            context.SaveChanges();

            context.EmpleadoArea.Add(new EmpleadoArea { Descripcion = "Administración" });
            context.SaveChanges();
        }

    }
}
