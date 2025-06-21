using Backend.Data;
using Backend.Entities;
using Backend.Interfaces;
using Microsoft.EntityFrameworkCore;
namespace Backend.Repositories
{
    public class UserTableRepo : IUserTableRepo
    {
        private readonly AppDBContext _context;

        public UserTableRepo(AppDBContext context)
        {
            _context = context;
        }

        public async Task<List<UserTable>> GetAllTablesAsync()
        {
            return await _context.UserTables
                .Include(t => t.Columns)
                .Include(t => t.Rows)
                .ToListAsync();
        }

        public async Task<UserTable?> GetTableByIdAsync(int id)
        {
            UserTable? table = await _context.UserTables
                .Include(t => t.Columns)
                .Include(t => t.Rows)
                .FirstOrDefaultAsync(t => t.Id == id) ?? throw new Exception($"Table with Id {id} was not found.");
            return table;
        }

        public async Task<UserTable> CreateTableAsync(UserTable table)
        {
            _context.UserTables.Add(table);
            int created = await _context.SaveChangesAsync();
            if (created == 0) throw new Exception("Failed to create UserTable.");
            return table;
        }

        public async Task<UserTable> UpdateTableAsync(int id, UserTable updatedTable)
        {
            UserTable existingTable = await _context.UserTables.FindAsync(id) ?? throw new Exception($"UserTable with Id {id} not found.");
            _context.Entry(existingTable).CurrentValues.SetValues(updatedTable);
            int updated = await _context.SaveChangesAsync();
            if (updated == 0) throw new Exception("Failed to update UserTable.");
            return existingTable;
        }

        public async Task<bool> DeleteTableAsync(int id)
        {
            UserTable table = await _context.UserTables.FindAsync(id) ?? throw new Exception($"UserTable with Id {id} not found.");
            _context.UserTables.Remove(table);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
