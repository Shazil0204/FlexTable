using Backend.DTOs.UserColumn;
using Backend.Entities;

namespace Backend.Mappers
{
    public static class UserColumnMapper
    {
        public static UserColumn MapToEntity(CreateUserColumnRequestDTO dto, int tableId)
        {
            return new UserColumn
            (
                tableId,
                dto.Name,
                dto.DataType,
                dto.IsRequired
            );
        }

        public static GetUserColumnResponseDTO MapToDTO(UserColumn userColumn)
        {
            return new GetUserColumnResponseDTO
            (
                userColumn.Id,
                userColumn.Name,
                userColumn.DataType,
                userColumn.IsRequired
            );
        }

        #region Update Methods
        public static bool HasActualUpdates(UpdateUserColumnRequestDTO dto, UserColumn existingUserColumn)
        {
            return
            (!string.IsNullOrWhiteSpace(dto.Name) && dto.Name != existingUserColumn.Name) ||
            (dto.DataType.HasValue && dto.DataType.Value != existingUserColumn.DataType) ||
            (dto.IsRequired.HasValue && dto.IsRequired.Value != existingUserColumn.IsRequired);
        }

        public static UserColumn UpdateToEntity(UserColumn entity, UpdateUserColumnRequestDTO dto)
        {
            entity.TableId = entity.TableId;
            entity.Name = dto.Name ?? entity.Name;
            entity.DataType = dto.DataType ?? entity.DataType;
            entity.IsRequired = dto.IsRequired ?? entity.IsRequired;

            return entity;
        }
        #endregion
    }
}