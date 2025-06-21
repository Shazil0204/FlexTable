using Backend.DTOs.UserTable;
using Backend.Entities;
using Backend.Interfaces;
using Backend.Mappers;
using Backend.utilities;
using Backend.Utilities.QueryParams;

namespace Backend.Services
{
    public class UserTableService : IUserTableService
    {
        private readonly IUserTableRepo _userTableRepo;
        public UserTableService(IUserTableRepo userTableRepo)
        {
            _userTableRepo = userTableRepo;
        }

        /// <summary>
        /// Retrieves a paginated list of <see cref="UserTable"/> objects based on the specified query parameters.
        /// </summary>
        /// <param name="query">The query parameters for filtering and pagination, including page number and optional table name filter.</param>
        /// <returns>
        /// A <see cref="Task{TResult}"/> representing the asynchronous operation, containing a <see cref="PagedResult{UserTable}"/>
        /// with the filtered and paginated list of user tables and the total count.
        /// </returns>
        public async Task<PagedResult<GetUserTableResponseDTO>> GetAllTablesAsync(UserTableQuery query)
        {
            int skip = (query.PageNumber - 1) * 30;

            List<UserTable> userTables = await _userTableRepo.GetAllTablesAsync().ConfigureAwait(false);

            var filteredTables = userTables
                .Where(table => string.IsNullOrEmpty(query.TableName) || table.Name.Contains(query.TableName, StringComparison.OrdinalIgnoreCase))
                .ToList();

            int totalCount = filteredTables.Count;

            var pagedTables = filteredTables
                .Skip(skip)
                .Take(30)
                .Select(UserTableMapper.MapToDTO)
                .ToList();

            return new PagedResult<GetUserTableResponseDTO>
            {
                Results = pagedTables,
                TotalCount = totalCount,
            };
        }

        public async Task<GetUserTableResponseDTO?> GetTableByIdAsync(int id)
        {
            var userTable = await _userTableRepo.GetTableByIdAsync(id).ConfigureAwait(false);
            return userTable != null ? UserTableMapper.MapToDTO(userTable) : throw new KeyNotFoundException($"User table with ID {id} not found.");
        }

        public async Task<GetUserTableResponseDTO> CreateTableAsync(string name)
        {
            if (string.IsNullOrWhiteSpace(name)) throw new ArgumentException("Table name cannot be null or empty.", nameof(name));
            UserTable userTable = UserTableMapper.MapToEntity(name);
            UserTable createdTable = await _userTableRepo.CreateTableAsync(userTable).ConfigureAwait(false);
            return UserTableMapper.MapToDTO(createdTable);
        }

        public async Task<GetUserTableResponseDTO> UpdateTableAsync(int id, string name)
        {
            if (id <= 0) throw new ArgumentException("Invalid table ID.", nameof(id));
            if (string.IsNullOrWhiteSpace(name)) throw new ArgumentException("Table name cannot be null or empty.", nameof(name));

            UserTable? existingUserTable = await _userTableRepo.GetTableByIdAsync(id).ConfigureAwait(false) ?? throw new KeyNotFoundException($"User table with ID {id} not found.");

            if (!UserTableMapper.HasActualUpdates(name, existingUserTable))
            {
                return UserTableMapper.MapToDTO(existingUserTable);
            }

            UserTable updatedUserTable = UserTableMapper.UpdateToEntity(existingUserTable, name);
            UserTable updatedEntity = await _userTableRepo.UpdateTableAsync(id, updatedUserTable).ConfigureAwait(false);
            return UserTableMapper.MapToDTO(updatedEntity);
        }

        public async Task<bool> DeleteTableAsync(int id)
        {
            if (id <= 0) throw new ArgumentException("Invalid table ID.", nameof(id));
            return await _userTableRepo.DeleteTableAsync(id).ConfigureAwait(false);
        }
    }
}