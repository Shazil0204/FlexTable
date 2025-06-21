using Backend.Data;
using Backend.Entities;
using Backend.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Backend.Repositories
{
    public class UserColumnRepo : IUserColumnRepo
    {
        private readonly AppDBContext _context;
        public UserColumnRepo(AppDBContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context), "AppDBContext cannot be null.");
        }

        public async Task<List<UserColumn>> GetAllColumnsAsync()
        {
            return await _context.UserColumns
                .Include(c => c.Table)
                .ToListAsync()
                .ConfigureAwait(false);
        }

        public async Task<UserColumn?> GetColumnByIdAsync(int id)
        {
            UserColumn? column = await _context.UserColumns
                .FirstOrDefaultAsync(c => c.Id == id)
                .ConfigureAwait(false);
            return column ?? throw new KeyNotFoundException($"User column with ID {id} not found.");
        }

        public async Task<List<UserColumn>> GetAllColumnsByTableIdAsync(int tableId)
        {
            return await _context.UserColumns
                .Where(c => c.TableId == tableId)
                .ToListAsync()
                .ConfigureAwait(false);
        }

        public async Task<UserColumn> CreateColumnAsync(UserColumn column)
        {
            if (column == null) throw new ArgumentNullException(nameof(column), "UserColumn cannot be null.");
            _context.UserColumns.Add(column);
            var created = await _context.SaveChangesAsync().ConfigureAwait(false);
            if (created == 0) throw new Exception("Failed to create UserColumn.");
            return column;
        }

        public async Task<UserColumn> UpdateColumnAsync(int id, UserColumn updatedColumn)
        {
            if (updatedColumn == null) throw new ArgumentNullException(nameof(updatedColumn), "UserColumn cannot be null.");
            UserColumn? existingColumn = await _context.UserColumns.FindAsync(id).ConfigureAwait(false)
                ?? throw new KeyNotFoundException($"User column with ID {id} not found.");
            _context.Entry(existingColumn).CurrentValues.SetValues(updatedColumn);
            int updated = await _context.SaveChangesAsync().ConfigureAwait(false);
            if (updated == 0) throw new Exception("Failed to update UserColumn.");
            return existingColumn;
        }
        
        public async Task<bool> DeleteColumnAsync(int id)
        {
            UserColumn? column = await _context.UserColumns.FindAsync(id).ConfigureAwait(false)
                ?? throw new KeyNotFoundException($"User column with ID {id} not found.");
            _context.UserColumns.Remove(column);
            int deleted = await _context.SaveChangesAsync().ConfigureAwait(false);
            if (deleted == 0) throw new Exception("Failed to delete UserColumn.");
            return true;
        }
    }
}