using WebTinTuc.Models.Entities;

namespace WebTinTuc.Repositories
{
    public interface INewsRepository
    {
        Task<IEnumerable<News>> GetAll();
        Task<News> GetById(int id);
        Task AddAsync(News news);
        Task UpdateAsync(News news);
        Task DeleteAsync(int id);
        Task SaveChangesAsync();
    }
}
