using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Domain.Entities;
using Infraestructure.ContextDB;

namespace FrontEnd.Controllers
{
    public class EUsuariosController : Controller
    {
        private readonly Context _context;

        public EUsuariosController(Context context)
        {
            _context = context;
        }

        // GET: EUsuarios
        public async Task<IActionResult> Index()
        {
            return View(await _context.DimUsuarios.ToListAsync());
        }

        // GET: EUsuarios/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var eUsuarios = await _context.DimUsuarios
                .FirstOrDefaultAsync(m => m.Key_Usuarios == id);
            if (eUsuarios == null)
            {
                return NotFound();
            }

            return View(eUsuarios);
        }

        // GET: EUsuarios/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: EUsuarios/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Key_Usuarios,Nombre,Correo,Password")] EUsuarios eUsuarios)
        {
            if (ModelState.IsValid)
            {
                _context.Add(eUsuarios);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(eUsuarios);
        }

        // GET: EUsuarios/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var eUsuarios = await _context.DimUsuarios.FindAsync(id);
            if (eUsuarios == null)
            {
                return NotFound();
            }
            return View(eUsuarios);
        }

        // POST: EUsuarios/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Key_Usuarios,Nombre,Correo,Password")] EUsuarios eUsuarios)
        {
            if (id != eUsuarios.Key_Usuarios)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(eUsuarios);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EUsuariosExists(eUsuarios.Key_Usuarios))
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
            return View(eUsuarios);
        }

        // GET: EUsuarios/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var eUsuarios = await _context.DimUsuarios
                .FirstOrDefaultAsync(m => m.Key_Usuarios == id);
            if (eUsuarios == null)
            {
                return NotFound();
            }

            return View(eUsuarios);
        }

        // POST: EUsuarios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var eUsuarios = await _context.DimUsuarios.FindAsync(id);
            if (eUsuarios != null)
            {
                _context.DimUsuarios.Remove(eUsuarios);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EUsuariosExists(int id)
        {
            return _context.DimUsuarios.Any(e => e.Key_Usuarios == id);
        }
    }
}
