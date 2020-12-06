using BookStoreMvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStoreMvc.Repository
{
    public class BookRepository
    {
        public List<BookModel> GetAllBooks()
        {
            return DataSource();
        }
        public BookModel GetBookById(int id)
        {
            return DataSource().Where(x => x.Id == id).FirstOrDefault();
        }
        public List<BookModel> SearchBook(string title, string authorName)
        {
            return DataSource().Where(x => (title != null && x.Title.Contains(title)) || (authorName != null && x.Author.Contains(authorName))).ToList();
        }

        private List<BookModel> DataSource()
        {
            return new List<BookModel>()
            {
                new BookModel(){Id=1, Title="MVC", Author="John Doe"},
                new BookModel(){Id=2, Title="ADR", Author="Jane Doe"},
                new BookModel(){Id=3, Title="C#", Author="John Smith"},
                new BookModel(){Id=4, Title="Java", Author="Random Name"},
                new BookModel(){Id=5, Title="Php 8", Author="The no name Team"},
            };
        }
    }
}
