using Backend.DTOs.UserColumn;
using Backend.utilities;

namespace Backend.Interfaces
{
    public interface IUserColumnService
    {
        Task<PagedResult<GetUserColumnResponseDTO>> GetAllColumnsAsync(int pageNumber);
        Task<GetUserColumnResponseDTO?> GetColumnByIdAsync(int id);
        Task<List<GetUserColumnResponseDTO>> GetAllColumnsByTableIdAsync(int tableId);
        Task<GetUserColumnResponseDTO> CreateColumnAsync(CreateUserColumnRequestDTO dto, int tableId);
        Task<GetUserColumnResponseDTO> UpdateColumnAsync(int id, UpdateUserColumnRequestDTO dto);
        Task<bool> DeleteColumnAsync(int id);
    }
}