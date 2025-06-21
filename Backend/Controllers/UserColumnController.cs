using Backend.DTOs.UserColumn;
using Backend.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserColumnController : ControllerBase
    {
        private readonly IUserColumnService _userColumnService;
        public UserColumnController(IUserColumnService userColumnService)
        {
            _userColumnService = userColumnService ?? throw new ArgumentNullException(nameof(userColumnService), "UserColumnService cannot be null.");
        }

        [HttpGet]
        public async Task<IActionResult> GetAllColumns([FromQuery] int pageNumber = 1)
        {
            var result = await _userColumnService.GetAllColumnsAsync(pageNumber).ConfigureAwait(false);
            return Ok(result);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetColumnById(int id)
        {
            var result = await _userColumnService.GetColumnByIdAsync(id).ConfigureAwait(false);
            return Ok(result);
        }

        [HttpGet("table/{tableId:int}")]
        public async Task<IActionResult> GetAllColumnsByTableId(int tableId)
        {
            if (tableId <= 0) return BadRequest("Invalid table ID.");
            var result = await _userColumnService.GetAllColumnsByTableIdAsync(tableId).ConfigureAwait(false);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateColumn([FromBody] CreateUserColumnRequestDTO dto, [FromQuery] int tableId)
        {
            if (dto == null) return BadRequest("CreateUserColumnRequestDTO cannot be null.");
            var result = await _userColumnService.CreateColumnAsync(dto, tableId).ConfigureAwait(false);
            return CreatedAtAction(nameof(GetColumnById), new { id = result.Id }, result);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateColumn(int id, [FromBody] UpdateUserColumnRequestDTO dto)
        {
            if (dto == null) return BadRequest("UpdateUserColumnRequestDTO cannot be null.");
            var result = await _userColumnService.UpdateColumnAsync(id, dto).ConfigureAwait(false);
            return Ok(result);
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteColumn(int id)
        {
            bool deleted = await _userColumnService.DeleteColumnAsync(id).ConfigureAwait(false);
            return deleted ? NoContent() : NotFound($"User column with ID {id} not found.");
        }
    }
}