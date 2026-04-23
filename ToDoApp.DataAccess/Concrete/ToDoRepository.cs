using Microsoft.EntityFrameworkCore;
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
    public class ToDoRepository : IToDoRepository
    {
        private readonly AppDbContext _context;

        public ToDoRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<ToDoItem>> GetByCategoryAsync(int id)
        {
            var toDoItems = await _context.ToDoItems.Where(t => t.CategoryId == id).ToListAsync();
            return toDoItems;
        }

        public async Task<IEnumerable<ToDoItem>> GetByUserIdAsync(string id)
        {
            var toDoItem = await _context.ToDoItems.Where(t => t.UserId == id).ToListAsync() ?? throw new InvalidOperationException("ToDo item not found for the given user ID.");
            return toDoItem;
        }

        public async Task<IEnumerable<ToDoItem>> GetCompletedAsync()
        {
            var toDoItems = await _context.ToDoItems.Where(x => x.IsCompleted == true).ToListAsync();
            return toDoItems;
        }
    }
}
