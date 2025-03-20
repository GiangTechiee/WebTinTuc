using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using WebTinTuc.Models.Entities;
using WebTinTuc.Repositories;

namespace WebTinTuc.Controllers
{
    public class NewsController : Controller
    {
        private readonly INewsRepository _newsRepository;

        public NewsController(INewsRepository newsRepository)
        {
            _newsRepository = newsRepository;
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

    }
}
