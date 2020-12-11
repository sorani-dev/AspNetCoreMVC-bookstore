using BookStoreMvc.Data;
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
        private readonly BookStoreContext _context = null;

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
                LanguageId = model.LanguageId,
                TotalPages = model.TotalPages.HasValue ? model.TotalPages.Value : 0,
                Description = model.Description,
                CreatedOn = DateTime.UtcNow,
                UpdatedOn = DateTime.UtcNow,
                CoverImageUrl = model.CoverImageUrl,
                BookPdfUrl = model.BookPdfUrl,
            };

            newBook.bookGallery = new List<BookGallery>();

            foreach (var file in model.Gallery)
            {
                newBook.bookGallery.Add(new BookGallery()
                {
                    Name = file.Name,
                    URL = file.URL,
                });
            }

            await _context.Books.AddAsync(newBook);
            await _context.SaveChangesAsync();

            return newBook.Id;
        }

        public async Task<List<BookModel>> GetAllBooks()
        {
            return await _context.Books
                .Select(book => new BookModel()
                    {
                        Author = book.Author,
                        Category = book.Category,
                        Description = book.Description,
                        Id = book.Id,
                        LanguageId = book.LanguageId,
                        Language = book.Language.Name,
                        Title= book.Title,
                        TotalPages= book.TotalPages,
                        CoverImageUrl = book.CoverImageUrl,
                    }).ToListAsync();
        }
        public async Task<BookModel> GetBookById(int id)
        {
            return await _context.Books.Where(x => x.Id == id)
                .Select(book => new BookModel()
                {
                    Author = book.Author,
                    Category = book.Category,
                    Description = book.Description,
                    Id = book.Id,
                    LanguageId = book.LanguageId,
                    Language = book.Language.Name,
                    Title = book.Title,
                    TotalPages = book.TotalPages,
                    CoverImageUrl = book.CoverImageUrl,
                    Gallery = book.bookGallery.Select(g => new GalleryModel()
                    {
                        Id = g.Id,
                        Name = g.Name,
                        URL = g.URL
                    }).ToList(),
                    BookPdfUrl = book.BookPdfUrl,
                }).FirstOrDefaultAsync();
        }
        public List<BookModel> SearchBook(string title, string authorName)
        {
            //return DataSource().Where(x => (title != null && x.Title.Contains(title)) || (authorName != null && x.Author.Contains(authorName))).ToList();
            return null;
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
