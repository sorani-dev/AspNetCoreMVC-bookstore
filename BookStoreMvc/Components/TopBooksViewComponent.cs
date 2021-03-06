﻿using BookStoreMvc.Repository;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace BookStoreMvc.Components
{
    public class TopBooksViewComponent : ViewComponent
    {
        private readonly IBookRepository bookRepository;

        public TopBooksViewComponent(IBookRepository bookRepository)
        {
            this.bookRepository = bookRepository;
        }

        public async Task<IViewComponentResult> InvokeAsync(int count)
        {
            var books = await bookRepository.GetTopBooksAsync(count);
            return View(books);
        }
    }
}
