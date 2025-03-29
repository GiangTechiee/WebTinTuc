using Microsoft.EntityFrameworkCore;
using WebTinTuc.Data;
using WebTinTuc.Models.Entities;

namespace WebTinTuc.Repositories
{
    public class CommentRepository : ICommentRepository
    {
        private readonly WebTinTucContext _context;

        public CommentRepository(WebTinTucContext context)
        {
            _context = context;
        }

        public async Task AddComment(Comment comment)
        {
            _context.Comments.Add(comment);
            await _context.SaveChangesAsync();
        }

        public async Task<string> GetUserFullName(int userId)
        {
            var user = await _context.Users
                .Where(u => u.UserId == userId)
                .Select(u => u.FullName)
                .FirstOrDefaultAsync();
            return user;
        }

        // Lấy danh sách bài viết có bình luận (phân trang)
        public async Task<List<News>> GetNewsWithCommentsAsync(int page, int pageSize)
        {
            return await _context.News
                .Where(n => n.Comments.Any()) // Chỉ lấy bài viết có bình luận
                .Include(n => n.Comments)
                .OrderByDescending(n => n.PostedDate)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        // Đếm tổng số bài viết có bình luận
        public async Task<int> GetNewsCountAsync()
        {
            return await _context.News
                .CountAsync(n => n.Comments.Any());
        }

        // Lấy danh sách bình luận theo bài viết và trạng thái (phân trang)
        public async Task<List<Comment>> GetCommentsByNewsIdAsync(int newId, string tab, int page, int pageSize)
        {
            var query = _context.Comments
                .Where(c => c.Fk_NewId == newId)
                .Include(c => c.User)
                .AsQueryable();

            switch (tab)
            {
                case "approved":
                    query = query.Where(c => c.IsApprove);
                    break;
                case "rejected":
                    query = query.Where(c => !c.IsApprove);
                    break;
                    // "all" không cần lọc thêm
            }

            return await query
                .OrderByDescending(c => c.CreatedAt)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        // Đếm số lượng bình luận theo bài viết và trạng thái
        public async Task<int> GetCommentCountByNewsIdAsync(int newId, string tab)
        {
            var query = _context.Comments
                .Where(c => c.Fk_NewId == newId)
                .AsQueryable();

            switch (tab)
            {
                case "approved":
                    query = query.Where(c => c.IsApprove);
                    break;
                case "rejected":
                    query = query.Where(c => !c.IsApprove);
                    break;
                    // "all" không cần lọc thêm
            }

            return await query.CountAsync();
        }

        // Lấy bình luận theo ID
        public async Task<Comment> GetCommentByIdAsync(int commentId)
        {
            return await _context.Comments
                .Include(c => c.User)
                .FirstOrDefaultAsync(c => c.CommentId == commentId);
        }

        // Cập nhật bình luận
        public async Task UpdateCommentAsync(Comment comment)
        {
            _context.Comments.Update(comment);
            await _context.SaveChangesAsync();
        }

        // Xóa bình luận
        public async Task DeleteCommentAsync(int commentId)
        {
            var comment = await _context.Comments
                .Include(c => c.Replies) // Tải các bình luận con
                .FirstOrDefaultAsync(c => c.CommentId == commentId);

            if (comment == null)
            {
                throw new InvalidOperationException("Bình luận không tồn tại.");
            }

            try
            {
                // Xóa đệ quy các bình luận con
                if (comment.Replies != null && comment.Replies.Any())
                {
                    foreach (var reply in comment.Replies.ToList())
                    {
                        await DeleteCommentAsync(reply.CommentId); // Gọi đệ quy
                    }
                }

                _context.Comments.Remove(comment);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                throw new InvalidOperationException("Không thể xóa bình luận do lỗi dữ liệu.", ex);
            }
        }
    }
}
