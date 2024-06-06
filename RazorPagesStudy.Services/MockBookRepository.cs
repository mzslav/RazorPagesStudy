using RazorPagesStudy.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace RazorPagesStudy.Services
{
    public class MockBookRepository : IBookRepository
    {

        private List<Book> _bookList;

        public MockBookRepository()
        {
            _bookList = new List<Book>()
            {
                new Book()
                {
                    Id = 0, Name = "Harry Potter", Author = "J. K. Rownling", 
                    PhotoPath = "harrypotter.png", Description = "Storrry about boy", Department = Dept.Fantazja
                },
                
                new Book()
                {
                    Id = 1, Name = "Pes Patron", Author = "Donbass",
                    PhotoPath = "pespatron.png", Description = "Badaboom", Department = Dept.Grozy
                },
                 
                new Book()
                {
                    Id = 2, Name = "Thomas Shelby", Author = "Birninghem",
                    PhotoPath = "tomas.png", Description = "SigmaMovie", Department = Dept.Powieść
                },
                    
                new Book()
                {
                    Id = 3, Name = "Peremogabude", Author = "ZhyZha",
                    PhotoPath = "peremoga.png", Description = "15majperemoha", Department = Dept.Detektyw
                },

            };
        }

        public Book Add(Book newBook)
        {
            newBook.Id = _bookList.Max(x => x.Id) + 1;
            _bookList.Add(newBook);
            return newBook;
        }

        public IEnumerable<DeptHeadCount> BookCountByDept(Dept? dept)
        {
            IEnumerable<Book> query = _bookList; 
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
            Book bookToDelete = _bookList.FirstOrDefault(x => x.Id == id);

            if (bookToDelete != null)        
                _bookList.Remove(bookToDelete);

            return bookToDelete;
            
        }

        public IEnumerable<Book> GetAllBooks()
        {
            return _bookList;
        }

        public Book GetBook(int id)
        {
            return _bookList.FirstOrDefault(x => x.Id == id);
        }

		public IEnumerable<Book> Search(string searchTerm)
		{
			if (string.IsNullOrWhiteSpace(searchTerm))
                return _bookList;

            return _bookList.Where(x => x.Name.ToLower().Contains(searchTerm.ToLower()) || x.Author.Contains(searchTerm.ToLower()));
		}

		public Book Update(Book updatedBook)
        {
            Book book = _bookList.FirstOrDefault(x => x.Id == updatedBook.Id);

            if (book != null)
            {
                book.Name = updatedBook.Name;
                book.Author = updatedBook.Author;
                book.Author = updatedBook.Author;
                book.PhotoPath = updatedBook.PhotoPath;
                book.Description = updatedBook.Description;
            }
            return book;
        }
    }
}
