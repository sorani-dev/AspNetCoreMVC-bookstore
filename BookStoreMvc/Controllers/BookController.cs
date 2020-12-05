using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStoreMvc.Controllers
{
    public class BookController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public string GetAllBooks()
        {
            return "All Books";
        }

        public string GetBook(int id)
        {
            return $"Book with id = {id}";
        }

        public string SearchBooks(string bookName, string authorName)
        {
            return $"Book with name: {bookName} and author: {authorName}";
        }
    }
}
