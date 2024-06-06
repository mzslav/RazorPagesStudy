using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using RazorPagesStudy.Models;
using RazorPagesStudy.Services;
using System.IO;

namespace RazorPagesGeneral.Pages.Books
{
    public class EditModel : PageModel
    {
        private readonly IBookRepository _bookRepository;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public EditModel(IBookRepository bookRepository, IWebHostEnvironment webHostEnvironment)
        {
            _bookRepository = bookRepository;
            _webHostEnvironment = webHostEnvironment;
        }

        [BindProperty]
        public Book Book { get; set; }

        [BindProperty]
        public IFormFile Photo { get; set; }

        public IActionResult OnGet(int? id)
        {
            if (id.HasValue)
            {
                Book = _bookRepository.GetBook(id.Value);
            }
            else
            {
                Book = new Book();
            }

            if (Book == null)
            {
                return RedirectToPage("/NotFound");
            }
            return Page();
        }

        public IActionResult OnPost()
        {
            if (ModelState.IsValid)
            {
                if (Photo != null)
                {
                    
                    if (Book.PhotoPath != null)
                    {
                        string filePath = Path.Combine(_webHostEnvironment.WebRootPath, "images", Book.PhotoPath);
                        if (Book.PhotoPath != "noimage.png")
                        System.IO.File.Delete(filePath);
                    }

                    
                    Book.PhotoPath = ProcessUploadFile();
                }

                if (Book.Id > 0)
                {
                    Book = _bookRepository.Update(Book);
                    TempData["SuccessMessage"] = $"Aktulizacja {Book.Name} udana!";
                }
                else
                {
                    Book = _bookRepository.Add(Book);
                    TempData["SuccessMessage"] = $"Dodawanie {Book.Name} udane!";
                }

                return RedirectToPage("Books");
            }

            return Page();
        }

        private string ProcessUploadFile()
        {
            string uniqueFileName = null;

            if (Photo != null)
            {
                string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "images");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + Photo.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    Photo.CopyTo(fileStream);
                }
            }

            return uniqueFileName;
        }
    }
}
