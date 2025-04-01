using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebTinTuc.Data;

namespace WebTinTuc.Controllers
{
    [Route("api/[controller]")]
    [Authorize(Roles = "Admin")]
    public class RoleController : Controller
    {
        private readonly WebTinTucContext _context;

        public RoleController(WebTinTucContext context)
        {
            _context = context;
        }

        [HttpGet("getall")]
        public IActionResult GetAll()
        {
            var roles = _context.Roles.Select(r => new
            {
                roleId = r.RoleId,
                roleName = r.RoleName
            }).ToList();
            return Ok(roles);
        }
    }
}
