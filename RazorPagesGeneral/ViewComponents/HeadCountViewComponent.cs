using Microsoft.AspNetCore.Mvc;
using RazorPagesStudy.Models;
using RazorPagesStudy.Services;

namespace RazorPagesGeneral.ViewComponents
{
    public class HeadCountViewComponent : ViewComponent
    {
        private readonly IBookRepository _bookRepository;

        public HeadCountViewComponent(IBookRepository bookRepository)
        {
           _bookRepository = bookRepository;
        }

        public IViewComponentResult Invoke(Dept? department = null)
        {
            var result = _bookRepository.BookCountByDept(department);
            return View(result);
        }


    }
}
