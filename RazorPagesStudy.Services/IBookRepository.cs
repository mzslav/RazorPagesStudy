using RazorPagesStudy.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RazorPagesStudy.Services
{
    public interface IBookRepository
    {
        IEnumerable<Book> Search(string searchTerm);
        IEnumerable<Book> GetAllBooks();
        Book GetBook(int id);
        Book Update(Book updatedBook);
        Book Add(Book newBook);
        Book Delete(int id);
        IEnumerable<DeptHeadCount> BookCountByDept(Dept? dept);
    }
}
