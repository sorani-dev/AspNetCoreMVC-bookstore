using BookStoreMvc.Models;
using BookStoreMvc.Repository;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace BookStoreMvc.Controllers
{
    public class BookController : Controller
    {
        private readonly BookRepository _bookRepository = null;
        private readonly LanguageRepository languageRepository;
        private readonly IWebHostEnvironment webHostEnvironment;

        public BookController(BookRepository bookRepository, LanguageRepository languageRepository, IWebHostEnvironment webHostEnvironment)
        {
            _bookRepository = bookRepository;
            this.languageRepository = languageRepository;
            this.webHostEnvironment = webHostEnvironment;
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

        public async Task<ViewResult> AddNewBook(bool isSuccess = false, int bookId = 0)
        {
            var model = new BookModel()
            {
                //Language = "2",
            };

            //ViewBag.Language = GetLanguages().Select(x => new SelectListItem()
            //{
            //    Text = x.Name,
            //    Value = x.Id.ToString(),
            //}).ToList();

            //ViewBag.Language = new List<SelectListItem>() {
            //    new SelectListItem(){Text="English", Value="1"},
            //    new SelectListItem(){Text="French", Value="2"},
            //    new SelectListItem(){Text="Spanish", Value="3"},
            //    new SelectListItem(){Text="Chinese", Value="4"},
            //    new SelectListItem(){Text="Dutch", Value="5"},
            //    new SelectListItem(){Text="Japanese", Value="6"},
            //    new SelectListItem(){Text="Greek", Value="7"},
            //};
            ViewBag.Language = new SelectList(await languageRepository.GetLanguages(), "Id", "Name");

            ViewBag.IsSuccess = isSuccess;
            ViewBag.BookId = bookId;
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AddNewBook(BookModel bookModel)
        {
            if (ModelState.IsValid)
            {
                if (bookModel.CoverPhoto != null)
                {
                    string folder = "books/cover/";
                    folder += Guid.NewGuid().ToString() + "_" + bookModel.CoverPhoto.FileName;
                    string serverFolder = Path.Combine(webHostEnvironment.WebRootPath, folder);

                    await bookModel.CoverPhoto.CopyToAsync(new FileStream(serverFolder, FileMode.Create));
                }

                int id = await _bookRepository.AddNewBook(bookModel);
                if (id > 0)
                {
                    return RedirectToAction(nameof(AddNewBook), new { isSuccess = true, bookId = id });
                }
            }

            ViewBag.Language = new SelectList(await languageRepository.GetLanguages(), "Id", "Name");

            return View();
        }
    }
}
