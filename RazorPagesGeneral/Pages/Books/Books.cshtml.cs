using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorPagesStudy.Models;
using RazorPagesStudy.Services;

namespace RazorPagesGeneral.Pages.Books
{
    public class BooksModel : PageModel
    {
        private readonly IBookRepository _db;

        public BooksModel(IBookRepository db)
        {
            _db = db;
        }

        public IEnumerable<Book> Books { get; set; }

		[BindProperty(SupportsGet = true)]
		public string SearchTerm { get; set; }

        public void OnGet()
        {
			SearchTerm = SearchTerm ?? string.Empty;

			Books = _db.Search(SearchTerm);

        }
    }
}
