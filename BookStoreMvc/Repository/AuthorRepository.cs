using BookStoreMvc.Data;
using BookStoreMvc.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStoreMvc.Repository
{
    public class AuthorRepository : IAuthorRepository
    {
        private readonly BookStoreContext context;

        public AuthorRepository(BookStoreContext context)
        {
            this.context = context;
        }

        public async Task<List<AuthorModel>> GetAuthors()
        {
            return await context.Authors.Select(x => new AuthorModel()
            {
                Id = x.Id,
                Name = x.FirstName + " " + x.LastName,
                FirstName = x.FirstName,
                LastName = x.LastName,
                DateOfBirth = x.DateOfBirth
            }).ToListAsync();
        }
    }
}
