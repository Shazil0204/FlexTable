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
        public async Task<IActionResult> GetAllRows([FromQuery] int pageNumber = 1)
        {
            var result = await _userRowService.GetAllRowsAsync(pageNumber).ConfigureAwait(false);
            return Ok(result);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetRowById(int id)
        {
            var result = await _userRowService.GetRowByIdAsync(id).ConfigureAwait(false);
            return result != null ? Ok(result) : NotFound($"User row with ID {id} not found.");
        }

        [HttpGet("table/{tableId:int}")]
        public async Task<IActionResult> GetAllRowsByTableId(int tableId)
        {
            var result = await _userRowService.GetAllRowsByTableIdAsync(tableId).ConfigureAwait(false);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateRow([FromBody] string data, [FromQuery] int tableId)
        {
            if (string.IsNullOrWhiteSpace(data)) return BadRequest("Data cannot be null or empty.");
            var result = await _userRowService.CreateRowAsync(data, tableId).ConfigureAwait(false);
            return CreatedAtAction(nameof(GetRowById), new { id = result.Id }, result);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateRow(int id, [FromBody] string data)
        {
            if (data == null)
            {
                return BadRequest("Data cannot be null.");
            }

            var result = await _userRowService.UpdateRowAsync(id, data).ConfigureAwait(false);
            return Ok(result);
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteRow(int id)
        {
            bool deleted = await _userRowService.DeleteRowAsync(id).ConfigureAwait(false);
            return deleted ? NoContent() : NotFound($"User row with ID {id} not found.");
        }
    }
}