using Backend.DTOs.UserTable;
using Backend.Entities;

namespace Backend.Mappers
{
    public static class UserTableMapper
    {
        public static UserTable MapToEntity(string name)
        {
            return new UserTable(name);
        }

        public static GetUserTableResponseDTO MapToDTO(UserTable userTable)
        {
            return new GetUserTableResponseDTO
            (
                userTable.Id,
                userTable.Name,
                userTable.CreatedAt,
                userTable.Rows.Select(UserRowMapper.MapToDTO).ToList(),
                userTable.Columns.Select(UserColumnMapper.MapToDTO).ToList()
            );
        }

        #region Update Methods
        public static bool HasActualUpdates(string name, UserTable existingUserTable)
        {
            return
            !string.IsNullOrWhiteSpace(name) && name != existingUserTable.Name;
        }

        public static UserTable UpdateToEntity(UserTable entity, string name)
        {
            entity.Name = name ?? entity.Name;
            return entity;
        }
        #endregion
    }
}