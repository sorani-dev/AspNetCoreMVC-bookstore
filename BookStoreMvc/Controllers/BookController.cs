using BookStoreMvc.Models;
using BookStoreMvc.Repository;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace BookStoreMvc.Controllers
{
    public class BookController : Controller
    {
        private readonly BookRepository _bookRepository = null;

        public BookController()
        {
            _bookRepository = new BookRepository();
        }

        public IActionResult Index()
        {
            return View();
        }

        public ViewResult GetAllBooks()
        {
            var data = _bookRepository.GetAllBooks();

            return View(data);
        }

        [Route("book-details/{id}", Name = "bookDetailsRoute")]
        public ViewResult GetBook(int id)
        {
            BookModel data = _bookRepository.GetBookById(id);
            return View(data);
        }

        public List<BookModel> SearchBooks(string bookName, string authorName)
        {
            return _bookRepository.SearchBook(bookName, authorName); // $"Book with name: {bookName} and author: {authorName}";
        }
    }
}
