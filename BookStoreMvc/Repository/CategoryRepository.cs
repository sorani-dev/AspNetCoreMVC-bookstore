using BookStoreMvc.Data;
using BookStoreMvc.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStoreMvc.Repository
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly BookStoreContext context;

        public CategoryRepository(BookStoreContext context)
        {
            this.context = context;
        }

        public async Task<List<CategoryModel>> GetCategories()
        {
            return await context.Category.Select(x => new CategoryModel()
            {
                Id = x.CategoryId,
                Name = x.CategoryName
            }).ToListAsync();
        }
    }
}
