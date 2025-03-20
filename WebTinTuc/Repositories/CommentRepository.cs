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
    }
}
