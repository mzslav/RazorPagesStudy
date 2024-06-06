using RazorPagesStudy.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RazorPagesStudy.Services
{
	public class SQLBookRepository : IBookRepository
	{
		private readonly AppDbContext _context;

		public SQLBookRepository(AppDbContext context)
        {
			_context = context;
		}

        public Book Add(Book newBook)
		{
			_context.Books.Add(newBook);
			_context.SaveChanges();
			return newBook;	
		}

		public IEnumerable<DeptHeadCount> BookCountByDept(Dept? dept)
		{
			IEnumerable<Book> query = _context.Books;
			if (dept.HasValue)
			{
				query = query.Where(x => x.Department == dept.Value);
			}

			return query.GroupBy(x => x.Department)
				 .Select(x => new DeptHeadCount()
				 {
					 Department = x.Key.Value,
					 Count = x.Count()
				 }).ToList();
		}

		public Book Delete(int id)
		{
			var bookToDelete = _context.Books.Find(id);
			if (bookToDelete != null)
			{
				_context.Books.Remove(bookToDelete);
				_context.SaveChanges();
			}
			return bookToDelete;
		}

		public IEnumerable<Book> GetAllBooks()
		{
			return _context.Books;
		}

		public Book GetBook(int id)
		{
			return _context.Books.Find(id);
		}

		public IEnumerable<Book> Search(string searchTerm)
		{
			if (string.IsNullOrWhiteSpace(searchTerm))
				return _context.Books;

			return _context.Books.Where(x => x.Name.ToLower().Contains(searchTerm.ToLower()) || x.Author.Contains(searchTerm.ToLower()));
		
	    }

		public Book Update(Book updatedBook)
		{
			var book = _context.Books.Attach(updatedBook);
			book.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
			_context.SaveChanges();
			return updatedBook;
		}
	}
}
