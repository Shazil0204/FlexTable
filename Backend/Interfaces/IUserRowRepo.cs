using Backend.Entities;

namespace Backend.Interfaces
{
    public interface IUserRowRepo
    {
        Task<List<UserRow>> GetAllRowsAsync();
        Task<UserRow?> GetRowByIdAsync(int id);
        Task<List<UserRow>> GetAllRowsbyTableIdAsync(int tableId);
        Task<UserRow> CreateRowAsync(UserRow row);
        Task<UserRow> UpdateRowAsync(int id, UserRow updatedRow);
        Task<bool> DeleteRowAsync(int id);
    }
}