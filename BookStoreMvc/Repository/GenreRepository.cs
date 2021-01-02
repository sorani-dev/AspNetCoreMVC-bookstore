using BookStoreMvc.Data;
using BookStoreMvc.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace BookStoreMvc.Repository
{
    public class GenreRepository : IGenreRepository
    {
        private readonly BookStoreContext context = null;

        public GenreRepository(BookStoreContext context)
        {
            this.context = context;
        }

        public async Task<List<GenreModel>> GetGenres()
        {
            return await context.Genres.Select(x => new GenreModel()
            {
                Id = x.Id,
                Name = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(x.Name)
            }).ToListAsync();
        }
    }
}
