using BookStoreMvc.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookStoreMvc.Repository
{
    public interface IBookRepository
    {
        Task<int> AddNewBook(BookModel model);
        Task<List<BookModel>> GetAllBooks();
        Task<BookModel> GetBookById(int id);
        Task<List<BookModel>> GetTopBooksAsync(int maxBooks);
        List<BookModel> SearchBook(string title, string authorName);
    }
}