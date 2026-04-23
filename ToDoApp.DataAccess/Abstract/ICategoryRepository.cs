using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoApp.Core.Entities;

namespace ToDoApp.DataAccess.Abstract
{
    public interface ICategoryRepository
    {
        Task<IEnumerable<Category>> GetByUserIdAsync();
        Task<IEnumerable<Category>> GetAll();
    }
}
