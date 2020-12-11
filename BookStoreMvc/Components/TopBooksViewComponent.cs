using BookStoreMvc.Repository;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStoreMvc.Components
{
    public class TopBooksViewComponent : ViewComponent
    {
        private readonly BookRepository bookRepository;

        public TopBooksViewComponent(BookRepository bookRepository)
        {
            this.bookRepository = bookRepository;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var books = await bookRepository.GetTopBooksAsync();
            return View(books);
        }
    }
}
