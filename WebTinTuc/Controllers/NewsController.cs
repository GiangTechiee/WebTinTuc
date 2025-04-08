using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
        public readonly ICategoryRepository _categoryRepository;
        public readonly IFavoriteRepository _favoriteRepository;

        public NewsController(INewsRepository newsRepository, IWebHostEnvironment environment, 
            IUserRepository userRepository, ICategoryRepository categoryRepository, IFavoriteRepository favoriteRepository)
        {
            _newsRepository = newsRepository;
            _environment = environment;
            _userRepository = userRepository;
            _categoryRepository = categoryRepository;
            _favoriteRepository = favoriteRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetLatestNews(int skip = 0, int take = 6)
        {
            var newsList = await _newsRepository.GetAll(); // Lấy toàn bộ tin
            var filteredNews = newsList.Skip(skip).Take(take).Select(n => new
            {
                newId = n.NewId,
                title = n.Title,
                @abstract = n.Abstract,
                fullImagePath = n.FullImagePath,
                postedDate = n.PostedDate,
                viewCount = n.ViewCount
            });
            return Ok(filteredNews);
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
            string sessionKey = $"ViewedNews_{id}";
            if (HttpContext.Session.GetString(sessionKey) == null)
            {
                news.ViewCount++;
                await _newsRepository.UpdateAsync(news);
                await _newsRepository.SaveChangesAsync();

                // Đánh dấu bài viết đã được xem trong session
                HttpContext.Session.SetString(sessionKey, "true");
            }

            // Kiểm tra trạng thái yêu thích nếu người dùng đã đăng nhập
            bool isFavorited = false;
            if (User.Identity.IsAuthenticated)
            {
                int userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
                isFavorited = await _favoriteRepository.IsFavoriteAsync(userId, id);
            }

            ViewBag.IsFavorited = isFavorited;
            return View(news);
        }


        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            ViewBag.Categories = await _categoryRepository.GetAllAsync();
            return View();
        }

        // Controllers/NewsController.cs
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([FromForm] News news, IFormFile imageFile, string categoryIds)
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

                // Xử lý chuỗi categoryIds thành List<int>
                List<int> categoryIdList = string.IsNullOrEmpty(categoryIds)
                    ? new List<int>()
                    : categoryIds.Split(',').Select(int.Parse).ToList();

                // Thêm danh mục vào bài viết
                if (categoryIds.Any())
                {
                    news.Categories = (await _categoryRepository.GetAllAsync())
                        .Where(c => categoryIdList.Contains(c.CategoryId))
                        .ToList();
                }

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
            // Kiểm tra xem bài viết đã được xem trong session này chưa
            string sessionKey = $"ViewedNews_{id}";
            if (HttpContext.Session.GetString(sessionKey) == null)
            {
                news.ViewCount++;
                await _newsRepository.UpdateAsync(news);
                await _newsRepository.SaveChangesAsync();

                // Đánh dấu bài viết đã được xem trong session
                HttpContext.Session.SetString(sessionKey, "true");
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


        // dành cho admin
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> ApprovePosts()
        {
            // Lấy danh sách bài viết chưa duyệt của user (không phải admin)
            var allPosts = await _newsRepository.GetAll();

            // Áp dụng LINQ sau khi await
            var posts = allPosts
                .Where(n => !n.IsApprove && n.User.Role.RoleId != 1) // 1 là admin
                .ToList(); // Chuyển thành List
            return View(posts);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Approve(int id)
        {
            var news = await _newsRepository.GetById(id);
            if (news == null)
            {
                return NotFound();
            }
            news.IsApprove = true;
            await _newsRepository.UpdateAsync(news);
            await _newsRepository.SaveChangesAsync();
            return Ok();
        }

    }
}
