using Microsoft.AspNetCore.Mvc;
using WebTinTuc.Repositories;

namespace WebTinTuc.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategoryRepository _categoryRepository;
        public readonly INewsRepository _newsRepository;

        public CategoryController(ICategoryRepository categoryRepository, INewsRepository newsRepository)
        {
            _categoryRepository = categoryRepository;
            _newsRepository = newsRepository;
        }

        [HttpGet]
        public async Task<IActionResult> CategoryDetails(int id)
        {
            var category = await _categoryRepository.GetByIdAsync(id);
            if (category == null)
            {
                return NotFound();
            }

            // Lấy danh sách tin tức thuộc danh mục này (tùy chọn)
            var newsInCategory = await _newsRepository.GetAll();
            category.News = newsInCategory.Where(n => n.Categories.Any(c => c.CategoryId == id)).ToList();
            

            return View(category);
        }
    }
}
