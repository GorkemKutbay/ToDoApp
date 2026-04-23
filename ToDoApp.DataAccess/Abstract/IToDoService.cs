using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoApp.Core.Entities;

namespace ToDoApp.DataAccess.Abstract
{
    public interface IToDoService
    {
        Task<IEnumerable<ToDoItem>> GetAllAsync();
        Task<IEnumerable<ToDoItem>> GetAllByUserIdAsync(string userId);
        Task CreateAsync(ToDoItem toDoItem);
        Task UpdateAsync(ToDoItem toDoItem);
        Task DeleteAsync(int id,string userId);
    }
}
