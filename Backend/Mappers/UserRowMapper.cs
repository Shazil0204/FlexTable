using Backend.DTOs.UserRow;
using Backend.Entities;

namespace Backend.Mappers
{
    public static class UserRowMapper
    {
        public static UserRow MapToEntity(string data, int tableId)
        {
            return new UserRow
            (
                tableId,
                data
            );
        }

        public static GetUserRowResponseDTO MapToDTO(UserRow userRow)
        {
            return new GetUserRowResponseDTO
            (
                userRow.Id,
                userRow.Data,
                userRow.CreatedAt
            );
        }

        #region Update Methods
        public static bool HasActualUpdates(UpdateUserRowRequestDTO dto, UserRow existingUserRow)
        {
            return
            (!string.IsNullOrWhiteSpace(dto.Name) && dto.Name != existingUserRow.Table?.Name) ||
            (dto.Data != null && dto.Data != existingUserRow.Data);
        }

        public static UserRow UpdateToEntity(UserRow entity, UpdateUserRowRequestDTO dto)
        {
            entity.TableId = entity.TableId;
            entity.Data = dto.Data ?? entity.Data;

            return entity;
        }
        #endregion
    }
}