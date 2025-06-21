using Backend.DTOs.UserRow;
using Backend.utilities;

namespace Backend.Interfaces
{
    public interface IUserRowService
    {
        Task<PagedResult<GetUserRowResponseDTO>> GetAllRowsAsync(int pageNumber);
        Task<GetUserRowResponseDTO?> GetRowByIdAsync(int id);
        Task<List<GetUserRowResponseDTO>> GetAllRowsByTableIdAsync(int tableId);
        Task<GetUserRowResponseDTO> CreateRowAsync(string data, int tableId);
        Task<GetUserRowResponseDTO> UpdateRowAsync(int id, UpdateUserRowRequestDTO dto);
        Task<bool> DeleteRowAsync(int id);
    }
}