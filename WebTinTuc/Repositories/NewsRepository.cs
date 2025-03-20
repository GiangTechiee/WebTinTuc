using Microsoft.EntityFrameworkCore;
using WebTinTuc.Data;
using WebTinTuc.Models.Entities;

namespace WebTinTuc.Repositories
{
    public class NewsRepository : INewsRepository
    {
        private readonly WebTinTucContext _context;

        public NewsRepository(WebTinTucContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<News>> GetAll()
        {
            return await _context.News.ToListAsync();
        }

        public async Task<News?> GetById(int id)
        {
            return await _context.News
                .Include(n => n.Comments)
                    .ThenInclude(c => c.User)
                .Include(n => n.Categories)
                .FirstOrDefaultAsync(n => n.NewId == id);
        }

        public async Task AddAsync(News news)
        {
            await _context.News.AddAsync(news);
        }

        public async Task UpdateAsync(News news)
        {
            _context.News.Update(news);
        }

        public async Task DeleteAsync(int id)
        {
            var news = await _context.News.FindAsync(id);
            if (news != null)
            {
                _context.News.Remove(news);
            }
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
