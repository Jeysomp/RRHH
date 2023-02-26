#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RRHH.Data;
using RRHH.Models;

namespace RRHH.Controllers
{
    public class EmpleadosController : Controller
    {
        private readonly RRHHContext _context;

        public EmpleadosController(RRHHContext context)
        {
            _context = context;
        }

        // GET: Empleados
        public async Task<IActionResult> Index()
        {
            var listEmpleados = _context.Empleado.Include(e => e.EmpleadoArea)
                                .Include(e => e.EmpleadoCargo)
                                .Include(e => e.EmpleadoEstado)
                                .Include(e => e.EmpleadoTipo);

            ViewData["EmpleadoAreaId"] = new SelectList(_context.Set<EmpleadoArea>(), "EmpleadoAreaId", "Descripcion");
            ViewData["EmpleadoTipoId"] = new SelectList(_context.Set<EmpleadoTipo>(), "EmpleadoTipoId", "Descripcion");

            var empleadoViewModel = new Models.ViewModels.EmpleadoListViewModel {
                EmpleadoAreaId = -1,
                EmpleadoTipoId = -1,
                FiltrarNombre = "",
                Empleados = await listEmpleados.ToListAsync()
            };

            return View(empleadoViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Index(string FiltrarNombre, int? EmpleadoAreaId, int? EmpleadoTipoId)
        {
            string f_Nombre = string.IsNullOrEmpty(FiltrarNombre) ? "" : FiltrarNombre;
            int f_ETipoId = (EmpleadoTipoId == null) ? -1 : (int)EmpleadoTipoId;
            int f_EAreaId = (EmpleadoAreaId == null) ? -1 : (int)EmpleadoAreaId;

            var listEmpleado = _context.Empleado.Include(e => e.EmpleadoArea)
                                        .Include(e => e.EmpleadoCargo)
                                        .Include(e => e.EmpleadoEstado)
                                        .Include(e => e.EmpleadoTipo)
                                        .Where(e => (e.PrimerNombre + e.SegundoNombre + e.PrimerApellido + e.SegundoApellido).ToUpper().Contains(f_Nombre.ToUpper())
                                                    && (e.EmpleadoAreaId == f_EAreaId || f_EAreaId == -1)
                                                    && (e.EmpleadoTipoId == f_ETipoId || f_ETipoId == -1));

            ViewData["EmpleadoAreaId"] = new SelectList(_context.Set<EmpleadoArea>(), "EmpleadoAreaId", "Descripcion", f_EAreaId);
            ViewData["EmpleadoTipoId"] = new SelectList(_context.Set<EmpleadoTipo>(), "EmpleadoTipoId", "Descripcion", f_ETipoId);


            var empleadoViewModel = new Models.ViewModels.EmpleadoListViewModel {
                EmpleadoAreaId = f_EAreaId,
                EmpleadoTipoId = f_ETipoId,
                FiltrarNombre = f_Nombre,
                Empleados = await listEmpleado.ToListAsync()
            };

            return View(empleadoViewModel);
        }


        // GET: Empleados/Create
        public IActionResult Create()
        {
            ViewData["EmpleadoAreaId"] = new SelectList(_context.Set<EmpleadoArea>(), "EmpleadoAreaId", "Descripcion");
            ViewData["EmpleadoCargoId"] = new SelectList(_context.Set<EmpleadoCargo>(), "EmpleadoCargoId", "Descripcion");
            ViewData["EmpleadoTipoId"] = new SelectList(_context.Set<EmpleadoTipo>(), "EmpleadoTipoId", "Descripcion");
            return View();
        }

        // POST: Empleados/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost, ActionName("Create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreatePost()
        {
            var empleadoToInsert = new Empleado();
            if (ModelState.IsValid) {
                empleadoToInsert.EmpleadoEstadoId = 1;//Activo

                if (await TryUpdateModelAsync<Empleado>(empleadoToInsert, "",
                            e => e.Codigo, e => e.NSS, e => e.NumeroCedula, e => e.PrimerNombre, e => e.SegundoNombre,
                            e => e.PrimerApellido, e => e.SegundoApellido, e => e.FechaNacimiento, e => e.Edad, e => e.Sexo,
                            e => e.Direccion, e => e.Telefono, e => e.SalarioOrdinario, e => e.FechaIngreso, e => e.NumeroCuentaBanco,
                            e => e.EmpleadoTipoId, e => e.EmpleadoAreaId, e => e.EmpleadoCargoId, e => e.EmpleadoEstadoId)) {
                    _context.Empleado.Add(empleadoToInsert);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            }
            ViewData["EmpleadoAreaId"] = new SelectList(_context.Set<EmpleadoArea>(), "EmpleadoAreaId", "Descripcion", empleadoToInsert.EmpleadoAreaId);
            ViewData["EmpleadoCargoId"] = new SelectList(_context.Set<EmpleadoCargo>(), "EmpleadoCargoId", "Descripcion", empleadoToInsert.EmpleadoCargoId);
            ViewData["EmpleadoTipoId"] = new SelectList(_context.Set<EmpleadoTipo>(), "EmpleadoTipoId", "Descripcion", empleadoToInsert.EmpleadoTipoId);
            return View(empleadoToInsert);
        }


        // GET: Empleados/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var empleado = await _context.Empleado.FindAsync(id);
            if (empleado == null)
            {
                return NotFound();
            }
            ViewData["EmpleadoAreaId"] = new SelectList(_context.Set<EmpleadoArea>(), "EmpleadoAreaId", "Descripcion", empleado.EmpleadoAreaId);
            ViewData["EmpleadoCargoId"] = new SelectList(_context.Set<EmpleadoCargo>(), "EmpleadoCargoId", "Descripcion", empleado.EmpleadoCargoId);
            ViewData["EmpleadoTipoId"] = new SelectList(_context.Set<EmpleadoTipo>(), "EmpleadoTipoId", "Descripcion", empleado.EmpleadoTipoId);
            return View(empleado);
        }

        // POST: Empleados/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditPost(int id)
        {
            var empleadoToUpdate = await _context.Empleado.FindAsync(id);

            if (ModelState.IsValid) {
                try {
                    if (await TryUpdateModelAsync<Empleado>(empleadoToUpdate, "",
                            e => e.Codigo, e => e.NSS, e => e.NumeroCedula, e => e.PrimerNombre, e => e.SegundoNombre,
                            e => e.PrimerApellido, e => e.SegundoApellido, e => e.FechaNacimiento, e => e.Edad, e => e.Sexo,
                            e => e.Direccion, e => e.Telefono, e => e.SalarioOrdinario, e => e.FechaIngreso, e => e.NumeroCuentaBanco,
                            e => e.EmpleadoTipoId, e => e.EmpleadoAreaId, e => e.EmpleadoCargoId)) {
                        await _context.SaveChangesAsync();
                    }
                }
                catch (DbUpdateConcurrencyException) {
                    if (!EmpleadoExists((int)id)) {
                        return NotFound();
                    }
                    else {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["EmpleadoAreaId"] = new SelectList(_context.Set<EmpleadoArea>(), "EmpleadoAreaId", "Descripcion", empleadoToUpdate.EmpleadoAreaId);
            ViewData["EmpleadoCargoId"] = new SelectList(_context.Set<EmpleadoCargo>(), "EmpleadoCargoId", "Descripcion", empleadoToUpdate.EmpleadoCargoId);
            ViewData["EmpleadoTipoId"] = new SelectList(_context.Set<EmpleadoTipo>(), "EmpleadoTipoId", "Descripcion", empleadoToUpdate.EmpleadoTipoId);
            return View(empleadoToUpdate);
        }

        public async Task<IActionResult> Save(int? id)
        {
            if (id == null) {
                ViewData["EmpleadoAreaId"] = new SelectList(_context.Set<EmpleadoArea>(), "EmpleadoAreaId", "Descripcion");
                ViewData["EmpleadoCargoId"] = new SelectList(_context.Set<EmpleadoCargo>(), "EmpleadoCargoId", "Descripcion");
                ViewData["EmpleadoTipoId"] = new SelectList(_context.Set<EmpleadoTipo>(), "EmpleadoTipoId", "Descripcion");
                ViewData["Operacion"] = "Create";
                ViewData["CodigoSiguiente"] = _context.Empleado.Count() + 1;

                return View();
            }
            else {
                var empleado = await _context.Empleado.Include(e => e.EmpleadoEstado).FirstAsync(e => e.EmpleadoId == id);

                ViewData["EmpleadoAreaId"] = new SelectList(_context.Set<EmpleadoArea>(), "EmpleadoAreaId", "Descripcion", empleado.EmpleadoAreaId);
                ViewData["EmpleadoCargoId"] = new SelectList(_context.Set<EmpleadoCargo>(), "EmpleadoCargoId", "Descripcion", empleado.EmpleadoCargoId);
                ViewData["EmpleadoTipoId"] = new SelectList(_context.Set<EmpleadoTipo>(), "EmpleadoTipoId", "Descripcion", empleado.EmpleadoTipoId);
                ViewData["Operacion"] = "Edit";
                return View(empleado);
            }
        }

        [HttpPost, ActionName("Save")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SavePost(int? id)
        {
            string operacion = (id != null) ? "Edit" : "Create";
            var empleadoToSave = (operacion == "Edit") ? await _context.Empleado.FindAsync(id) : new Empleado();

            if (ModelState.IsValid) {
                try {
                    if (operacion == "Create") {
                        empleadoToSave.EmpleadoEstadoId = 1;//Activo
                    }
                    if (await TryUpdateModelAsync<Empleado>(empleadoToSave, "",
                                e => e.Codigo, e => e.NSS, e => e.NumeroCedula, e => e.PrimerNombre, e => e.SegundoNombre,
                                e => e.PrimerApellido, e => e.SegundoApellido, e => e.FechaNacimiento, e => e.Edad, e => e.Sexo,
                                e => e.Direccion, e => e.Telefono, e => e.SalarioOrdinario, e => e.FechaIngreso, e => e.NumeroCuentaBanco,
                                e => e.EmpleadoTipoId, e => e.EmpleadoAreaId, e => e.EmpleadoCargoId, e => e.EmpleadoEstadoId)) {
                        if (operacion == "Create") {
                            _context.Empleado.Add(empleadoToSave);
                        }
                        await _context.SaveChangesAsync();
                    }
                }
                catch (DbUpdateConcurrencyException) {
                    if (!EmpleadoExists((int)id)) {
                        return NotFound();
                    }
                    else {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["EmpleadoAreaId"] = new SelectList(_context.Set<EmpleadoArea>(), "EmpleadoAreaId", "Descripcion", empleadoToSave.EmpleadoAreaId);
            ViewData["EmpleadoCargoId"] = new SelectList(_context.Set<EmpleadoCargo>(), "EmpleadoCargoId", "Descripcion", empleadoToSave.EmpleadoCargoId);
            ViewData["EmpleadoTipoId"] = new SelectList(_context.Set<EmpleadoTipo>(), "EmpleadoTipoId", "Descripcion", empleadoToSave.EmpleadoTipoId);
            return View(nameof(Create), empleadoToSave);
        }


        private bool EmpleadoExists(int id)
        {
            return _context.Empleado.Any(e => e.EmpleadoId == id);
        }
    }
}
