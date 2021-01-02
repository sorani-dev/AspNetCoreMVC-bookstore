using BookStoreMvc.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookStoreMvc.Repository
{
    public interface ICategoryRepository
    {
        Task<List<CategoryModel>> GetCategories();
    }
}