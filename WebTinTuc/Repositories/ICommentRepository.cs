using WebTinTuc.Models.Entities;

namespace WebTinTuc.Repositories
{
    public interface ICommentRepository
    {
        Task AddComment(Comment comment);
    }
}
