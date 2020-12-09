using BookStoreMvc.Models;
using BookStoreMvc.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStoreMvc.Controllers
{
    public class BookController : Controller
    {
        private readonly BookRepository _bookRepository = null;

        public BookController(BookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<ViewResult> GetAllBooks()
        {
            var data = await _bookRepository.GetAllBooks();

            return View(data);
        }

        [Route("book-details/{id}", Name = "bookDetailsRoute")]
        public async Task<ViewResult> GetBookAsync(int id)
        {
            BookModel data = await _bookRepository.GetBookById(id);
            return View(data);
        }

        public List<BookModel> SearchBooks(string bookName, string authorName)
        {
            return _bookRepository.SearchBook(bookName, authorName); // $"Book with name: {bookName} and author: {authorName}";
        }

        public ViewResult AddNewBook(bool isSuccess = false, int bookId = 0)
        {
            var model = new BookModel()
            {
                Language = "2",
            };

            //ViewBag.Language = GetLanguages().Select(x => new SelectListItem()
            //{
            //    Text = x.Name,
            //    Value = x.Id.ToString(),
            //}).ToList();

            var group1 = new SelectListGroup() { Name = "Group 1" };
            var group2 = new SelectListGroup() { Name = "Group 2", Disabled=true };
            var group3 = new SelectListGroup() { Name = "Group 3" };

            ViewBag.Language = new List<SelectListItem>() {
                new SelectListItem(){Text="English", Value="1", Group=group1 },
                new SelectListItem(){Text="French", Value="2",Group=group1 },
                new SelectListItem(){Text="Spanish", Value="3" , Group=group1 },
                new SelectListItem(){Text="Chinese", Value="4", Group=group2 },
                new SelectListItem(){Text="Dutch", Value="5", Group=group3 },
                new SelectListItem(){Text="Japanese", Value="6",  Group=group2},
                new SelectListItem(){Text="Greek", Value="7",  Group=group3},
            };

            ViewBag.IsSuccess = isSuccess;
            ViewBag.BookId = bookId;
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AddNewBook(BookModel bookModel)
        {
            if (ModelState.IsValid)
            {
                int id = await _bookRepository.AddNewBook(bookModel);
                if (id > 0)
                {
                    return RedirectToAction(nameof(AddNewBook), new { isSuccess = true, bookId = id });
                }
            }


            ViewBag.Language = GetLanguages();

            return View();
        }

        private List<LanguageModel> GetLanguages()
        {
            return new List<LanguageModel>()
            {
                new LanguageModel() {Id = 1, Name="English"},
                new LanguageModel() {Id = 2, Name="French"},
                new LanguageModel() {Id = 3, Name="Spanish"},
            };
        }     
    }
}
