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

        public BookModel GetBook(int id)
        {
            return _bookRepository.GetBookById(id);
        }

        public List<BookModel> SearchBooks(string bookName, string authorName)
        {
            return _bookRepository.SearchBook(bookName, authorName); // $"Book with name: {bookName} and author: {authorName}";
        }
    }
}
