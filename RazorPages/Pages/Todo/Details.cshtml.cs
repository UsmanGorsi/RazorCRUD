using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RazorPages.Data;
using RazorPages.Models;

namespace RazorPages.Pages.Todo
{
    public class DetailsModel : PageModel
    {
        private readonly RazorPages.Data.RazorPagesContext _context;

        public DetailsModel(RazorPages.Data.RazorPagesContext context)
        {
            _context = context;
        }

        public todo todo { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            todo = await _context.todo.FirstOrDefaultAsync(m => m.ID == id);

            if (todo == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
