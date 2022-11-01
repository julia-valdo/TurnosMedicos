using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TurnosMedicos.Models;
using TurnosMedicos.ModelsView;
using TurnosMedicos.Reglas;

namespace TurnosMedicos.Controllers
{
    [Authorize]
    public class TurnosController : Controller
    {
        private readonly DbContext _context;

        public TurnosController(DbContext context)
        {
            _context = context;
        }

        // GET: Turnos
        [Authorize(Roles = "Administrador, Supervisor, Paciente")]
        public async Task<IActionResult> Index()
        {
            return View(await _context.Turno.Include(m => m.Medico).ToListAsync());
        }

        // GET: Turnos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Turno == null)
            {
                return NotFound();
            }

            var turno = await _context.Turno
                .FirstOrDefaultAsync(m => m.TurnoId == id);
            if (turno == null)
            {
                return NotFound();
            }

            return View(turno);
        }

        // GET: Turnos/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Turnos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Fecha")] Turno turno)
        {
            if (ModelState.IsValid)
            {
                _context.Add(turno);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(turno);
        }

        // GET: Turnos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Turno == null)
            {
                return NotFound();
            }

            var turno = await _context.Turno.FindAsync(id);
            if (turno == null)
            {
                return NotFound();
            }
            return View(turno);
        }

        // POST: Turnos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Fecha")] Turno turno)
        {
            if (id != turno.TurnoId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(turno);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TurnoExists(turno.TurnoId))
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
            return View(turno);
        }

        // GET: Turnos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Turno == null)
            {
                return NotFound();
            }

            var turno = await _context.Turno
                .FirstOrDefaultAsync(m => m.TurnoId == id);
            if (turno == null)
            {
                return NotFound();
            }

            return View(turno);
        }

        // POST: Turnos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Turno == null)
            {
                return Problem("Entity set 'DbContext.Turno'  is null.");
            }
            var turno = await _context.Turno.FindAsync(id);
            if (turno != null)
            {
                _context.Turno.Remove(turno);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TurnoExists(int id)
        {
            return _context.Turno.Any(e => e.TurnoId == id);
        }

        // GET: JornadasLaborales/Generacion
        public IActionResult Generacion()
        {
            ViewData["MedicoId"] = new SelectList(_context.Medico, "MedicoId", "Nombre");
            return View();
        }

        [HttpPost, ActionName("Generacion")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Generacion(GeneradorTurno generador)
        {
            var regla = new ReglaTurnos(_context);
            await regla.GenerarTurnos(generador);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> ListarTurnos(int id)
        {
            var turnos = _context.Turno.Where(m => m.MedicoId == id).ToList();
            return View(turnos);
        }
    }
}
