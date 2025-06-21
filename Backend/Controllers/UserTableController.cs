using Backend.Interfaces;
using Backend.Utilities.QueryParams;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserTableController : ControllerBase
    {
        private readonly IUserTableService _userTableService;

        public UserTableController(IUserTableService userTableService)
        {
            _userTableService = userTableService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllTables([FromQuery] UserTableQuery query)
        {
            var pagedResult = await _userTableService.GetAllTablesAsync(query);
            return Ok(pagedResult);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetTableById(int id)
        {
            var table = await _userTableService.GetTableByIdAsync(id);
            if (table == null) return NotFound($"Table with Id {id} not found.");
            return Ok(table);
        }

        [HttpPost]
        public async Task<IActionResult> CreateTable([FromBody] string name)
        {
            if (string.IsNullOrWhiteSpace(name)) return BadRequest("Table name cannot be null or empty.");
            var createdTable = await _userTableService.CreateTableAsync(name);
            return CreatedAtAction(nameof(GetTableById), new { id = createdTable.Id }, createdTable);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateTable(int id, [FromBody] string name)
        {
            if (string.IsNullOrWhiteSpace(name)) return BadRequest("Table name cannot be null or empty.");
            var updatedTable = await _userTableService.UpdateTableAsync(id, name);
            return Ok(updatedTable);
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteTable(int id)
        {
            bool deleted = await _userTableService.DeleteTableAsync(id);
            if (!deleted) return NotFound($"Table with Id {id} not found.");
            return NoContent();
        }
    }
}