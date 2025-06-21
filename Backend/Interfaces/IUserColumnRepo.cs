using Backend.Entities;

namespace Backend.Interfaces
{
    public interface IUserColumnRepo
    {
        Task<List<UserColumn>> GetAllColumnsAsync();
        Task<UserColumn?> GetColumnByIdAsync(int id);
        Task<List<UserColumn>> GetAllColumnsByTableIdAsync(int tableId);
        Task<UserColumn> CreateColumnAsync(UserColumn column);
        Task<UserColumn> UpdateColumnAsync(int id, UserColumn updatedColumn);
        Task<bool> DeleteColumnAsync(int id);
    }
}