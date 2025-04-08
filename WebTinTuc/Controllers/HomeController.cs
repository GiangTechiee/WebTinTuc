using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using WebTinTuc.Models;
using WebTinTuc.Repositories;

namespace WebTinTuc.Controllers
{
    public class HomeController : Controller
    {
        private readonly INewsRepository _newsRepository;

        public HomeController(INewsRepository newsRepository)
        {
            _newsRepository = newsRepository;
        }

        public async Task<IActionResult> Index()
        {
            var approvedNews = await _newsRepository.GetApproveNews();
            return View(approvedNews);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Support()
        {
            return View();
        }
    }
}
