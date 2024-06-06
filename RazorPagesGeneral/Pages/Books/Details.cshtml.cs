using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorPagesStudy.Models;
using RazorPagesStudy.Services;

namespace RazorPagesGeneral.Pages.Books
{
    public class DetailsModel : PageModel
    {
        private readonly IBookRepository _bookRepository;

        public DetailsModel(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        public Book Book { get; private set; }

		public IActionResult OnGet(int id)
		{
			Book = _bookRepository.GetBook(id);

			if (Book == null)
			{
				return RedirectToPage("/NotFound");
			}
			return Page();
		}


	}
}
