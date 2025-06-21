using System.ComponentModel.DataAnnotations;
using Backend.Enum;

namespace Backend.DTOs.UserColumn
{
    public class CreateUserColumnRequestDTO
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public ColumnDataType DataType { get; set; }
        [Required]
        public bool IsRequired { get; set; }

        public CreateUserColumnRequestDTO(string name, ColumnDataType dataType, bool isRequired)
        {
            Name = name;
            DataType = dataType;
            IsRequired = isRequired;
        }

        public CreateUserColumnRequestDTO() // EF Core requires a parameterless constructor
        {
            Name = string.Empty;
            DataType = ColumnDataType.Text;
            IsRequired = false;
        }
    }
}