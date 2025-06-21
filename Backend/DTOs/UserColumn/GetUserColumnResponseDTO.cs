using Backend.Enum;

namespace Backend.DTOs.UserColumn
{
    public class GetUserColumnResponseDTO : BaseResponseDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ColumnDataType DataType { get; set; }
        public bool IsRequired { get; set; }
        public GetUserColumnResponseDTO(int id, string name, ColumnDataType dataType, bool isRequired)
        {
            Id = id;
            Name = name;
            DataType = dataType;
            IsRequired = isRequired;
        }
    }
}