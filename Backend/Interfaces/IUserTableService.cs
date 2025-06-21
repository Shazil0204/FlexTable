using Backend.DTOs.UserTable;
using Backend.Entities;
using Backend.utilities;
using Backend.Utilities.QueryParams;

namespace Backend.Interfaces
{
    public interface IUserTableService
    {
        Task<PagedResult<GetUserTableResponseDTO>> GetAllTablesAsync(UserTableQuery query);
        Task<GetUserTableResponseDTO?> GetTableByIdAsync(int id);
        Task<GetUserTableResponseDTO> CreateTableAsync(string name);
        Task<GetUserTableResponseDTO> UpdateTableAsync(int id, string name);
        Task<bool> DeleteTableAsync(int id);
    }
}