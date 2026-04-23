using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoApp.Core.Entities;

namespace ToDoApp.Core.DTOs.Auth
{
    public class CreateToDoDto
    {
        public string Title { get; set; }
        public string Note { get; set; }
        public DateTime? DueDate { get; set; }
        public int CategoryId { get; set; }
       
    }

}
