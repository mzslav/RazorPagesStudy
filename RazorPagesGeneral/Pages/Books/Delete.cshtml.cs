using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorPagesStudy.Models;
using RazorPagesStudy.Services;

namespace RazorPagesGeneral.Pages.Books
{
    public class DeleteModel : PageModel
    {
        private readonly IBookRepository _bookRepository;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public DeleteModel(IBookRepository bookRepository, IWebHostEnvironment webHostEnvironment)
        {
            _bookRepository = bookRepository;
            _webHostEnvironment = webHostEnvironment;
        }

        public Book Book { get; set; }

        public IActionResult OnGet(int id)
        {
            Book = _bookRepository.GetBook(id);
            if (Book == null)
                return RedirectToPage("/NotFound");

            return Page();
        }

        public IActionResult OnPost(int id)
        {
            Book deletedBook = _bookRepository.Delete(id);
            if (deletedBook == null)
                return RedirectToPage("/NotFound");

            if (!string.IsNullOrEmpty(deletedBook.PhotoPath) && deletedBook.PhotoPath != "noimage.png")
            {
                string filePath = Path.Combine(_webHostEnvironment.WebRootPath, "images", deletedBook.PhotoPath);
                if (System.IO.File.Exists(filePath))
                {
                    System.IO.File.Delete(filePath);
                }
            }

            return RedirectToPage("Books");
        }
    }
}
