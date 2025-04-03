using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebTinTuc.Data;
using WebTinTuc.Models.Entities;

namespace WebTinTuc.Controllers
{
    public class SearchController : Controller
    {
        private readonly WebTinTucContext _context;

        public SearchController(WebTinTucContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Results(string query)
        {
            if (string.IsNullOrWhiteSpace(query))
            {
                ViewBag.Message = "Vui lòng nhập từ khóa tìm kiếm.";
                return View(new List<News>());
            }

            // Tìm kiếm bài viết dựa trên Title, Abstract, hoặc Content
            var searchResults = await _context.News
                .Where(n => n.IsApprove && // Chỉ lấy bài viết đã được duyệt
                            (n.Title.ToLower().Contains(query.ToLower()) ||
                             n.Abstract.ToLower().Contains(query.ToLower()) ||
                             n.Content.ToLower().Contains(query.ToLower())))
                .Include(n => n.User) // Lấy thông tin người đăng nếu cần
                .Include(n => n.Categories) // Lấy danh mục nếu cần
                .OrderByDescending(n => n.PostedDate) // Sắp xếp theo ngày đăng mới nhất
                .ToListAsync();

            if (!searchResults.Any())
            {
                ViewBag.Message = $"Không tìm thấy bài viết nào liên quan đến '{query}'.";
            }

            ViewBag.Query = query; // Truyền từ khóa tìm kiếm để hiển thị trên view
            return View(searchResults);
        }
    }
}
