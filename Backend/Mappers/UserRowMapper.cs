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
        public static bool HasActualUpdates(string data, UserRow existingUserRow)
        {
            return
            !string.IsNullOrWhiteSpace(data) && data != existingUserRow.Data;
        }

        public static UserRow UpdateToEntity(UserRow entity, string data)
        {
            entity.TableId = entity.TableId;
            entity.Data = data ?? entity.Data;

            return entity;
        }
        #endregion
    }
}