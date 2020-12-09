﻿using BookStoreMvc.Data;
using BookStoreMvc.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStoreMvc.Repository
{
    public class BookRepository
    {
        private BookStoreContext _context = null;

        public BookRepository(BookStoreContext context)
        {
            this._context = context;
        }

        public async Task<int> AddNewBook(BookModel model)
        {
            var newBook = new Book()
            {
                Author = model.Author,
                Title = model.Title,
                TotalPages = model.TotalPages,
                Description = model.Description,
                CreatedOn = DateTime.UtcNow,
                UpdatedOn = DateTime.UtcNow,
            };

            await _context.Books.AddAsync(newBook);
            await _context.SaveChangesAsync();

            return newBook.Id;
        }

        public async Task<List<BookModel>> GetAllBooks()
        {
            var books = new List<BookModel>();
            var allbooks = await _context.Books.ToListAsync();

            if (allbooks?.Any()==true)
            {
                foreach (var book in allbooks)
                {
                    books.Add(new BookModel()
                    {
                        Author = book.Author,
                        Category = book.Category,
                        Description = book.Description,
                        Id = book.Id,
                        Language= book.Language,
                        Title= book.Title,
                        TotalPages= book.TotalPages,
                    });
                }
            }
            return books;
        }
        public async Task<BookModel> GetBookById(int id)
        {
            var book = await _context.Books.FindAsync(id);

            //await _context.Books.Where(x => x.Id == id).FirstOrDefaultAsync();

            if (book != null)
            {
                var bookDetails = new BookModel()
                {
                    Author = book.Author,
                    Category = book.Category,
                    Description = book.Description,
                    Id = book.Id,
                    Language = book.Language,
                    Title = book.Title,
                    TotalPages = book.TotalPages,
                };
                return bookDetails;
            }
            return null;
        }
        public List<BookModel> SearchBook(string title, string authorName)
        {
            return DataSource().Where(x => (title != null && x.Title.Contains(title)) || (authorName != null && x.Author.Contains(authorName))).ToList();
        }

        private List<BookModel> DataSource()
        {
            return new List<BookModel>()
            {
                new BookModel(){Id=1, Title="MVC", Author="John Doe", Description="This is the description of the MVC book", Category="Programming", Language="English", TotalPages=307, },
                new BookModel(){Id=2, Title="ADR", Author="Jane Doe", Description="This is the description of the ADR book", Category="Design Patterns", Language="English", TotalPages=78, },
                new BookModel(){Id=3, Title="C#", Author="John Smith", Description="This is the description of the C# book", Category="Programming", Language="English", TotalPages=201, },
                new BookModel(){Id=4, Title="Java", Author="Random Name", Description="This is the description of the Java book", Category="Concept", Language="Spanish", TotalPages=278, },
                new BookModel(){Id=5, Title="Php 8", Author="The no name Team", Description="This is the description of the PHP 8 book", Category="Programming", Language="English", TotalPages=408, },
                new BookModel(){Id=6, Title="Azure DevOps", Author="Azure Team", Description="This is the description of the Azure DevOps book", Category="DevOps", Language="French", TotalPages=701, },
            };
        }
    }
}
