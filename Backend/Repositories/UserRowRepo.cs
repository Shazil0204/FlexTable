using Backend.Data;
using Backend.Entities;
using Backend.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Backend.Repositories
{
    public class UserRowRepo : IUserRowRepo
    {
        private readonly AppDBContext _context;

        public UserRowRepo(AppDBContext context)
        {
            _context = context;
        }

        public async Task<List<UserRow>> GetAllRowsAsync()
        {
            return await _context.UserRows
                .ToListAsync()
                .ConfigureAwait(false);
        }

        public async Task<UserRow?> GetRowByIdAsync(int id)
        {
            UserRow? row = await _context.UserRows
                .FirstOrDefaultAsync(r => r.Id == id)
                .ConfigureAwait(false);
            return row ?? throw new KeyNotFoundException($"User row with ID {id} not found.");
        }

        public async Task<List<UserRow>> GetAllRowsbyTableIdAsync(int tableId)
        {
            return await _context.UserRows
                .Where(r => r.TableId == tableId)
                .ToListAsync()
                .ConfigureAwait(false);
        }

        public async Task<UserRow> CreateRowAsync(UserRow row)
        {
            if (row == null) throw new ArgumentNullException(nameof(row), "UserRow cannot be null.");
            _context.UserRows.Add(row);
            var created = await _context.SaveChangesAsync().ConfigureAwait(false);
            if (created == 0) throw new Exception("Failed to create UserRow.");
            return row;
        }

        public async Task<UserRow> UpdateRowAsync(int id, UserRow updatedRow)
        {
            if (updatedRow == null) throw new ArgumentNullException(nameof(updatedRow), "UserRow cannot be null.");
            UserRow? existingRow = await _context.UserRows.FindAsync(id).ConfigureAwait(false)
                ?? throw new KeyNotFoundException($"User row with ID {id} not found.");
            _context.Entry(existingRow).CurrentValues.SetValues(updatedRow);
            int updated = await _context.SaveChangesAsync().ConfigureAwait(false);
            if (updated == 0) throw new Exception("Failed to update UserRow.");
            return existingRow;
        }

        public async Task<bool> DeleteRowAsync(int id)
        {
            UserRow? row = await _context.UserRows.FindAsync(id).ConfigureAwait(false)
                ?? throw new KeyNotFoundException($"User row with ID {id} not found.");
            _context.UserRows.Remove(row);
            int deleted = await _context.SaveChangesAsync().ConfigureAwait(false);
            if (deleted == 0) throw new Exception("Failed to delete UserRow.");
            return true;
        }
    }
}