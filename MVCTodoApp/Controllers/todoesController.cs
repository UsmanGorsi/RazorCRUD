using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MVCTodoApp.DTOs;
using MVCTodoApp.Models;

namespace MVCTodoApp.Controllers
{
    public class todoesController : Controller
    {
        private readonly todoDBContext _context;

        public todoesController(todoDBContext context)
        {
            _context = context;
        }

        // GET: todoes
        public async Task<IActionResult> Index()
        {
            return View(await _context.todos.ToListAsync());
        }

        // GET: todoes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var todo = await _context.todos
                .FirstOrDefaultAsync(m => m.id == id);
            if (todo == null)
            {
                return NotFound();
            }

            return View(todo);
        }

        // GET: todoes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: todoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create([Bind("id,Name")] todo todo)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        _context.Add(todo);
        //        await _context.SaveChangesAsync();
        //        return RedirectToAction(nameof(Index));
        //    }
        //    return View(todo);
        //}
        public async Task<IActionResult> Create([Bind("id,Name")] todo todo) {
            try {
                _context.Add(todo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            } catch {
                return View(todo);
            }
        }

        // GET: todoes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var todo = await _context.todos.FindAsync(id);
            if (todo == null)
            {
                return NotFound();
            }
            return View(todo);
        }

        // POST: todoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,Name")] todo todo)
        {
            if (id != todo.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(todo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!todoExists(todo.id))
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
            return View(todo);
        }

        // GET: todoes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var todo = await _context.todos
                .FirstOrDefaultAsync(m => m.id == id);
            if (todo == null)
            {
                return NotFound();
            }

            return View(todo);
        }

        // POST: todoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var todo = await _context.todos.FindAsync(id);
            _context.todos.Remove(todo);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool todoExists(int id)
        {
            return _context.todos.Any(e => e.id == id);
        }
    }
}
