using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebTinTuc.Models.Entities;
using WebTinTuc.Repositories;

namespace WebTinTuc.Controllers
{
    [Authorize(Roles = "Admin")]
    public class CategoryController : Controller
    {
        private readonly ICategoryRepository _categoryRepository;
        public readonly INewsRepository _newsRepository;

        public CategoryController(ICategoryRepository categoryRepository, INewsRepository newsRepository)
        {
            _categoryRepository = categoryRepository;
            _newsRepository = newsRepository;
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> CategoryDetails(int id)
        {
            var category = await _categoryRepository.GetByIdAsync(id);
            if (category == null)
            {
                return NotFound();
            }

            // Lấy danh sách tin tức thuộc danh mục này
            var newsInCategory = await _newsRepository.GetAll();
            category.News = newsInCategory.Where(n => n.Categories.Any(c => c.CategoryId == id)).ToList();
            

            return View(category);
        }


        // Hiển thị danh sách danh mục
        public async Task<IActionResult> Index()
        {
            var categories = await _categoryRepository.GetAllAsync();
            return View(categories);
        }


        [HttpGet]
        public IActionResult Create()
        {
            return PartialView("_CreateCategoryModal", new Category());
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([FromForm] Category category)
        {
            if (!ModelState.IsValid)
            {
                return PartialView("_CreateCategoryModal", category);
            }

            await _categoryRepository.AddAsync(category);
            await _categoryRepository.SaveChangesAsync();
            return Json(new { success = true, message = "Thêm danh mục thành công!" });
        }


        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var category = await _categoryRepository.GetByIdAsync(id);
            if (category == null)
            {
                return NotFound();
            }
            return PartialView("_EditCategoryModal", category);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [FromForm] Category category)
        {
            if (id != category.CategoryId)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return PartialView("_EditCategoryModal", category);
            }

            await _categoryRepository.UpdateAsync(category);
            await _categoryRepository.SaveChangesAsync();
            return Json(new { success = true, message = "Cập nhật danh mục thành công!" });
        }


        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var category = await _categoryRepository.GetByIdAsync(id);
            if (category == null)
            {
                return NotFound();
            }
            return PartialView("_DeleteCategoryModal", category);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var category = await _categoryRepository.GetByIdAsync(id);
            if (category == null)
            {
                return NotFound();
            }

            await _categoryRepository.DeleteAsync(id);
            await _categoryRepository.SaveChangesAsync();
            return Json(new { success = true, message = "Xóa danh mục thành công!" });
        }
    }
}
