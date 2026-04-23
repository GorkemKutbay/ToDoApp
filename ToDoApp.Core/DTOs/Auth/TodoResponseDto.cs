using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoApp.Core.DTOs.Auth
{
    public class TodoResponseDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Note { get; set; }
        public bool IsCompleted { get; set; }
        public DateTime? DueDate { get; set; }
        public string CategoryName { get; set; }

    }
}
