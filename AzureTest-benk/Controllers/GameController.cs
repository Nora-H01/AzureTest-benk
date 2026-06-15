using AzureTest_benk.Models;
using backend_api.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AzureTest_benk.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GamesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public GamesController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<Game>>> GetAll()
        {
            return await _context.Game.ToListAsync();
        }

        [HttpGet("test-db")]
        public async Task<IActionResult> TestDb()
        {
            var count = await _context.Game.CountAsync();
            return Ok(new { gamesInDb = count });
        }

        [HttpPost]
        public async Task<ActionResult<Game>> Create(CreateGameDTO dto)
        {
            var game = new Game
            {
                Name = dto.Name,
                Genre = dto.Genre,
                ReleaseDate = DateTime.UtcNow
            };

            _context.Game.Add(game);

            await _context.SaveChangesAsync();

            return Ok(game);
        }
    }
}