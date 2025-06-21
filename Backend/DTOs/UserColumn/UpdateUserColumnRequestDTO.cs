using Backend.Enum;

namespace Backend.DTOs.UserColumn
{
    public class UpdateUserColumnRequestDTO
    {
        public string? Name { get; set; }
        public ColumnDataType? DataType { get; set; }
        public bool? IsRequired { get; set; }

        public UpdateUserColumnRequestDTO(string? name = null, ColumnDataType? dataType = null, bool? isRequired = null)
        {
            Name = name;
            DataType = dataType;
            IsRequired = isRequired;
        }
    }
}