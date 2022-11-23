using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
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
        private readonly TurnosContext _context;

        public TurnosController(TurnosContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            ViewData["EspecialidadId"] = new SelectList(_context.Especialidad, "EspecialidadId", "Nombre");
            return View(await _context.Turno.Include(m => m.Medico).ToListAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Turno == null)
            {
                return NotFound();
            }

            var turno = await _context.Turno.Include(m => m.Medico).Include(m => m.Usuario).FirstOrDefaultAsync(m => m.TurnoId == id);
            if (turno == null)
            {
                return NotFound();
            }

            return View(turno);
        }

        [Authorize(Roles = "Administrador, Supervisor")]
        public IActionResult Create()
        {
            ViewData["MedicoId"] = new SelectList(_context.Medico, "MedicoId", "Nombre");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TurnoId,Fecha,MedicoId")] Turno turno)
        {
            if (ModelState.IsValid)
            {
                _context.Add(turno);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(turno);
        }

        [Authorize(Roles = "Administrador, Supervisor")]
        public async Task<IActionResult> Edit(int? id)
        {
            ViewData["MedicoId"] = new SelectList(_context.Medico, "MedicoId", "Nombre");
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("TurnoId,Fecha,Usuario")] Turno turno)
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

        [Authorize(Roles = "Administrador, Supervisor")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Turno == null)
            {
                return NotFound();
            }

            var turno = await _context.Turno.FirstOrDefaultAsync(m => m.TurnoId == id);
            if (turno == null)
            {
                return NotFound();
            }

            return View(turno);
        }

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

        [HttpPost, ActionName("Index")]
        public async Task<IActionResult> ListarTurnos(int especialidades)
        {
            var turnos = await _context.Turno.Include(m => m.Medico).ToListAsync();
            ViewData["EspecialidadId"] = new SelectList(_context.Especialidad, "EspecialidadId", "Nombre");

            if (especialidades != 0)
            {
                turnos = turnos.Where(e => e.Medico.EspecialidadId == especialidades).ToList();
            }

            return View("Index", turnos);
        }

        public async Task<IActionResult> Reservar(int? id)
        {
            if (id == null || _context.Turno == null)
            {
                return NotFound();
            }

            var turno = await _context.Turno.Include(m => m.Medico).FirstOrDefaultAsync(m => m.TurnoId == id);
            if (turno == null)
            {
                return NotFound();
            }
            return View(turno);
        }
        [HttpPost]
        public async Task<IActionResult> Reservar(int id)
        {            
            int usuarioId = Int32.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var usuario = await _context.Usuario.FirstOrDefaultAsync(u => u.UsuarioId == usuarioId);
            var turno = await _context.Turno.FindAsync(id);

            if (id != turno.TurnoId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    turno.Usuario = usuario;
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

        public async Task<IActionResult> TurnosPaciente()
        {            
            int usuarioId = Int32.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

            var turnos = await _context.Turno.Include(m => m.Medico).ToListAsync();
            turnos = turnos.Where(t => t.UsuarioId == usuarioId).ToList();

            return View(turnos);
        }
    }
}
