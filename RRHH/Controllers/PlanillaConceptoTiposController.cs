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
    public class PlanillaConceptoTiposController : Controller
    {
        private readonly RRHHContext _context;

        public PlanillaConceptoTiposController(RRHHContext context)
        {
            _context = context;
        }

        // GET: PlanillaConceptoTipos
        public async Task<IActionResult> Index()
        {
            return View(await _context.PlanillaConceptoTipo.ToListAsync());
        }

        // GET: PlanillaConceptoTipos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var planillaConceptoTipo = await _context.PlanillaConceptoTipo
                .FirstOrDefaultAsync(m => m.PlanillaConceptoTipoId == id);
            if (planillaConceptoTipo == null)
            {
                return NotFound();
            }

            return View(planillaConceptoTipo);
        }

        // GET: PlanillaConceptoTipos/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: PlanillaConceptoTipos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PlanillaConceptoTipoId,Descripcion,Signo,Orden")] PlanillaConceptoTipo planillaConceptoTipo)
        {
            if (ModelState.IsValid)
            {
                _context.Add(planillaConceptoTipo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(planillaConceptoTipo);
        }

        // GET: PlanillaConceptoTipos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var planillaConceptoTipo = await _context.PlanillaConceptoTipo.FindAsync(id);
            if (planillaConceptoTipo == null)
            {
                return NotFound();
            }
            return View(planillaConceptoTipo);
        }

        // POST: PlanillaConceptoTipos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PlanillaConceptoTipoId,Descripcion,Signo,Orden")] PlanillaConceptoTipo planillaConceptoTipo)
        {
            if (id != planillaConceptoTipo.PlanillaConceptoTipoId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(planillaConceptoTipo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PlanillaConceptoTipoExists(planillaConceptoTipo.PlanillaConceptoTipoId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(planillaConceptoTipo);
        }

        // GET: PlanillaConceptoTipos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var planillaConceptoTipo = await _context.PlanillaConceptoTipo
                .FirstOrDefaultAsync(m => m.PlanillaConceptoTipoId == id);
            if (planillaConceptoTipo == null)
            {
                return NotFound();
            }

            return View(planillaConceptoTipo);
        }

        // POST: PlanillaConceptoTipos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var planillaConceptoTipo = await _context.PlanillaConceptoTipo.FindAsync(id);
            _context.PlanillaConceptoTipo.Remove(planillaConceptoTipo);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Save(int? id)
        {
            if (id == null) {
                ViewData["Operacion"] = "Create";
                return PartialView("_EditPartialView");
            }
            else {
                var planillaConceptoTipo = await _context.PlanillaConceptoTipo.FindAsync(id);
                ViewData["Operacion"] = "Edit";
                return PartialView("_EditPartialView", planillaConceptoTipo);
            }
        }

        [HttpPost, ActionName("Save")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SavePost(int? id)
        {
            string operacion = (id != null) ? "Edit" : "Create";
            var conceptoTipoToSave = (operacion == "Edit") ? await _context.PlanillaConceptoTipo.FindAsync(id) : new PlanillaConceptoTipo();
            if (ModelState.IsValid) {
                try {
                    if (await TryUpdateModelAsync<PlanillaConceptoTipo>(conceptoTipoToSave, "",
                        c => c.Descripcion, c => c.Signo, c => c.Orden)) {
                        if (operacion == "Create") {
                            _context.PlanillaConceptoTipo.Add(conceptoTipoToSave);
                        }
                        await _context.SaveChangesAsync();
                    }
                }
                catch (DbUpdateConcurrencyException) {
                    if (!PlanillaConceptoTipoExists((int)id)) {
                        return NotFound();
                    }
                    else {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return PartialView("_EditPartialView", conceptoTipoToSave);
        }


        private bool PlanillaConceptoTipoExists(int id)
        {
            return _context.PlanillaConceptoTipo.Any(e => e.PlanillaConceptoTipoId == id);
        }
    }
}
