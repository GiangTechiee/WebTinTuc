using WebTinTuc.Models.Entities;

namespace WebTinTuc.Repositories
{
    public interface ICommentRepository
    {
        Task AddComment(Comment comment);
        Task<string> GetUserFullName(int userId);
        Task<List<News>> GetNewsWithCommentsAsync(int page, int pageSize);
        Task<int> GetNewsCountAsync();
        Task<List<Comment>> GetCommentsByNewsIdAsync(int newId, string tab, int page, int pageSize);
        Task<int> GetCommentCountByNewsIdAsync(int newId, string tab);
        Task<Comment> GetCommentByIdAsync(int commentId);
        Task UpdateCommentAsync(Comment comment);
        Task DeleteCommentAsync(int commentId);
    }
}
