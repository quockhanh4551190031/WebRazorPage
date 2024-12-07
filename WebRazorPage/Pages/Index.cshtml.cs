using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebRazorPage.Data;
using WebRazorPage.Models;

namespace WebRazorPage.Pages
{
    public class IndexModel : PageModel
    {
        private readonly AppDbContext _context;

        public IndexModel(AppDbContext context)
        {
            _context = context;
        }

        public IList<Product> Product { get;set; } = default!;  
        
        public IList<Category> Category {  get;set; } = default!;

        [BindProperty(SupportsGet = true)]
        public string? SearchQuery { get; set; }

        public SelectList? Categories { get; set; }

        [BindProperty(SupportsGet = true)]
        public string? ProductCategories {  get; set; }

        public async Task OnGetAsync(int? Id)
        {
            //Product = await _context.Products
            //    .Include(p => p.Category).ToListAsync();

            //<snippet_search_linqQuery>
            IQueryable<string> categoryQuery = from c in _context.Categories
                                               orderby c.CategoryId
                                               select c.CategoryName;

            //</snippet_search_linqQuery>

            var products = from p in _context.Products.Include(p => p.Category)
                           select p;

            var categories = from c in _context.Categories
                             select c;

            if (!string.IsNullOrEmpty(SearchQuery))
            {
                products = products.Where(p => p.Name.Contains(SearchQuery));
            }

            if(!string.IsNullOrEmpty(ProductCategories))
            {
                products = products.Where(p => p.Category != null && p.Category.CategoryName == ProductCategories);
            }

            Categories = new SelectList(await categoryQuery.Distinct().ToListAsync());

            Product = await products.ToListAsync();

        }
    }
}
