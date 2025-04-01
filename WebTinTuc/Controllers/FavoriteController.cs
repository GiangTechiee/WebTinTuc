using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using WebTinTuc.Models.DTOs;
using WebTinTuc.Models.Entities;
using WebTinTuc.Repositories;

namespace WebTinTuc.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FavoriteController : ControllerBase
    {
        private readonly IFavoriteRepository _favoriteRepository;

        public FavoriteController(IFavoriteRepository favoriteRepository)
        {
            _favoriteRepository = favoriteRepository;
        }

        [Authorize]
        [HttpPost("toggle")]
        public async Task<IActionResult> ToggleFavorite([FromBody] FavoriteToggleRequest request)
        {
            try
            {
                int userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
                bool isFavorited = await _favoriteRepository.IsFavoriteAsync(userId, request.NewsId);

                if (isFavorited)
                {
                    // Xóa khỏi yêu thích
                    bool removed = await _favoriteRepository.RemoveFavoriteAsync(userId, request.NewsId);
                    if (!removed)
                    {
                        return BadRequest(new { success = false, message = "Không thể xóa khỏi yêu thích" });
                    }
                    return Ok(new { success = true, isFavorited = false, message = "Đã xóa khỏi yêu thích" });
                }
                else
                {
                    // Thêm vào yêu thích
                    var favorite = new Favorite
                    {
                        Fk_UserId = userId,
                        Fk_NewId = request.NewsId,
                        CreatedAt = DateTime.Now
                    };
                    await _favoriteRepository.AddFavoriteAsync(favorite);
                    return Ok(new { success = true, isFavorited = true, message = "Đã thêm vào yêu thích" });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new { success = false, message = $"Lỗi: {ex.Message}" });
            }
        }
    }
}