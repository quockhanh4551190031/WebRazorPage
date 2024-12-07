using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WebRazorPage.Data;
using WebRazorPage.Models;

namespace WebRazorPage.Pages
{
    public class DetailsModel : PageModel
    {
        private readonly WebRazorPage.Data.AppDbContext _context;

        public DetailsModel(WebRazorPage.Data.AppDbContext context)
        {
            _context = context;
        }

        public Product Product { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products.Include(p => p.Category).FirstOrDefaultAsync(m => m.ID == id);
            if (product == null)
            {
                return NotFound();
            }
            else
            {
                Product = product;
            }
            return Page();
        }
    }
}
