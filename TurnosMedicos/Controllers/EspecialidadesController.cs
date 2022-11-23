using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TurnosMedicos.Models;

namespace TurnosMedicos.Controllers
{
    [Authorize]
    public class EspecialidadesController : Controller
    {
        private readonly TurnosContext _context;

        public EspecialidadesController(TurnosContext context)
        {
            _context = context;
        }

        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> Index()
        {
            return View(await _context.Especialidad.ToListAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Especialidad == null)
            {
                return NotFound();
            }

            var especialidad = await _context.Especialidad
                .FirstOrDefaultAsync(m => m.EspecialidadId == id);
            if (especialidad == null)
            {
                return NotFound();
            }

            return View(especialidad);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("EspecialidadId,Nombre")] Especialidad especialidad)
        {
            if (_context.Especialidad.Any(e => e.Nombre == especialidad.Nombre))
                ModelState.AddModelError("Nombre", "Ya existe una especialidad con ese nombre");

            if (ModelState.IsValid)
            {
                _context.Add(especialidad);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(especialidad);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Especialidad == null)
            {
                return NotFound();
            }

            var especialidad = await _context.Especialidad.FindAsync(id);
            if (especialidad == null)
            {
                return NotFound();
            }
            return View(especialidad);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("EspecialidadId,Nombre")] Especialidad especialidad)
        {
            if (_context.Especialidad.Any(e => e.Nombre == especialidad.Nombre))
                ModelState.AddModelError("Nombre", "Ya existe una especialidad con ese nombre");

            if (id != especialidad.EspecialidadId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(especialidad);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EspecialidadExists(especialidad.EspecialidadId))
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
            return View(especialidad);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Especialidad == null)
            {
                return NotFound();
            }

            var especialidad = await _context.Especialidad
                .FirstOrDefaultAsync(m => m.EspecialidadId == id);
            if (especialidad == null)
            {
                return NotFound();
            }

            return View(especialidad);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Especialidad == null)
            {
                return Problem("Entity set 'DbContext.Especialidad'  is null.");
            }
            var especialidad = await _context.Especialidad.FindAsync(id);
            if (especialidad != null)
            {
                _context.Especialidad.Remove(especialidad);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EspecialidadExists(int id)
        {
            return _context.Especialidad.Any(e => e.EspecialidadId == id);
        }
    }
}
