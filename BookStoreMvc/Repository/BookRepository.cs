﻿using BookStoreMvc.Models;
using System.Collections.Generic;
using System.Linq;

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
                new BookModel(){Id=1, Title="MVC", Author="John Doe", Description="This is the description of the MVC book" },
                new BookModel(){Id=2, Title="ADR", Author="Jane Doe", Description="This is the description of the ADR book" },
                new BookModel(){Id=3, Title="C#", Author="John Smith", Description="This is the description of the C# book" },
                new BookModel(){Id=4, Title="Java", Author="Random Name", Description="This is the description of the Java book" },
                new BookModel(){Id=5, Title="Php 8", Author="The no name Team", Description="This is the description of the PHP 8 book" },
                new BookModel(){Id=6, Title="Azure DevOps", Author="Azure Team", Description="This is the description of the Azure DevOps book" },
            };
        }
    }
}