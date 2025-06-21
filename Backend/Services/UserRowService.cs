using Backend.DTOs.UserRow;
using Backend.Entities;
using Backend.Interfaces;
using Backend.Mappers;
using Backend.utilities;

namespace Backend.Services
{
    public class UserRowService : IUserRowService
    {
        private readonly IUserRowRepo _userRowRepo;
        private readonly IUserColumnService _userColumnService;

        public UserRowService(IUserRowRepo userRowRepo, IUserColumnService userColumnService)
        {
            _userRowRepo = userRowRepo ?? throw new ArgumentNullException(nameof(userRowRepo), "UserRowRepo cannot be null.");
            _userColumnService = userColumnService ?? throw new ArgumentNullException(nameof(userColumnService), "UserColumnService cannot be null.");
        }

        public async Task<PagedResult<GetUserRowResponseDTO>> GetAllRowsAsync(int pageNumber)
        {
            int skip = (pageNumber - 1) * 30;

            List<UserRow> userRows = await _userRowRepo.GetAllRowsAsync().ConfigureAwait(false);

            var allUserRows = userRows.OrderBy(r => r.Id).ToList();

            int totalCount = allUserRows.Count;

            var pagedRows = allUserRows
                .Skip(skip)
                .Take(30)
                .Select(UserRowMapper.MapToDTO)
                .ToList();

            return new PagedResult<GetUserRowResponseDTO>
            {
                Results = pagedRows,
                TotalCount = totalCount,
            };
        }

        public async Task<GetUserRowResponseDTO?> GetRowByIdAsync(int id)
        {
            UserRow? userRow = await _userRowRepo.GetRowByIdAsync(id).ConfigureAwait(false);
            return userRow != null ? UserRowMapper.MapToDTO(userRow) : throw new KeyNotFoundException($"User row with ID {id} not found.");
        }

        public async Task<List<GetUserRowResponseDTO>> GetAllRowsByTableIdAsync(int tableId)
        {
            List<UserRow> userRows = await _userRowRepo.GetAllRowsbyTableIdAsync(tableId).ConfigureAwait(false);
            return userRows.Select(UserRowMapper.MapToDTO).ToList();
        }

        public async Task<GetUserRowResponseDTO> CreateRowAsync(string data, int tableId)
        {
            if (data == null) throw new ArgumentNullException(nameof(data), "CreateUserRowRequestDTO cannot be null.");

            // Step 1: Fetch all columns for the table
            var columns = await _userColumnService.GetAllColumnsByTableIdAsync(tableId).ConfigureAwait(false);
            var columnNames = columns.Select(c => c.Name).ToList();

            // Step 2: Validate that the JSON string contains all required keys
            var dataDict = System.Text.Json.JsonSerializer.Deserialize<Dictionary<string, object>>(data);
            if (dataDict == null)
                throw new ArgumentException("Invalid JSON data.", nameof(data));

            var missingColumns = columnNames.Where(name => !dataDict.ContainsKey(name)).ToList();
            if (missingColumns.Any())
                throw new ArgumentException($"Missing data for columns: {string.Join(", ", missingColumns)}");

            // Step 3: Save the JSON string as-is
            UserRow userRow = UserRowMapper.MapToEntity(data, tableId);
            UserRow createdRow = await _userRowRepo.CreateRowAsync(userRow).ConfigureAwait(false);
            return UserRowMapper.MapToDTO(createdRow);
        }

        public async Task<GetUserRowResponseDTO> UpdateRowAsync(int id, UpdateUserRowRequestDTO dto)
        {
            if (dto == null) throw new ArgumentNullException(nameof(dto), "UpdateUserRowRequestDTO cannot be null.");
            UserRow? existingUserRow = await _userRowRepo.GetRowByIdAsync(id).ConfigureAwait(false)
                ?? throw new KeyNotFoundException($"User row with ID {id} not found.");

            if (!UserRowMapper.HasActualUpdates(dto, existingUserRow))
            {
                return UserRowMapper.MapToDTO(existingUserRow);
            }

            UserRow updatedUserRow = UserRowMapper.UpdateToEntity(existingUserRow, dto);
            UserRow updatedRow = await _userRowRepo.UpdateRowAsync(id, updatedUserRow).ConfigureAwait(false);
            return UserRowMapper.MapToDTO(updatedRow);
        }

        public async Task<bool> DeleteRowAsync(int id)
        {
            if (id <= 0) throw new ArgumentException("Invalid row ID.", nameof(id));
            return await _userRowRepo.DeleteRowAsync(id).ConfigureAwait(false);
        }
    }
}