using Microsoft.AspNetCore.Mvc.ModelBinding;
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
    public class ToDoService : IToDoService
    {
       
        private readonly IGenericRepository<ToDoItem> _generic;

        public ToDoService(IGenericRepository<ToDoItem> generic)
        {
            _generic = generic;
        }

        public async Task CreateAsync(ToDoItem toDoItem)
        {
            if (toDoItem == null)
                throw new Exception("ToDoItem not found");
           await _generic.AddAsync(toDoItem);

        }

        public async Task DeleteAsync(int id,string userId)
        {
            var allEntity = await _generic.GetAllAsync();
            var entity = allEntity.FirstOrDefault(x => x.Id == id && x.UserId == userId);
            if (entity == null)
                throw new Exception("ToDoItem not found");
            
            await _generic.DeleteAsync(entity.Id);
        }

        public async Task<IEnumerable<ToDoItem>> GetAllAsync()
        {
            return await _generic.GetAllAsync();
        }

        public async Task<IEnumerable<ToDoItem>> GetAllByUserIdAsync(string userId)
        {
            var allItems = await _generic.GetAllAsync();
            return allItems.Where(x => x.UserId == userId);
        }

        public async Task UpdateAsync(ToDoItem toDoItem)
        {
            await _generic.UpdateAsync(toDoItem);
        }
    }
}
