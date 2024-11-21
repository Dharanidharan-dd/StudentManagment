using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentSport.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SportsController : Controller
    {
        private readonly DatabaseHelper _dbHelper;

        public SportsController(DatabaseHelper dbHelper)
        {
            _dbHelper = dbHelper;
        }

        // GET: api/Sports
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Sports>>> GetSports()
        {
            return await _dbHelper.GetSportsAsync();
        }

        // GET: api/Sports/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Sports>> GetSport(int id)
        {
            var sport = await _dbHelper.GetSportAsync(id);

            if (sport == null)
            {
                return NotFound();
            }

            return sport;
        }

        // POST: api/Sports
        [HttpPost]
        public async Task<ActionResult<Sports>> PostSport(Sports sport)
        {
            await _dbHelper.AddSportAsync(sport);
            return CreatedAtAction(nameof(GetSport), new { id = sport.StudentID }, sport);
        }

        // PUT: api/Sports/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSport(string id, Sports sport)
        {
            if (id != sport.StudentID)
            {
                return BadRequest();
            }

            await _dbHelper.UpdateSportAsync(sport);
            return NoContent();
        }

        // DELETE: api/Sports/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSport(int id)
        {
            await _dbHelper.DeleteSportAsync(id);
            return NoContent();
        }
    }
}
