using WebTinTuc.Models.Entities;

namespace WebTinTuc.Repositories
{
    public interface IFavoriteRepository
    {
        Task AddFavoriteAsync(Favorite favorite);
        Task<bool> RemoveFavoriteAsync(int userId, int newsId);
        Task<Favorite> GetFavoriteAsync(int userId, int newsId);
        Task<bool> IsFavoriteAsync(int userId, int newsId);
    }
}
