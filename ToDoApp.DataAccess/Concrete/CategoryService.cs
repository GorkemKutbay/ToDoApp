using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoApp.Core.Entities;
using ToDoApp.DataAccess.Abstract;
using ToDoApp.DataAccess.DataAcces;

namespace ToDoApp.DataAccess.Concrete
{
    public class CategoryService : ICategoryService
    {
        private readonly IGenericRepository<Category> _generic;
        private readonly AppDbContext _context;

        public CategoryService(IGenericRepository<Category> generic,AppDbContext context)
        {
            _generic = generic;
            _context = context;
        }

        public async Task CreateAsync(Category category)
        {
            await _generic.AddAsync(category);
        }

        public async Task DeleteAsync(int id, string userId)
        {
            var allCategories = _generic.GetAllAsync();
            var category = (await allCategories).FirstOrDefault(x => x.Id == id && x.UserId == userId);
            if (category != null)
                await _generic.DeleteAsync(category.Id);
        }

        public async Task<IEnumerable<Category>> GetAllAsync()
        {
            return await _generic.GetAllAsync();
        }

        public async Task<IEnumerable<Category>> GetAllByUserIdAsync(string userId)
        {
            var allCategories = await _generic.GetAllAsync();
            return allCategories.Where(x => x.UserId == userId);
        }

        public async Task UpdateAsync(Category category)
        {
            await _generic.UpdateAsync(category);
        }
    }
}
