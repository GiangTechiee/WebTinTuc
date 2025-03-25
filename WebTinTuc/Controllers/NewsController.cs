using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using WebTinTuc.Models.Entities;
using WebTinTuc.Repositories;
using WebTinTuc.Services;

namespace WebTinTuc.Controllers
{
    public class NewsController : Controller
    {
        private readonly INewsRepository _newsRepository;
        private readonly IWebHostEnvironment _environment;
        public readonly IUserRepository _userRepository;

        public NewsController(INewsRepository newsRepository, IWebHostEnvironment environment, IUserRepository userRepository)
        {
            _newsRepository = newsRepository;
            _environment = environment;
            _userRepository = userRepository;
        }

        // Action hiển thị trang chi tiết
        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var news = await _newsRepository.GetById(id);
            if (news == null)
            {
                return NotFound();
            }
            return View(news);
        }


        [Authorize]
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        // Controllers/NewsController.cs
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([FromForm] News news, IFormFile imageFile)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    var errors = ModelState.Values
                        .SelectMany(v => v.Errors)
                        .Select(e => e.ErrorMessage)
                        .ToList();
                    return Json(new { success = false, errors = errors });
                }

                if (imageFile != null && imageFile.Length > 0)
                {
                    news.Image = ImageUtil.SaveImage(imageFile, _environment);
                }

                news.PostedDate = DateTime.Now;
                news.Fk_UserId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier) ?? throw new Exception("Không tìm thấy ID người dùng"));
                news.ViewCount = 0;
                news.IsApprove = await _userRepository.IsAdminAsync(news.Fk_UserId);

                await _newsRepository.AddAsync(news);
                await _newsRepository.SaveChangesAsync();

                return Json(new { success = true, message = "Tin tức đã được tạo thành công" });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, errors = new[] { $"Lỗi máy chủ: {ex.Message}" } });
            }
        }

        // Action để xem các bài đã đăng
        [Authorize]
        public async Task<IActionResult> MyPosts()
        {
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var posts = await _newsRepository.GetAll();
            var myPosts = posts.Where(p => p.Fk_UserId == userId);
            return View(myPosts);
        }


        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var news = await _newsRepository.GetById(id);
            if (news == null || news.Fk_UserId != int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)))
            {
                return NotFound();
            }
            return View(news);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [FromForm] News news, IFormFile imageFile)
        {
            if (id != news.NewId)
            {
                return BadRequest();
            }

            var existingNews = await _newsRepository.GetById(id);
            if (existingNews == null || existingNews.Fk_UserId != int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)))
            {
                return NotFound();
            }

            try
            {
                if (imageFile == null)
                {
                    ModelState.Remove("imageFile");
                }

                if (!ModelState.IsValid)
                {
                    return View(news);
                }

                existingNews.Title = news.Title;
                existingNews.Abstract = news.Abstract;
                existingNews.Content = news.Content;
                // Giữ nguyên các trường khác như PostedDate, Fk_UserId, IsApprove

                if (imageFile != null && imageFile.Length > 0)
                {
                    existingNews.Image = ImageUtil.SaveImage(imageFile, _environment);
                }

                await _newsRepository.UpdateAsync(existingNews);
                await _newsRepository.SaveChangesAsync();

                return RedirectToAction("MyPosts");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"Lỗi khi cập nhật: {ex.Message}");
                return View(news);
            }
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var news = await _newsRepository.GetById(id);
            if (news == null || news.Fk_UserId != int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)))
            {
                return NotFound();
            }

            await _newsRepository.DeleteAsync(id);
            await _newsRepository.SaveChangesAsync();
            return Ok(); // Trả về 200 OK cho AJAX
        }

    }
}
