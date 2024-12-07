using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebRazorPage.Data;
using WebRazorPage.Models;

namespace WebRazorPage.Pages
{
    public class CreateModel : PageModel
    {
        private readonly AppDbContext _context;

        public CreateModel(AppDbContext context)
        {
            _context = context;
        
        }

        [BindProperty]
        public Product Product { get; set; }
        public SelectList Categories { get; set; }

        //[BindProperty]
        //public List<IFormFile> Images { get; set; }
        public IActionResult OnGet()
        {
            Categories = new SelectList(_context.Categories, "CategoryId", "CategoryName");
            return Page();
        }


        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                Categories = new SelectList(_context.Categories, "CategoryId", "CategoryName");
                return Page();
            }

            //// Kiểm tra thư mục images 
            //var imagesPath = Path.Combine(_environment.WebRootPath, "images");
            //if (!Directory.Exists(imagesPath))
            //{
            //    Directory.CreateDirectory(imagesPath);
            //}

            //var imagePaths = new List<string>();
            //foreach (var image in Images)
            //{
            //    if (image.Length > 0)
            //    {
            //        var filePath = Path.Combine(imagesPath, image.FileName);
            //        using (var stream = new FileStream(filePath, FileMode.Create))
            //        {
            //            await image.CopyToAsync(stream);
            //        }
            //        imagePaths.Add("/images/" + image.FileName);
            //    }
            //}
            ////Lưu danh sách đường dẫn ảnh vào Product
            //Product.ImagePath = imagePaths;


            _context.Products.Add(Product);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
