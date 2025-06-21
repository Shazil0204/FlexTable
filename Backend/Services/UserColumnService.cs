using Backend.DTOs.UserColumn;
using Backend.Entities;
using Backend.Interfaces;
using Backend.Mappers;
using Backend.utilities;

namespace Backend.Services
{
    public class UserColumnService : IUserColumnService
    {
        private readonly IUserColumnRepo _userColumnRepo;
        public UserColumnService(IUserColumnRepo userColumnRepo)
        {
            _userColumnRepo = userColumnRepo ?? throw new ArgumentNullException(nameof(userColumnRepo), "UserColumnRepo cannot be null.");
        }

        public async Task<PagedResult<GetUserColumnResponseDTO>> GetAllColumnsAsync(int pageNumber)
        {
            int skip = (pageNumber - 1) * 30;

            List<UserColumn> userColumns = await _userColumnRepo.GetAllColumnsAsync().ConfigureAwait(false);

            var allUserColumn = userColumns.ToList().OrderBy(c => c.Id);

            int totalCount = allUserColumn.Count();

            var pagedColumns = allUserColumn
                .Skip(skip)
                .Take(30)
                .Select(UserColumnMapper.MapToDTO)
                .ToList();

            return new PagedResult<GetUserColumnResponseDTO>
            {
                Results = pagedColumns,
                TotalCount = totalCount,
            };
        }

        public async Task<GetUserColumnResponseDTO?> GetColumnByIdAsync(int id)
        {
            UserColumn? userColumn = await _userColumnRepo.GetColumnByIdAsync(id).ConfigureAwait(false);
            return userColumn != null ? UserColumnMapper.MapToDTO(userColumn) : throw new KeyNotFoundException($"User column with ID {id} not found.");
        }

        public async Task<List<GetUserColumnResponseDTO>> GetAllColumnsByTableIdAsync(int tableId)
        {
            if (tableId <= 0) throw new ArgumentException("Invalid table ID.", nameof(tableId));
            List<UserColumn> userColumns = await _userColumnRepo.GetAllColumnsByTableIdAsync(tableId).ConfigureAwait(false);
            return userColumns.Select(UserColumnMapper.MapToDTO).ToList();
        }

        public async Task<GetUserColumnResponseDTO> CreateColumnAsync(CreateUserColumnRequestDTO dto, int tableId)
        {
            if (dto == null) throw new ArgumentNullException(nameof(dto), "CreateUserColumnRequestDTO cannot be null.");
            UserColumn userColumn = UserColumnMapper.MapToEntity(dto, tableId);
            UserColumn createdColumn = await _userColumnRepo.CreateColumnAsync(userColumn).ConfigureAwait(false);
            return UserColumnMapper.MapToDTO(createdColumn);
        }

        public async Task<GetUserColumnResponseDTO> UpdateColumnAsync(int id, UpdateUserColumnRequestDTO dto)
        {
            if (dto == null) throw new ArgumentNullException(nameof(dto), "UpdateUserColumnRequestDTO cannot be null.");
            UserColumn? existingUserColumn = await _userColumnRepo.GetColumnByIdAsync(id).ConfigureAwait(false)
                ?? throw new KeyNotFoundException($"User column with ID {id} not found.");

            if (!UserColumnMapper.HasActualUpdates(dto, existingUserColumn))
            {
                return UserColumnMapper.MapToDTO(existingUserColumn);
            }

            UserColumn updatedUserColumn = UserColumnMapper.UpdateToEntity(existingUserColumn, dto);
            UserColumn updatedColumn = await _userColumnRepo.UpdateColumnAsync(id, updatedUserColumn).ConfigureAwait(false);
            return UserColumnMapper.MapToDTO(updatedColumn);
        }

        public async Task<bool> DeleteColumnAsync(int id)
        {
            if (id <= 0) throw new ArgumentException("Invalid column ID.", nameof(id));
            return await _userColumnRepo.DeleteColumnAsync(id).ConfigureAwait(false);
        }
    }
}