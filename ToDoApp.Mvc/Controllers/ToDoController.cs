using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using ToDoApp.Core.DTOs.Auth;
using ToDoApp.Core.Entities;
using ToDoApp.DataAccess.Abstract;

namespace ToDoApp.Mvc.Controllers
{
    [Authorize]
    public class ToDoController : Controller
    {
        private readonly IToDoRepository _repo;
        private readonly IToDoService _service;
        private readonly ICategoryService _categoryService;

        public ToDoController(IToDoRepository repo, IToDoService service, ICategoryService categoryService)
        {

            _repo = repo;
            _service = service;
            _categoryService = categoryService;

        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var todos = await _service.GetAllByUserIdAsync(userId);
            return View(todos);
        }

        [HttpGet]
        public async Task<IActionResult> GetToDoById(string id)
        {

            id = GetUserId();
            var todo = await _repo.GetByUserIdAsync(id);
            return View(todo);

        }

        [HttpGet]
        public async Task<IActionResult> GetTodoByCategories(int id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var allTodos = await _service.GetAllByUserIdAsync(userId);
            var todos = allTodos.Where(x => x.CategoryId == id).ToList();
            return View(todos);
        }
        [HttpGet]
        public async Task<IActionResult> AddTodo()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var categories = await _categoryService.GetAllByUserIdAsync(userId);
            ViewBag.Categories = new Microsoft.AspNetCore.Mvc.Rendering.SelectList(categories, "Id", "Name");
            return View();
        }
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreateCategoryAjax(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                return BadRequest("Category name cannot be empty.");

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var category = new Category
            {
                Name = name,
                UserId = userId
            };

            await _categoryService.CreateAsync(category);
            return Json(new { id = category.Id, name = category.Name });
        }

        [HttpPost]
        public async Task<IActionResult> AddTodo([FromForm] CreateToDoDto create)
        {
            if (!ModelState.IsValid)
            {
                var userId2 = User.FindFirstValue(ClaimTypes.NameIdentifier);
                var categories = await _categoryService.GetAllByUserIdAsync(userId2);
                ViewBag.Categories = new Microsoft.AspNetCore.Mvc.Rendering.SelectList(categories, "Id", "Name");
                return View(create);
            }

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var todo = new ToDoItem
            {
                Title = create.Title,
                Note = create.Note,
                CategoryId = create.CategoryId,
                DueDate = create.DueDate,
                CreatedAt = DateTime.UtcNow,
                IsCompleted = false,
                UserId = userId

            };
            await _service.CreateAsync(todo);
            return RedirectToAction("GetAll", "ToDo");

        }
        
        [HttpGet]
        public async Task<IActionResult> UpdateTodo(int id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var allTodos = await _service.GetAllByUserIdAsync(userId);
            var todo = allTodos.FirstOrDefault(x => x.Id == id);
            if (todo == null)
            {
                return NotFound();
            }

            var updateDto = new UpdateToDoDto
            {
                Title = todo.Title,
                Note = todo.Note,
                DueDate = todo.DueDate,
                CategoryId = todo.CategoryId
            };
            
            var categories = await _categoryService.GetAllByUserIdAsync(userId);
            ViewBag.Categories = new Microsoft.AspNetCore.Mvc.Rendering.SelectList(categories, "Id", "Name");
            
            ViewBag.TodoId = id;
            return View(updateDto);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateTodo(int id, [FromForm] UpdateToDoDto update)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var allTodos = await _service.GetAllByUserIdAsync(userId);
            var todo = allTodos.FirstOrDefault(x => x.Id == id);
            if (todo == null)
            {
                return NotFound();
            }

            // Mevcut tracked entity'nin property'lerini güncelle
            todo.Title = update.Title;
            todo.Note = update.Note;
            todo.CategoryId = update.CategoryId;
            todo.DueDate = update.DueDate;

            await _service.UpdateAsync(todo);
            return RedirectToAction("GetAll", "ToDo");
        }
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var allTodos = await _service.GetAllByUserIdAsync(userId);
            var todo = allTodos.FirstOrDefault(x => x.Id == id);
            if (todo == null)
            {
                return NotFound();
            }
            await _service.DeleteAsync(id, userId);
            ViewBag.Success = "ToDo item deleted successfully.";
            return RedirectToAction("GetAll", "ToDo");
        }
        private string GetUserId()
        {
            return User.FindFirstValue(ClaimTypes.NameIdentifier) ?? string.Empty;
        }
    }
}

