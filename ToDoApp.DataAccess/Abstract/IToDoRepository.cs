using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoApp.Core.Entities;

namespace ToDoApp.DataAccess.Abstract
{
    public interface IToDoRepository
    {
        Task<IEnumerable<ToDoItem>> GetByUserIdAsync(string id);
        Task<IEnumerable<ToDoItem>> GetByCategoryAsync(int id);
        Task<IEnumerable<ToDoItem>> GetCompletedAsync(); 
       
    }
}
