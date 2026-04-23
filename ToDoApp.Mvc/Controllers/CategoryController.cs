using Microsoft.AspNetCore.Mvc;
using ToDoApp.Core.Entities;
using ToDoApp.DataAccess.Abstract;

namespace ToDoApp.Mvc.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategoryService _category;
        private readonly ICategoryRepository _repo;

        public CategoryController(ICategoryService category, ICategoryRepository repo)
        {
            _category = category;
            _repo = repo;
        }
        [HttpGet]
        public async Task<IActionResult> GetCategories()
        {
            //var categories = await _category.GetAllAsync();
            var categories = await _repo.GetAll();
            return View(categories);
        }
        //[HttpGet]
        //public IActionResult CreateAsync()
        //{
        //    return View();
        //}
        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromForm] Category category)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await _category.CreateAsync(category);

            return View();
        }
        //[HttpGet]
        //public IActionResult Put()
        //{
        //    return View();
        //}
        [HttpPost]
        public async Task<IActionResult> Put([FromForm] Category category)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await _category.UpdateAsync(category);
            return View();
        }
        [HttpPost] 
        public async Task<IActionResult> Delete(int id, string userId)
        {
            await _category.DeleteAsync(id, userId);
            return View();
        }
    }
}
