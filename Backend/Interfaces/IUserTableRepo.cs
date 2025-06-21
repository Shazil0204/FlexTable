using Backend.Entities;

namespace Backend.Interfaces
{
    public interface IUserTableRepo
    {
        Task<List<UserTable>> GetAllTablesAsync();
        Task<UserTable?> GetTableByIdAsync(int id);
        Task<UserTable> CreateTableAsync(UserTable table);
        Task<UserTable> UpdateTableAsync(int id, UserTable updatedTable);
        Task<bool> DeleteTableAsync(int id);
    }
}