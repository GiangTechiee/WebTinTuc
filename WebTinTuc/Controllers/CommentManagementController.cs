using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebTinTuc.Repositories;

namespace WebTinTuc.Controllers
{
    [Authorize(Roles = "Admin")]
    public class CommentManagementController : Controller
    {
        private readonly ICommentRepository _commentRepository;

        public CommentManagementController(ICommentRepository commentRepository)
        {
            _commentRepository = commentRepository;
        }

        // Trang chính: Danh sách bài viết có bình luận
        public async Task<IActionResult> Index(int page = 1)
        {
            int pageSize = 10;
            var newsWithComments = await _commentRepository.GetNewsWithCommentsAsync(page, pageSize);
            ViewBag.CurrentPage = page;
            ViewBag.TotalPages = (int)Math.Ceiling(await _commentRepository.GetNewsCountAsync() / (double)pageSize);
            return View(newsWithComments);
        }

        // Chi tiết bình luận của bài viết
        public async Task<IActionResult> Comments(int newId, string tab = "all", int page = 1)
        {
            int pageSize = 20;
            var comments = await _commentRepository.GetCommentsByNewsIdAsync(newId, tab, page, pageSize);
            ViewBag.NewId = newId;
            ViewBag.Tab = tab;
            ViewBag.CurrentPage = page;
            ViewBag.TotalPages = (int)Math.Ceiling(await _commentRepository.GetCommentCountByNewsIdAsync(newId, tab) / (double)pageSize);
            return View(comments);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RejectComment(int commentId)
        {
            var comment = await _commentRepository.GetCommentByIdAsync(commentId);
            if (comment == null) return NotFound();

            comment.IsApprove = false;
            await _commentRepository.UpdateCommentAsync(comment);
            return Json(new { success = true });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ApproveComment(int commentId)
        {
            var comment = await _commentRepository.GetCommentByIdAsync(commentId);
            if (comment == null) return NotFound();

            comment.IsApprove = true;
            await _commentRepository.UpdateCommentAsync(comment);
            return Json(new { success = true });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteComment(int commentId)
        {
            try
            {
                var comment = await _commentRepository.GetCommentByIdAsync(commentId);
                if (comment == null)
                {
                    return Json(new { success = false, message = "Không tìm thấy bình luận." });
                }

                await _commentRepository.DeleteCommentAsync(commentId);
                return Json(new { success = true });
            }
            catch (Exception ex)
            {
                // Log lỗi nếu cần (dùng ILogger nếu có)
                return StatusCode(500, new { success = false, message = "Lỗi server: " + ex.Message });
            }
        }
    }
}
