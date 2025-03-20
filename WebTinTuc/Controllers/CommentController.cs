using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using WebTinTuc.Models.Entities;
using WebTinTuc.Repositories;

namespace WebTinTuc.Controllers
{
    public class CommentController : Controller
    {
        private readonly ICommentRepository _commentRepository;

        public CommentController(ICommentRepository commentRepository)
        {
            _commentRepository = commentRepository;
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> AddComment(int NewId, string Content)
        {
            if (string.IsNullOrWhiteSpace(Content))
            {
                return RedirectToAction("Details", "News", new { id = NewId });
            }

            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            var comment = new Comment
            {
                Fk_NewId = NewId,
                Fk_UserId = userId,
                Content = Content,
                CreatedAt = DateTime.Now,
                IsApprove = false 
            };

            await _commentRepository.AddComment(comment);
            return RedirectToAction("Details", "News", new { id = NewId });
        }
    }
}
