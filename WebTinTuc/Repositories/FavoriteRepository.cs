using Microsoft.EntityFrameworkCore;
using WebTinTuc.Data;
using WebTinTuc.Models.Entities;

namespace WebTinTuc.Repositories
{
    public class FavoriteRepository : IFavoriteRepository
    {
        private readonly WebTinTucContext _context; // Thay bằng DbContext của bạn

        public FavoriteRepository(WebTinTucContext context)
        {
            _context = context;
        }

        public async Task AddFavoriteAsync(Favorite favorite)
        {
            _context.Favorites.Add(favorite);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> RemoveFavoriteAsync(int userId, int newsId)
        {
            var favorite = await _context.Favorites
                .FirstOrDefaultAsync(f => f.Fk_UserId == userId && f.Fk_NewId == newsId);
            if (favorite == null) return false;

            _context.Favorites.Remove(favorite);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<Favorite> GetFavoriteAsync(int userId, int newsId)
        {
            return await _context.Favorites
                .FirstOrDefaultAsync(f => f.Fk_UserId == userId && f.Fk_NewId == newsId);
        }

        public async Task<bool> IsFavoriteAsync(int userId, int newsId)
        {
            return await _context.Favorites
                .AnyAsync(f => f.Fk_UserId == userId && f.Fk_NewId == newsId);
        }
    }
}
