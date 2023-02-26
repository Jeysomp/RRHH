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
    public class PlanillaConceptosController : Controller
    {
        private readonly RRHHContext _context;

        public PlanillaConceptosController(RRHHContext context)
        {
            _context = context;
        }

        // GET: PlanillaConceptos
        public async Task<IActionResult> Index()
        {
            var rRHHContext = _context.PlanillaConcepto.Include(p => p.PlanillaConceptoTipo);
            return View(await rRHHContext.ToListAsync());
        }

        // GET: PlanillaConceptos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var planillaConcepto = await _context.PlanillaConcepto
                .Include(p => p.PlanillaConceptoTipo)
                .FirstOrDefaultAsync(m => m.PlanillaConceptoId == id);
            if (planillaConcepto == null)
            {
                return NotFound();
            }

            return View(planillaConcepto);
        }

        // GET: PlanillaConceptos/Create
        public IActionResult Create()
        {
            ViewData["PlanillaConceptoTipoId"] = new SelectList(_context.PlanillaConceptoTipo, "PlanillaConceptoTipoId", "Descripcion");
            return View();
        }

        // POST: PlanillaConceptos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PlanillaConceptoId,PlanillaConceptoTipoId,Descripcion,EsPago,EsRetencion,PagaIR,PagaSS,PagaINATEC,EsIR,EsSS")] PlanillaConcepto planillaConcepto)
        {
            planillaConcepto.PlanillaConceptoTipo = _context.PlanillaConceptoTipo.Find(planillaConcepto.PlanillaConceptoTipoId);
            if (ModelState.IsValid)
            {
                _context.Add(planillaConcepto);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["PlanillaConceptoTipoId"] = new SelectList(_context.PlanillaConceptoTipo, "PlanillaConceptoTipoId", "Descripcion", planillaConcepto.PlanillaConceptoTipoId);
            return View(planillaConcepto);
        }

        // GET: PlanillaConceptos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var planillaConcepto = await _context.PlanillaConcepto.FindAsync(id);
            if (planillaConcepto == null)
            {
                return NotFound();
            }
            ViewData["PlanillaConceptoTipoId"] = new SelectList(_context.PlanillaConceptoTipo, "PlanillaConceptoTipoId", "PlanillaConceptoTipoId", planillaConcepto.PlanillaConceptoTipoId);
            return View(planillaConcepto);
        }

        // POST: PlanillaConceptos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PlanillaConceptoId,PlanillaConceptoTipoId,Descripcion,EsPago,EsRetencion,PagaIR,PagaSS,PagaINATEC,EsIR,EsSS")] PlanillaConcepto planillaConcepto)
        {
            if (id != planillaConcepto.PlanillaConceptoId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(planillaConcepto);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PlanillaConceptoExists(planillaConcepto.PlanillaConceptoId))
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
            ViewData["PlanillaConceptoTipoId"] = new SelectList(_context.PlanillaConceptoTipo, "PlanillaConceptoTipoId", "PlanillaConceptoTipoId", planillaConcepto.PlanillaConceptoTipoId);
            return View(planillaConcepto);
        }

        // GET: PlanillaConceptos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var planillaConcepto = await _context.PlanillaConcepto
                .Include(p => p.PlanillaConceptoTipo)
                .FirstOrDefaultAsync(m => m.PlanillaConceptoId == id);
            if (planillaConcepto == null)
            {
                return NotFound();
            }

            return View(planillaConcepto);
        }

        // POST: PlanillaConceptos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var planillaConcepto = await _context.PlanillaConcepto.FindAsync(id);
            _context.PlanillaConcepto.Remove(planillaConcepto);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PlanillaConceptoExists(int id)
        {
            return _context.PlanillaConcepto.Any(e => e.PlanillaConceptoId == id);
        }
    }
}
