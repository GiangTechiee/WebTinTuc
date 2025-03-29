using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using WebTinTuc.Models.DTOs;
using WebTinTuc.Models.Entities;
using WebTinTuc.Repositories;

namespace WebTinTuc.Controllers
{
    public class CommentController : Controller
    {
        private readonly ICommentRepository _commentRepository;
        private readonly INewsRepository _newsRepository;

        public CommentController(ICommentRepository commentRepository, INewsRepository newsRepository)
        {
            _commentRepository = commentRepository;
            _newsRepository = newsRepository;
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddComment([FromBody] CommentRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.Content))
            {
                return BadRequest(new { message = "Nội dung bình luận không được để trống" });
            }

            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            var comment = new Comment
            {
                Fk_NewId = request.NewId,
                Fk_UserId = userId,
                Content = request.Content,
                CreatedAt = DateTime.Now,
                IsApprove = true
            };

            await _commentRepository.AddComment(comment);

            // Lấy FullName trực tiếp từ User
            var userFullName = await _commentRepository.GetUserFullName(userId);
            if (string.IsNullOrEmpty(userFullName))
            {
                return StatusCode(500, new { message = "Không thể load thông tin người dùng" });
            }

            return Ok(new
            {
                commentId = comment.CommentId,
                content = comment.Content,
                createdAt = comment.CreatedAt.ToString("o"),
                isApprove = comment.IsApprove,
                userFullName = userFullName,
                fk_UserId = comment.Fk_UserId
            });
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddReply([FromBody] ReplyRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.Content))
            {
                return BadRequest(new { message = "Nội dung trả lời không được để trống" });
            }

            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            var reply = new Comment
            {
                Fk_NewId = request.NewId,
                Fk_UserId = userId,
                ParentId = request.ParentId,
                Content = request.Content,
                CreatedAt = DateTime.Now,
                IsApprove = true
            };

            await _commentRepository.AddComment(reply);

            // Lấy FullName trực tiếp từ User
            var userFullName = await _commentRepository.GetUserFullName(userId);
            if (string.IsNullOrEmpty(userFullName))
            {
                return StatusCode(500, new { message = "Không thể load thông tin người dùng" });
            }

            return Ok(new
            {
                commentId = reply.CommentId,
                content = reply.Content,
                createdAt = reply.CreatedAt.ToString("o"),
                isApprove = reply.IsApprove,
                userFullName = userFullName,
                fk_UserId = reply.Fk_UserId
            });
        }
    }
}
