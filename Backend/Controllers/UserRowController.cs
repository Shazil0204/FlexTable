using Backend.DTOs.UserRow;
using Backend.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserRowController : ControllerBase
    {
        private readonly IUserRowService _userRowService;

        public UserRowController(IUserRowService userRowService)
        {
            _userRowService = userRowService ?? throw new ArgumentNullException(nameof(userRowService), "UserRowService cannot be null.");
        }

        [HttpGet]
        public async Task<IActionResult> GetAllRowsAsync([FromQuery] int pageNumber = 1)
        {
            var result = await _userRowService.GetAllRowsAsync(pageNumber).ConfigureAwait(false);
            return Ok(result);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetRowByIdAsync(int id)
        {
            var result = await _userRowService.GetRowByIdAsync(id).ConfigureAwait(false);
            return result != null ? Ok(result) : NotFound($"User row with ID {id} not found.");
        }

        [HttpGet("table/{tableId:int}")]
        public async Task<IActionResult> GetAllRowsByTableIdAsync(int tableId)
        {
            var result = await _userRowService.GetAllRowsByTableIdAsync(tableId).ConfigureAwait(false);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateRowAsync([FromBody] string data, [FromQuery] int tableId)
        {
            if (string.IsNullOrWhiteSpace(data))
            {
                return BadRequest("Data cannot be null or empty.");
            }

            var result = await _userRowService.CreateRowAsync(data, tableId).ConfigureAwait(false);
            return CreatedAtAction(nameof(GetRowByIdAsync), new { id = result.Id }, result);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateRowAsync(int id, [FromBody] UpdateUserRowRequestDTO dto)
        {
            if (dto == null)
            {
                return BadRequest("UpdateUserRowRequestDTO cannot be null.");
            }

            var result = await _userRowService.UpdateRowAsync(id, dto).ConfigureAwait(false);
            return Ok(result);
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteRowAsync(int id)
        {
            bool deleted = await _userRowService.DeleteRowAsync(id).ConfigureAwait(false);
            return deleted ? NoContent() : NotFound($"User row with ID {id} not found.");
        }
    }
}