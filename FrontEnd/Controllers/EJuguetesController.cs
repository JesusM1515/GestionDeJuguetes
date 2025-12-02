using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Domain.Entities;
using Infraestructure.ContextDB;

namespace FrontEnd.Controllers
{
    public class EJuguetesController : Controller
    {
        private readonly Context _context;

        public EJuguetesController(Context context)
        {
            _context = context;
        }

        // GET: EJuguetes
        public async Task<IActionResult> Index(string searchString)
        {
            // Mantener el valor de búsqueda en la vista
            ViewData["CurrentFilter"] = searchString;

            var juguetes = from j in _context.DimJuguetes
                           select j;

            if (!String.IsNullOrEmpty(searchString))
            {
                juguetes = juguetes.Where(j =>
                    j.Nombre.Contains(searchString) ||
                    j.ID_Juguete.ToString().Contains(searchString)
                );
            }

            return View(await juguetes.ToListAsync());
        }

        // GET: EJuguetes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
                return NotFound();

            var eJuguetes = await _context.DimJuguetes
                .FirstOrDefaultAsync(m => m.Key_Juguetes == id);

            if (eJuguetes == null)
                return NotFound();

            return View(eJuguetes);
        }

        // GET: EJuguetes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: EJuguetes/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Key_Juguetes,ID_Juguete,Nombre,Categoria,Tipo,Precio,Stock,Edad,Descripcion")] EJuguetes eJuguetes)
        {
            if (ModelState.IsValid)
            {
                _context.Add(eJuguetes);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(eJuguetes);
        }

        // GET: EJuguetes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return NotFound();

            var eJuguetes = await _context.DimJuguetes.FindAsync(id);
            if (eJuguetes == null)
                return NotFound();

            return View(eJuguetes);
        }

        // POST: EJuguetes/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Key_Juguetes,ID_Juguete,Nombre,Categoria,Tipo,Precio,Stock,Edad,Descripcion")] EJuguetes eJuguetes)
        {
            if (id != eJuguetes.Key_Juguetes)
                return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(eJuguetes);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EJuguetesExists(eJuguetes.Key_Juguetes))
                        return NotFound();
                    else
                        throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(eJuguetes);
        }

        // GET: EJuguetes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return NotFound();

            var eJuguetes = await _context.DimJuguetes
                .FirstOrDefaultAsync(m => m.Key_Juguetes == id);

            if (eJuguetes == null)
                return NotFound();

            return View(eJuguetes);
        }

        // POST: EJuguetes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var eJuguetes = await _context.DimJuguetes.FindAsync(id);
            if (eJuguetes != null)
                _context.DimJuguetes.Remove(eJuguetes);

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EJuguetesExists(int id)
        {
            return _context.DimJuguetes.Any(e => e.Key_Juguetes == id);
        }
    }
}
