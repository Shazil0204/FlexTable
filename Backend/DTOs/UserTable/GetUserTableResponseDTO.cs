using Backend.DTOs.UserColumn;
using Backend.DTOs.UserRow;

namespace Backend.DTOs.UserTable
{
    public class GetUserTableResponseDTO : BaseResponseDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime CreatedAt { get; set; }
        public List<GetUserColumnResponseDTO> Columns { get; set; } = [];
        public List<GetUserRowResponseDTO> Rows { get; set; } = [];

        public GetUserTableResponseDTO(int id, string name, DateTime createdAt, List<GetUserRowResponseDTO> rows, List<GetUserColumnResponseDTO> columns)
        {
            Id = id;
            Name = name;
            CreatedAt = createdAt;
            Rows = rows;
            Columns = columns;
        }
    }
}